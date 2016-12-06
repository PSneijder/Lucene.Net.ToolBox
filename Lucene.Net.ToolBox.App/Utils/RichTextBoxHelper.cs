using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace Lucene.Net.Toolbox.Utils
{
    internal sealed class RichTextBoxHelper
        : DependencyObject
    {
        public static string GetDocumentFromXaml(DependencyObject d)
        {
            return (string) d.GetValue(DocumentFromXaml);
        }

        public static void SetDocumentFromXaml(DependencyObject d, string value)
        {
            d.SetValue(DocumentFromXaml, value);
        }

        public static readonly DependencyProperty DocumentFromXaml
            = DependencyProperty.RegisterAttached("DocumentFromXaml", typeof(string), typeof(RichTextBoxHelper),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, PropertyChanged));

        private static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var richTextBox = (RichTextBox) d;
            var xaml = GetDocumentFromXaml(richTextBox);

            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xaml)))
            {
                FlowDocument document = (FlowDocument) XamlReader.Load(stream);
                richTextBox.Document = document;
            }

            richTextBox.TextChanged += (obj, args) =>
            {
                RichTextBox textBox = obj as RichTextBox;
                if (textBox != null)
                {
                    SetDocumentFromXaml(richTextBox, XamlWriter.Save(textBox.Document));
                }
            };
        }
    }
}