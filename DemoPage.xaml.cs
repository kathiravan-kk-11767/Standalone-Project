using SemSeparation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Zoho.Logging;
using Zoho.UWP.Components.Theme;
using Zoho.UWP.Components.Theme.Model;
using Zoho.UWP.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Zoho.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DemoPage : Page
    {

        public CommentsSampleViewModel CommentViewModel { get; } = new CommentsSampleViewModel();

        public ZThemeFont ZThemeFont = ZThemeUtil.GetAppThemeFont();
        public DemoPage()
        {
            this.InitializeComponent();
        }

        private async void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {

            CommentViewModel.InitializeComments();

            //ITasksNetHandler tasksNetHandler = (ITasksNetHandler)TasksDIServiceProvider.Instance.GetService(typeof(ITasksNetHandler));

            //var data = await tasksNetHandler.FetchTagsFromServerAsync("743880918");
        }


    }
}
