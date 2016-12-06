using Lucene.Net.Analysis;
using Lucene.Net.Toolbox.Contracts;
using Lucene.Net.Toolbox.Impl.Info;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Lucene.Net.Toolbox.Impl.Discovery
{
    public abstract class BaseDiscovery
        : IDiscovery
    {
        protected string AssemblyName => "Lucene.Net";

        bool IDiscovery.IsRunning => true;

        public virtual void Discover() { }

        public virtual void Dispose() { }

        public event DiscoverEventHandler Discovered;

        protected void OnDiscovered(IAnalyzer analyzer, EventArgs args)
        {
            Discovered?.Invoke(analyzer, args);
        }

        protected void DiscoverAnalyzers(string fullPath)
        {
            try
            {
                var assembly = Assembly.LoadFrom(fullPath);

                if (assembly != null)
                {
                    Discover(assembly);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }

        protected void DiscoverAnalyzers()
        {
            var assemblyName = Assembly
                .GetExecutingAssembly()
                    .GetReferencedAssemblies()
                        .FirstOrDefault(a => a.Name == AssemblyName);

            if (assemblyName == null)
            { 
                return;
            }

            var assembly = Assembly.Load(assemblyName);

            Discover(assembly);
        }

        private void Discover(Assembly assembly)
        {
            var analyzerTypes = assembly.GetExportedTypes()
                    .Where(t => typeof(Analyzer).IsAssignableFrom(t) && t != typeof(Analyzer) && !t.IsAbstract);

            foreach (var analyzerType in analyzerTypes)
            {
                IAnalyzer analyzer = CreateAnalyzer(analyzerType);

                OnDiscovered(analyzer, new EventArgs());

                Trace.WriteLine($"Discovery: {analyzer.Name}, {analyzer.Type.FullName}");
            }
        }

        protected IAnalyzer CreateAnalyzer(Type type)
        {
            var analyzerName = type.Name;

            var analyzerInfo = new AnalyzerInfo
            {
                Name = analyzerName,
                Type = type
            };

            return analyzerInfo;
        }
    }
}