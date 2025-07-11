using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Zoho.UWP.AppSuite.Resources
{
    public sealed class ZCommandBarResource : DependencyObject
    {
        public CommandBarDefaultLabelPosition DefaultLabelPosition
        {
            get { return (CommandBarDefaultLabelPosition)GetValue(DefaultLabelPositionProperty); }
            set { SetValue(DefaultLabelPositionProperty, value); }
        }

        public static readonly DependencyProperty DefaultLabelPositionProperty =
            DependencyProperty.Register("DefaultLabelPosition", typeof(CommandBarDefaultLabelPosition), typeof(ZCommandBarResource),
                new PropertyMetadata(CommandBarDefaultLabelPosition.Collapsed));
    }
}
