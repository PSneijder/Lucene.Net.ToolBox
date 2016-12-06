using System.Diagnostics;
using Lucene.Net.Toolbox.Utils;

namespace Lucene.Net.Toolbox.Views
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Trace.Listeners.Add(new TextBoxTraceListener(TraceListenerOutput));
        }
    }
}