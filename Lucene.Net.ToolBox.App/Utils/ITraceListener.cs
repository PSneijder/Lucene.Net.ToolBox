using System.ComponentModel;

namespace Lucene.Net.Toolbox.Desktop.Utils
{
    public interface ITraceListener
        : INotifyPropertyChanged
    {
        string Trace { get; }
    }
}