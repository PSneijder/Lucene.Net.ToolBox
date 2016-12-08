using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using Lucene.Net.Toolbox.Utils.RichTextBox.Formatters;

namespace Lucene.Net.Toolbox.Utils.RichTextBox
{
    class EditableRichTextBox
        : System.Windows.Controls.RichTextBox
    {
        #region Fields

        private bool _preventDocumentUpdate;
        private bool _preventTextUpdate;

        #endregion

        #region Properties

        #region Text

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(System.Windows.Controls.RichTextBox), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextPropertyChanged, CoerceTextProperty, true, UpdateSourceTrigger.LostFocus));

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EditableRichTextBox)d).UpdateDocumentFromText();
        }

        private static object CoerceTextProperty(DependencyObject d, object value)
        {
            return value ?? "";
        }

        #endregion

        #region TextFormatter

        public static readonly DependencyProperty TextFormatterProperty = DependencyProperty.Register("TextFormatter", typeof(ITextFormatter), typeof(System.Windows.Controls.RichTextBox), new FrameworkPropertyMetadata(new PlainTextFormatter(), OnTextFormatterPropertyChanged));

        public ITextFormatter TextFormatter
        {
            get
            {
                return (ITextFormatter)GetValue(TextFormatterProperty);
            }
            set
            {
                SetValue(TextFormatterProperty, value);
            }
        }

        private static void OnTextFormatterPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EditableRichTextBox richTextBox = d as EditableRichTextBox;
            if (richTextBox != null)
                richTextBox.OnTextFormatterPropertyChanged((ITextFormatter)e.OldValue, (ITextFormatter)e.NewValue);
        }

        protected virtual void OnTextFormatterPropertyChanged(ITextFormatter oldValue, ITextFormatter newValue)
        {
            UpdateTextFromDocument();
        }

        #endregion 

        #endregion

        public EditableRichTextBox() { }

        public EditableRichTextBox(FlowDocument document)
            : base(document) { }

        #region Methods

        protected override void OnTextChanged(System.Windows.Controls.TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            UpdateTextFromDocument();
        }

        private void UpdateTextFromDocument()
        {
            if (_preventTextUpdate)
                return;

            _preventDocumentUpdate = true;
            Text = TextFormatter.GetText(Document);
            _preventDocumentUpdate = false;
        }

        private void UpdateDocumentFromText()
        {
            if (_preventDocumentUpdate)
                return;

            _preventTextUpdate = true;
            TextFormatter.SetText(Document, Text);
            _preventTextUpdate = false;
        }

        public void Clear()
        {
            Document.Blocks.Clear();
        }

        public override void BeginInit()
        {
            base.BeginInit();
            // Do not update anything while initializing. See EndInit
            _preventTextUpdate = true;
            _preventDocumentUpdate = true;
        }

        public override void EndInit()
        {
            base.EndInit();
            _preventTextUpdate = false;
            _preventDocumentUpdate = false;
            // Possible conflict here if the user specifies 
            // the document AND the text at the same time 
            // in XAML. Text has priority.
            if (!string.IsNullOrEmpty(Text))
                UpdateDocumentFromText();
            else
                UpdateTextFromDocument();
        }

        #endregion
    }
}