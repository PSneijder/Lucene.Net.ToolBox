using System.Windows.Documents;

namespace Lucene.Net.Toolbox.Utils.RichTextBox.Formatters
{
    sealed class PlainTextFormatter
        : ITextFormatter
    {
        public string GetText(FlowDocument document)
        {
            return new TextRange(document.ContentStart, document.ContentEnd).Text;
        }

        public void SetText(FlowDocument document, string text)
        {
            new TextRange(document.ContentStart, document.ContentEnd).Text = text;
        }
    }
}