using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace Lucene.Net.Toolbox.Desktop.Utils
{
    sealed class StringBuilderTraceListener
        : TraceListener
            , ITraceListener
    {
        #region Fields

        private readonly StringBuilder _builder;

        #endregion

        #region Properties

        public string Trace => _builder.ToString();

        #endregion

        public StringBuilderTraceListener()
        {
            _builder = new StringBuilder();
        }

        #region Overrides

        public override void WriteLine(string message)
        {
            _builder.Append($"{message}{Environment.NewLine}");

            OnPropertyChanged(nameof(Trace));
        }

        public override void Write(string message)
        {
            _builder.Append(message);

            OnPropertyChanged(nameof(Trace));
        }

        #endregion

        #region INotifyPropertyChanged Implementations

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}