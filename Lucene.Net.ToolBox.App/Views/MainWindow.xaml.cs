using System.Diagnostics;
using Lucene.Net.Toolbox.Utils;
using MahApps.Metro.Controls;

namespace Lucene.Net.Toolbox.Views
{
    public partial class MainWindow
        : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Trace.Listeners.Add(new TextBoxTraceListener(TraceListenerOutput));
        }
    }
}