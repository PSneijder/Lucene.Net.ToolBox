using System.Windows.Documents;
using System.Windows.Media;
using Lucene.Net.Toolbox.Contracts;

namespace Lucene.Net.Toolbox.Desktop.Controls.Formatters
{
    sealed class HighlightTextFormatter
        : ITextFormatter
    {
        public string GetText(FlowDocument document)
        {
            return new TextRange(document.ContentStart, document.ContentEnd).Text;
        }

        public void SetText(FlowDocument document, string text, Color color, IToken token)
        {
            var textRange = new TextRange(document.ContentStart, document.ContentEnd)
            {
                Text = text
            };

            if (token == null)
            { 
                return;
            }

            var startOffset = textRange.Start.GetPositionAtOffset(token.StartOffset);
            var endOffset = textRange.Start.GetPositionAtOffset(token.EndOffset);

            var highlightRange = new TextRange(startOffset, endOffset);
            highlightRange.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(color));
        }
    }
}