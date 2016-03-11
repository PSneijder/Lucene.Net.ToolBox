using Lucene.Net.Analysis;
using Lucene.Net.Toolbox.Contracts;
using Lucene.Net.Toolbox.Impl.Info;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Lucene.Net.Toolbox.Impl.Discovery
{
    public abstract class BaseDiscovery
        : IDiscovery
    {
        protected string AssemblyName { get { return "Lucene.Net"; } }

        bool IDiscovery.IsRunning { get { return true; } }

        public virtual void Discover() { }

        public virtual void Dispose() { }

        public event DiscoverEventHandler Discovered;

        protected void OnDiscovered(IAnalyzer analyzer, EventArgs e)
        {
            if (Discovered != null)
            {
                Discovered(analyzer, e);
            }
        }

        protected void DiscoverAnalyzers(string fullPath)
        {
            Assembly assembly = null;

            try
            {
                FileStream fs = File.Open(fullPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                fs.Close();

                assembly = Assembly.LoadFrom(fullPath);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }

            if(assembly != null)
            { 
                Discover(assembly);
            }
        }

        protected void DiscoverAnalyzers()
        {
            var assemblyName = Assembly
                .GetExecutingAssembly()
                .GetReferencedAssemblies()
                .FirstOrDefault(a => a.Name == AssemblyName);

            Assembly assembly = Assembly.Load(assemblyName);

            Discover(assembly);
        }

        private void Discover(Assembly assembly)
        {
            var analyzerTypes = assembly.GetExportedTypes()
                 .Where(t => typeof(Analyzer).IsAssignableFrom(t))
                 .Where(t => t != typeof(Analyzer))
                 .Where(t => !t.IsAbstract);

            foreach (var analyzerType in analyzerTypes)
            {
                var analyzer = CreateAnalyzer(analyzerType);

                OnDiscovered(analyzer, new EventArgs());

                Trace.WriteLine(string.Format("Discovery: {0}, {1}", analyzer.Name, analyzer.Type.FullName));
            }
        }

        protected IAnalyzer CreateAnalyzer(Type type)
        {
            var analyzerName = type.Name;

            AnalyzerInfo analyzerInfo = new AnalyzerInfo
            {
                Name = analyzerName,
                Type = type
            };

            return analyzerInfo;
        }
    }
}