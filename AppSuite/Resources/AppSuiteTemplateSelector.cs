using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Zoho.UWP.Common.Theme.Model;
using Zoho.UWP.Components.Theme.Model;

namespace Zoho.UWP.AppSuite.Resources
{
    /// <summary><see cref="DataTemplateSelector"/> for app theme selector view</summary>
    public class ThemeTemplateSelector : DataTemplateSelector
    {
        /// <summary>Default accent color template selector</summary>
        public DataTemplate AccentColorSelectorTemplate { get; set; }
        /// <summary>Template selector for custom color theme</summary>
        public DataTemplate CustomColorThemeTemplate { get; set; }
        /// <summary>Template selector for Windows color theme</summary>
        public DataTemplate WindowsColorThemeTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            ZAccentColorVObj themeBObj = item as ZAccentColorVObj;
            //switch (themeBObj.Theme)
            //{
            //    case ThemeMode.CustomColor:
            //return CustomColorThemeTemplate;
            //    case ThemeMode.WindowsColor:
            //        return WindowsColorThemeTemplate;
            //    default:
            return AccentColorSelectorTemplate;
            //}
        }
    }
}