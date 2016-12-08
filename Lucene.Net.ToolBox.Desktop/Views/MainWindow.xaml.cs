using Lucene.Net.Toolbox.Desktop.ViewModels;

namespace Lucene.Net.Toolbox.Desktop.Views
{
    public partial class MainWindow
    {
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}