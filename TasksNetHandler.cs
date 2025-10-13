using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoho.Common.DI;
using Zoho.Common.Util;
using Zoho.Common.Web;
using Zoho.Logging;
using Zoho.Logging.Logger;
using Zoho.Network.Adapter.Contract;
using Zoho.UWP.Common.DI;
using Zoho.UWP.Tasks;

namespace Zoho.UWP
{
    public class TasksNetHandler : ITasksNetHandler
    {
        public const string DProxyMail = ZohoUrl.DProxy + "/zm"; //No I18N

        private static readonly ILogger Logger = LogManager.GetLogger();

        private readonly INetworkAdapter NetworkAdapter;

        public TasksNetHandler(INetworkAdapter networkAdapter)
        {
            NetworkAdapter = networkAdapter;
            NetworkAdapter.SetNetworkInspectHeaderForServiceName(ZServiceName.ToDo.ToString());
        }

      
        public async Task<string> FetchTagsFromServerAsync(string ownerZuid)
        {

            try
            {
                return (string)await NetworkAdapter.PostAsync(TasksApiUrl.DProxyMail, ApiParams.GetLabelDetails, null, addAuthHeader: true, zuid: ownerZuid);
            }

           
            catch (Exception ex)
            {
                throw;
            }

        }
    }

    public static class TasksApiUrl
    {

        public const string TasksAPI = ZohoUrl.ZMail + "/StreamAPI.do"; //NO I18N
        public const string TasksCustomFieldAPI = ZohoUrl.ZMail + "/customFieldsAPI.do"; //NO I18N

        public const string DownloadAttachAPI = ZohoUrl.ZMail + "/FileDownloadForMobile";//NO I18N
        public const string DownloadAttachAPIBase = "/zm/FileDownload";//NO I18N

        public const string DProxyStreams = ZohoUrl.DProxy + "/zstreams"; //No I18N
        public const string DProxyMail = ZohoUrl.DProxy + "/zm"; //No I18N
        public const string TasksViewAPI = ZohoUrl.ZMail + "/taskViewAPI.do"; //NO I18N
        public const string TasksGetAPI = ZohoUrl.ZohoMail + "/zm/taskGetAPI.do"; //NO I18N // Used For Fetching Task Category
        public const string TasksListGetAPI = ZohoUrl.ZMail + "/taskGetAPI.do"; //NO I18N
        public const string TasksFetchListAPI = "/taskGetAPI.do"; //NO I18N
        public const string TasksActionAPI = ZohoUrl.ZMail + "/taskActionAPI.do"; //NO I18N
        public const string TasksPostAPI = ZohoUrl.ZMail + "/PostAPI.do"; //NO I18N
        public const string StreamsGroups = "zmail.zoho.com/api/streams/groups/";

        private static HashSet<string> StaticSecuredAPIDomains = new HashSet<string>() {
          ZohoUrl.DProxy, ZohoUrl.ZohoMail, ZohoUrl.ZMailWebSocket, ZohoUrl.ZMailBase};

        public static HashSet<string> GetWhitelistedAPIDomains()
        {
            return StaticSecuredAPIDomains;
        }
    }

    public static class ApiParams
    {
        public const string TAction = "taction"; // NO I18N
        public const string Action = "action"; // NO I18N
        public const string Groups = "groups"; // NO I18N
        public const string GetGroups = "getGroups"; //NO I18N
        public const string GetGroupDetails = "getGroupDetails"; //NO I18N
        public const string GetTaskByStatus = "getGroupTasks"; //NO I18N

        public const string GetAgendaCount = "getAgendaCount"; //NO I18N
        public const string GetGroupCount = "getGroupCount"; //NO I18N
        public const string GetAssigneeCount = "getAssigneeCount"; //NO I18N
        public const string GetCategoryCount = "getCategoryCount"; //NO I18N
        public const string GetDueDateViewCount = "getDueDateViewCount"; //NO I18N
        public const string GetPriorityCount = "getPriorityCount"; //NO I18N
        public const string GetStatusCount = "getStatusCount"; //NO I18N
        public const string GetTagCount = "getTagCount"; //NO I18N

        public const string GetCategories = "getCategories"; //NO I18N
        public const string GetGroupCategories = "getGrpCategories"; //NO I18N
        public const string SaveCategoryPreference = "saveCategoryPreference"; //NO I18N

        public const string GetLabelDetails = "getLabelDetails"; //NO I18N

        public const string GetSharedTasks = "getSharedTasks"; //NO I18N
        public const string GetStarredTasks = "getAllFavTasks"; //NO I18N
        public const string GetTasksByTag = "getTagTasks"; //NO I18N
        public const string AddTasks = "addTaskSubtask"; //NO I18N
        public const string EditTask = "syncTask"; //NO I18N
        public const string ActionTypeAddEntity = "addEntity";//NO I18N
        public const string ActionTypeAddAttach = "addAttach";//NO I18N
        public const string ActionStreamsViewType = "Tasks";//NO I18N
        public const string ProcessType = "form";//NO I18N
        public const string DeleteAttachMode = "delAttach";//NO I18N
        public const string GetTasksByCategory = "getTasksByCategory"; //NO I18N
        public const string GetTasksByLabel = "getTasksByLabel"; //No I18N        
        public const string GetTasksAssociatedMail = "getTasksAssociatedMail"; //No I18N        

