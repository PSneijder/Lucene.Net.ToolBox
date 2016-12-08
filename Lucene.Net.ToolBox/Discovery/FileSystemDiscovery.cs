using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Lucene.Net.Toolbox.Impl.Discovery
{
    sealed class FileSystemDiscovery
        : BaseDiscovery
    {
        private readonly SynchronizationContext _context;
        private readonly FileSystemWatcher _watcher;
        private readonly Queue<FileSystemEventArgs> _queue;
        private readonly Timer _timer;

        private readonly string _folder;
        private readonly string _extension;
        private readonly int _interval;

        public string Folder => _folder;
        public string Extension => _extension;
        public bool IsRunning => _watcher?.EnableRaisingEvents ?? false;
        public int Interval => _interval;

        public FileSystemDiscovery(string filePath, string fileExtension, int interval)
            : this()
        {
            _folder = filePath;
            _extension = fileExtension;
            _interval = interval;
        }

        public FileSystemDiscovery()
        {
            _context = SynchronizationContext.Current;

            try
            {
                if (string.IsNullOrWhiteSpace(_folder))
                { 
                    _folder = "PlugIns";
                }
                if (string.IsNullOrWhiteSpace(_extension))
                { 
                    _extension = "*.dll";
                }
                if(_interval == 0)
                {
                    _interval = 2000;
                }

                _timer = CreateTimer();
                _queue = CreateQueue();
                _watcher = CreateFileSystemWatcher(_folder, _extension);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }

        public override void Discover()
        {
            DiscoverAnalyzers();

            var path = GetOrCreateFolder(_folder);
            var files = Directory.EnumerateFiles(path, _extension);

            foreach (var file in files)
            {
                DiscoverAnalyzers(file);
            }
        }

        public override void Dispose()
        {
            _timer.Dispose();
            _watcher.Dispose();
        }

        private FileSystemWatcher CreateFileSystemWatcher(string folder, string extension)
        {
            var path = GetOrCreateFolder(folder);
            var fileWatcher = new FileSystemWatcher(path, extension);

            fileWatcher.Created += (s, e) => { _queue.Enqueue(e); };
            fileWatcher.EnableRaisingEvents = true;

            return fileWatcher;
        }

        private string GetOrCreateFolder(string folder)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        private void ProcessQueue(object sender, ElapsedEventArgs e)
        {
            while (_queue.Count != 0)
            {
                FileSystemEventArgs args = _queue.Dequeue();

                _context.Post(state => { DiscoverAnalyzers(args.FullPath); }, null);
            }
        }

        private Queue<FileSystemEventArgs> CreateQueue()
        {
            return new Queue<FileSystemEventArgs>();
        }

        private Timer CreateTimer()
        {
            var timer = new Timer
            {
                Interval = Interval
            };

            timer.Start();
            timer.Elapsed += ProcessQueue;

            return timer;
        }
    }
}