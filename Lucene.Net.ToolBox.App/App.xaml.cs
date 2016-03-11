using System.Windows;
using Lucene.Net.Toolbox.Shared;
using Lucene.Net.Toolbox.ViewModels;
using Lucene.Net.Toolbox.Views;
using Ninject;

namespace Lucene.Net.Toolbox
{
    public partial class App
        : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var kernel = CreateKernel();
            var window = CreateWindow(kernel);

            window.Show();
        }

        private Window CreateWindow(IKernel kernel)
        {
            return new MainWindow
            {
                DataContext = kernel.Get<MainWindowViewModel>()
            };
        }

        private IKernel CreateKernel()
        {
            var kernel = new StandardKernel(new SharedModule());
            kernel.Bind<MainWindowViewModel>().ToSelf();

            return kernel;
        }
    }
}