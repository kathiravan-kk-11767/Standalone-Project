using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Zoho.Common.Util;
using Zoho.Streams.Collaboration.Entities.Util;
using Zoho.UWP.Components.Theme;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Zoho.UWP.ZStreamsCollaboration
{
    public sealed partial class StreamsControl : UserControl
    {
        private ZThemeUtil ZThemeUtil;
        public string GroupId = "60045561864";
        public string EntityId = "1757850537602115600";
        public StreamEntityType StreamEntityType = StreamEntityType.Tasks;

        public StreamsControl()
        {
            this.InitializeComponent();
        }

        private void GetPrivateComments()
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GroupId = "60045561864";
            EntityId = "1757850537602115600";
            StreamEntityType = StreamEntityType.Tasks;

            CommentControl.GroupId = GroupId;
            CommentControl.EntityId = EntityId;
            CommentControl.EntityType = StreamEntityType;
            CommentControl.IsEntityAssociatedWithStreamGroup = true;
            CommentControl.OwnerZUID = UserUtil.CurrentUser.ZUID;
            CommentControl.EntityOwnerZUID = UserUtil.CurrentUser.ZUID;

            CommentControl.GetComments(5);

        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            GroupId = "60045561864";
            EntityId = "1757850697665115600";
            StreamEntityType = StreamEntityType.Tasks;


            CommentControl.GroupId = GroupId;
            CommentControl.EntityId = EntityId;
            CommentControl.EntityType = StreamEntityType;
            CommentControl.OwnerZUID = UserUtil.CurrentUser.ZUID;
            CommentControl.IsEntityAssociatedWithStreamGroup = true;
            CommentControl.EntityOwnerZUID = "60040402253";

            CommentControl.GetComments(5);
        }
    }
}
