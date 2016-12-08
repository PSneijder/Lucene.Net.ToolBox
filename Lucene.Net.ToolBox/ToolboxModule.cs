using Lucene.Net.Toolbox.Contracts;
using Lucene.Net.Toolbox.Discovery;
using Lucene.Net.Toolbox.Info;
using Ninject.Modules;

namespace Lucene.Net.Toolbox
{
    public sealed class ToolboxModule
        : NinjectModule
    {
        public override void Load()
        {
            Bind<IDiscovery>()
                .To<FileSystemDiscovery>();

            Bind<IAnalyzer>()
                .To<AnalyzerInfo>();

            Bind<IToken>()
                .To<TokenInfo>();
        }
    }
}