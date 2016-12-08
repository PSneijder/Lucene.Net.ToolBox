using Ninject;

namespace Lucene.Net.Toolbox.Desktop.Extensions
{
    public static class NinjectExtensions
    {
        public static void Initialize(this IKernel kernel)
        {
            kernel.Load(new ToolboxModule(), new DesktopModule());
        }
    }
}