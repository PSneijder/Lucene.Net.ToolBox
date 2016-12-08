using System.Windows.Documents;

namespace Lucene.Net.Toolbox.Desktop.Utils.RichTextBox.Formatters
{
    interface ITextFormatter
    {
        string GetText(FlowDocument document);
        void SetText(FlowDocument document, string text);
    }
}