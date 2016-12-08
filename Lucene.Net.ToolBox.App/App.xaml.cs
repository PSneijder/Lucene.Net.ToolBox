using System.Windows;
using Lucene.Net.Toolbox.Desktop.Modularity;

namespace Lucene.Net.Toolbox.Desktop
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bootstrapper = new DesktopBootstrapper();
            bootstrapper.Run();
        }
    }
}