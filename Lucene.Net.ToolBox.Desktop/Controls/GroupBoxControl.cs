using System.Windows;
using System.Windows.Controls;

namespace Lucene.Net.Toolbox.Desktop.Controls
{
    sealed class GroupBoxControl
        : ItemsControl
    {
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(GroupBoxControl), new PropertyMetadata(Orientation.Vertical));
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(string), typeof(GroupBoxControl), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty ShowSeperatorProperty = DependencyProperty.Register("ShowSeperator", typeof(bool), typeof(GroupBoxControl), new PropertyMetadata(false));
        
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public bool ShowSeperator
        {
            get { return (bool)GetValue(ShowSeperatorProperty); }
            set { SetValue(ShowSeperatorProperty, value); }
        }

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ContentControl();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return false;
        }
    }
}