        public const string GetTasksByPriority = "getTasksByPriority";
        public const string GetTasksByStatus = "getTasksByStatus"; //No I18N
        public const string CreatedByMe = "createdbyme"; // No I18N
        public const string GetTasksSortedByDueDate = "getTasksSortedByDueDate"; //No I18N  
        public const string GetTasksSortedByModifiedDate = "getTasksSortedByModifiedDate"; //No I18N  
        public const string GetTasksByDueDate = "getTasksByDueDate"; //No I18N
        public const string MyTasks = "mytasks"; // No I18N
        public const string PersonalTasks = "personaltasks"; // No I18N
        public const string AgendaView = "agendaview"; // No I18N
        public const string GetGroupTasks = "getGroupTasks"; // No I18N
        public const string GetSubTasks = "getSubTasks"; // No I18N
        public const string GetPrivateTasks = "getPrivateTasks"; // No I18N
        public const string GetArchivedTasks = "getArchivedTasks"; // No I18N
        public const string GetTasksByCustomFieldValue = "getTasksByCustomFieldValue"; // No I18N

        public const string Mode = "mode"; //NO I18N
        public const string AttachId = "attachId"; //NO I18N
        public const string EntityType = "entityType"; //NO I18N
        public const string ActionType = "actionType"; //NO I18N
        public const string taction = "taction"; //NO I18N
        public const string action = "action"; //NO I18N
        public const string GroupId = "groupId"; //NO I18N
        public const string AttachType = "attachType"; //NO I18N
        public const string StreamsView = "streamsView"; //NO I18N
        public const string EntityId = "entityId"; //NO I18N

        public const string DownloadAttachment = "downloadAttachment"; //NO I18N

        public const string GetCategoryForLHS = "getCategoryForLHS"; //NO I18N

        public const string GetAllCustomStatus = "getAllCustomStatus"; //NO I18N
        public const string AddCustomStatus = "addCustomStatus"; //NO I18N
        public const string EditCustomStatus = "editCustomStatus"; //NO I18N
        public const string DeleteCustomStatus = "deleteCustomStatus"; //NO I18N
        public const string StatusName = "statusName"; //NO I18N
        public const string StatusColour = "statusColour"; //NO I18N

        public const string GetCF = "getCF"; // NO I18N
        public const string ChangeEntity = "changeEntity"; //NO I18N
        public const string ChangeCustomField = "updateCFVal"; //NO I18N
        public const string ChangeAssignees = "changeAssignees"; //NO I18N
        public const string SetDueDate = "setDueDate"; //NO I18N
        public const string ChangePriority = "changePriority"; //NO I18N
        public const string SetStartDate = "setStartDate"; //NO I18N
        public const string ChangeStatus = "changeStatus"; //NO I18N
        public const string ChangeTitle = "changeTitle"; //NO I18N
        public const string AddLabel = "addLabel"; //NO I18N
        public const string ChangeSummary = "changeSummary"; //NO I18N
        public const string ChangeGroup = "copyTask"; //NO I18N
        public const string CopyTask = "copyTask"; //NO I18N
        public const string ChangeCategory = "changeCategory"; //NO I18N
        public const string AddAttachment = "addAttachment"; //NO I18N
        public const string RemoveAttachment = "removeAttachment"; //NO I18N
        public const string SetReminder = "setReminder"; //NO I18N
        public const string ExportTask = "exportTask"; //NO I18N
        public const string ImportTask = "importTask"; //NO I18N
        public const string DeleteTask = "deleteTask"; //NO I18N
        public const string RestoreTask = "restoreTask"; //NO I18N
        public const string ArchiveTask = "archiveTask"; //NO I18N
        public const string ReorderSubtasks = "reorderSubtasks"; //NO I18N
        public const string UnArchiveTask = "unArchiveTask"; //NO I18N
        public const string LikeTask = "like"; //No I18N
        public const string UnLikeTask = "unlike"; //No I18N



        public const string GetCommentsList = "getCommentsList"; //NO I18N
        public const string ViewGroupEntity = "viewGroupEntity"; //No I18N
        public const string ViewSelfData = "viewSelfData"; //No I18N
        public const string AddComment = "addComment"; //No I18N
        public const string DeleteComment = "deleteComment"; //NO I18N


        public const string LikeComment = "handleCommentLike"; //No I18N
        public const string LikeStreamComment = "likecomment"; //No I18N
        public const string UnLikeStreamComment = "unlikecomment"; //No I18N
        public const string AddCategory = "addCategory"; //No I18N
        public const string EditCategory = "editCategory"; //No I18N
        public const string DeleteCategory = "deleteCategory"; //No I18N
        public const string TaskAndNotesMigratedUser = "isTaskAndNotesMigratedUser"; //No I18N
        public const string GetMigrationStatus = "getMigrationStatus"; //No I18N

        public const string AccessRequest = "accessRequest"; //NO I18N
        public const string DeleteLabel = "deleteLabel"; //NO I18N
        public const string CreateLabel = "createLabel"; //NO I18N
        public const string MailLabelAPI = "MailLabelAPI"; //NO I18N
        public const string RenameLabel = "renameLabel";//NO I18N

        public const string GetCustomStatusForInvitee = "getCustomStatusForInvitee";//NO I18N;
        public const string GetCFForInvitee = "getCFForInvitee";//NO I18N;
        public const string GetCategoryForInvitee = "getCategoryForInvitee";//NO I18N;

        public const string GetUserGroupSettingsPreference = "getUserGroupSettingsPreference"; //NO I18N
        public const string GetUserViewSettingsPreference = "getUserViewSettingsPreference"; //NO I18N
    }

}
