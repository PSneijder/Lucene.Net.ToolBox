using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Lucene.Net.Toolbox.Desktop.Behaviours
{
    sealed class ScrollOnTextChangeBehaviour
        : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.TextChanged += (sender, a) =>
            {
                AssociatedObject.ScrollToEnd();
            };

            base.OnAttached();
        }
    }
}