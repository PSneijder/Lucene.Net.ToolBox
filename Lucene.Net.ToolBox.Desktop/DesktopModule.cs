using Lucene.Net.Toolbox.Desktop.Utils;
using Ninject.Modules;

namespace Lucene.Net.Toolbox.Desktop
{
    public sealed class DesktopModule
        : NinjectModule
    {
        public override void Load()
        {
            Bind<ITraceListener>()
                .To<StringBuilderTraceListener>()
                    .InSingletonScope();  
        }
    }
}