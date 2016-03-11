using Lucene.Net.Toolbox.Contracts;
using Lucene.Net.Toolbox.Impl.Discovery;
using Lucene.Net.Toolbox.Impl.Info;
using Ninject.Modules;

namespace Lucene.Net.Toolbox.Shared
{
    public class SharedModule
        : NinjectModule
    {
        public override void Load()
        {
            Bind<IDiscovery>().To<FileSystemDiscovery>();

            Bind<IAnalyzer>().To<AnalyzerInfo>();
            Bind<IToken>().To<TokenInfo>();
        }
    }
}