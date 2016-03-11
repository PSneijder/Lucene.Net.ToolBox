using System;
using System.Diagnostics;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace Lucene.Net.Toolbox.Utils
{
    internal sealed class TextBoxTraceListener
            : TraceListener
    {
        private readonly TextBoxBase _textBox;

        public TextBoxTraceListener(TextBoxBase textBox)
        {
            _textBox = textBox;
        }

        public override void Write(string message)
        {
            Action append = delegate
            {
                _textBox.AppendText(message);
            };

            _textBox.Dispatcher.Invoke(DispatcherPriority.Normal, append);
        }

        public override void WriteLine(string message)
        {
            Write(message + Environment.NewLine);
        }
    }
}