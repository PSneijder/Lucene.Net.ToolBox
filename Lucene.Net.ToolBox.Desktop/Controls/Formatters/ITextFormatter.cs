using System.Windows.Documents;
using System.Windows.Media;
using Lucene.Net.Toolbox.Contracts;

namespace Lucene.Net.Toolbox.Desktop.Controls.Formatters
{
    interface ITextFormatter
    {
        string GetText(FlowDocument document);
        void SetText(FlowDocument document, string text, Color color, IToken token);
    }
}