using System.Diagnostics;
using System.Windows;
using Lucene.Net.Toolbox.Desktop.Extensions;
using Lucene.Net.Toolbox.Desktop.Utils;
using Lucene.Net.Toolbox.Desktop.Views;
using Ninject;
using Prism.Modularity;
using Prism.Ninject;

namespace Lucene.Net.Toolbox.Desktop.Modularity
{
    sealed class DesktopBootstrapper
        : NinjectBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Kernel.Get<MainWindow>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window) Shell;
            Application.Current.MainWindow.Show();
        }
        
        protected override void ConfigureKernel()
        {
            base.ConfigureKernel();

            Kernel.Initialize();

            var listener = Kernel.Get<ITraceListener>() as TraceListener;
            if (listener != null)
            {
                Trace.Listeners.Add(listener);
            }
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new AggregateModuleCatalog();
        }
    }
}