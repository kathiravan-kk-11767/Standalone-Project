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
using Zoho.Common.Util;
using Zoho.Logging;
using Zoho.Tasks.Library.Domain;
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

            {
                TasksFolder.OwnerZUID = UserUtil.CurrentUser.ZUID;
                TasksFolder.GetFolders();
            }
            

            //CommentViewModel.InitializeComments();

            //GetTaskFoldersRequest getTaskFoldersRequest = new GetTaskFoldersRequest(RequestType.LocalAndNetwork,UserUtil.CurrentUser.ZUID, true,true,true, UserUtil.CurrentUser.ZUID, default);
            //GetTaskFolders getTaskFolders = new GetTaskFolders(getTaskFoldersRequest, default);
            //getTaskFolders.Execute();

            //TasksFolderPicker.OwnerZUID = UserUtil.CurrentUser.ZUID;
            //TasksFolderPicker.GetFolders();

        }

        private void TextBlock_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            TasksFolderPicker.OwnerZUID = UserUtil.CurrentUser.ZUID;
            TasksFolderPicker.DisplayGroups = true;
            TasksFolderPicker.DisplayCategories = true;
            TasksFolderPicker.DisplayTags = false;
            TasksFolderPicker.GetFolders();
        }

        private void TextBlock_Tapped_2(object sender, TappedRoutedEventArgs e)
        {
            TasksFolderPicker.OwnerZUID = UserUtil.CurrentUser.ZUID;
            TasksFolderPicker.DisplayGroups = true;
            TasksFolderPicker.DisplayCategories = false;
            TasksFolderPicker.DisplayTags = false;
            TasksFolderPicker.GroupId = default;
            TasksFolderPicker.GetFolders();
        }

        private void TextBlock_Tapped_3(object sender, TappedRoutedEventArgs e)
        {
            TasksFolderPicker.OwnerZUID = UserUtil.CurrentUser.ZUID;
            TasksFolderPicker.DisplayGroups = false;
            TasksFolderPicker.DisplayCategories = true;
            TasksFolderPicker.GroupId = GroupIdTextBox.Text;
            TasksFolderPicker.DisplayTags = false;
            TasksFolderPicker.GetFolders();
        }

        private void TextBlock_Tapped_4(object sender, TappedRoutedEventArgs e)
        {
            TasksFolderPicker.OwnerZUID = UserUtil.CurrentUser.ZUID;
            TasksFolderPicker.DisplayGroups = false;
            TasksFolderPicker.DisplayCategories = false;
            TasksFolderPicker.DisplayTags = true;
            TasksFolderPicker.GroupId = default;
            TasksFolderPicker.GetFolders();
        }
    }
}
