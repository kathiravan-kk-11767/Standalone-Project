using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Zoho.Common.Util;
using Zoho.Components.Comments.Entities.Contract;
using Zoho.Components.Comments.Views.UWP.Util;

namespace SemSeparation
{
    public class CommentsSampleViewModel : ObservableObject
    {

        private int _HeaderCount;

        public int HeaderCount
        {
            get { return _HeaderCount; }
            set { _HeaderCount = value; OnPropertyChanged(); }
        }


        private ObservableCollection<CommentModel> _comments;

        /// <summary>
        /// Collection of comments to display
        /// </summary>
        public ObservableCollection<CommentModel> Comments
        {
            get => _comments;
            set => SetProperty(ref _comments, value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public CommentsSampleViewModel()
        {
            Comments = new ObservableCollection<CommentModel>();
            // Create sample comments
            //InitializeComments();
        }

        /// <summary>
        /// Initialize with sample data
        /// </summary>
        public void InitializeComments()
        {
            var allComments = new List<CommentModel>();

            // First, create all main comments
            for (int i = 1; i <= 50; i++)
            {
                var hasAttachments = i % 7 == 0; // Some comments have attachments
                var hasLikes = i % 5 == 0; // Some comments have likes

               

                var comment = new CommentModel
                {
                    Id = i.ToString(),
                    Content = $"This is comment number {i}. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    OwnerName = i % 2 == 0 ? "John Smith" : "Jane Doe",
                    OwnerId = i % 2 == 0 ? "user-1" : "user-2",
                    CommentedOn = DateTimeOffset.Now.AddMinutes(-i * 5),
                    IsLikedByCurrentUser = i % 3 == 0,
                    IsOwnedByCurrentUser = i % 5 == 0,
                    IsReply = false,
                    ParentCommentId = null,
                    HasAttachments = hasAttachments,
                    CanModify = i % 5 == 0,
                    PrivateRepliesAvailable = true,
                    Replies = new ObservableCollection<IComment>(),
                    HasReply = i <= 10 // First 10 comments have replies
                };

                allComments.Add(comment);

                // Add replies for first 10 comments
                if (i <= 10)
                {
                    var replyCount = i <= 5 ? 3 : 2; // First 5 have more replies
                    
                    for (int r = 1; r <= replyCount; r++)
                    {
                        var reply = new CommentModel
                        {
                            Id = $"{i}-r{r}",
                            Content = $"Reply {r} to comment {i}. This is a direct response to the main comment.",
                            OwnerName = r % 2 == 0 ? "ReplyUserA" : "ReplyUserB",
                            OwnerId = r % 2 == 0 ? "reply-user-a" : "reply-user-b",
                            CommentedOn = comment.CommentedOn.AddMinutes(r * 2),
                            LikesCount = r % 3,
                            IsLikedByCurrentUser = false,
                            IsOwnedByCurrentUser = r % 4 == 0,
                            IsReply = true,
                            ParentCommentId = comment.Id,
                            HasAttachments = false,
                            CanModify = r % 4 == 0,
                            PrivateRepliesAvailable = false,
                            Replies = new ObservableCollection<IComment>(),
                            IsLastReply = false,
                            HasReply = i <= 5 && r <= 2 // First 5 comments, first 2 replies have nested replies
                        };

                        allComments.Add(reply);

                        //// Add nested replies (replies to replies) for first 5 comments
                        //if (i <= 5 && r <= 2)
                        //{
                        //    var nestedReplyCount = 2;
                            
                        //    for (int nr = 1; nr <= nestedReplyCount; nr++)
                        //    {
                        //        var nestedReply = new CommentModel
                        //        {
                        //            Id = $"{i}-r{r}-nr{nr}",
                        //            Content = $"Nested reply {nr} to reply {r} of comment {i}. This is a reply to a reply.",
                        //            OwnerName = nr % 2 == 0 ? "NestedUserX" : "NestedUserY",
                        //            OwnerId = nr % 2 == 0 ? "nested-user-x" : "nested-user-y",
                        //            CommentedOn = reply.CommentedOn.AddMinutes(nr),
                        //            LikesCount = 0,
                        //            IsLikedByCurrentUser = false,
                        //            IsOwnedByCurrentUser = false,
                        //            IsReply = true,
                        //            ParentCommentId = reply.Id,
                        //            HasAttachments = false,
                        //            CanModify = false,
                        //            PrivateRepliesAvailable = false,
                        //            Replies = new ObservableCollection<IComment>(),
                        //            IsLastReply = nr == nestedReplyCount,
                        //            HasReply = false
                        //        };

                        //        allComments.Add(nestedReply);
                        //    }
                        //}

                        // Mark last reply
                        if (r == replyCount)
                        {
                            reply.IsLastReply = true;
                        }
                    }
                }
            }

            // Use the utility class to calculate levels and nested reply flags for proper thread line rendering
            CommentThreadingUtil.CalculateThreadingProperties(allComments);

            // Validate the threading structure
            if (!CommentThreadingUtil.ValidateThreadingStructure(allComments))
            {
                System.Diagnostics.Debug.WriteLine("Warning: Invalid threading structure detected in comments!");
            }

            // Add all comments to the observable collection (flattened structure)
            Comments = new ObservableCollection<CommentModel>(allComments);
        }

        /// <summary>
        /// Toggle like status for a comment
        /// </summary>
        public void ToggleLike(IComment comment)
        {
            //if (comment is CommentModel model)
            //{
            //    // Toggle like status
            //    model.IsLikedByCurrentUser = !model.IsLikedByCurrentUser;

            //    // Update like count
            //    if (model.IsLikedByCurrentUser)
            //    {
            //        model.LikesCount++;
            //        model.LikedBy.Add(new UserModel { Id = "current-user", Name = "You" });
            //    }
            //    else
            //    {
            //        model.LikesCount--;
            //        var currentUser = model.LikedBy.FirstOrDefault(u => u.Id == "current-user");
            //        if (currentUser != null)
            //        {
            //            model.LikedBy.Remove(currentUser);
            //        }
            //    }

            //    // Notify property changed
            //    model.NotifyPropertyChanged(nameof(model.LikesCount));
            //    model.NotifyPropertyChanged(nameof(model.IsLikedByCurrentUser));
            //    model.NotifyPropertyChanged(nameof(model.LikedBy));
            //}
        }

        /// <summary>
        /// Show users who liked a comment
        /// </summary>
       
        /// <summary>
        /// Show user profile
        /// </summary>
        public void ShowUserProfile(string userId)
        {
            // In a real app, this would navigate to the user profile or show a contact card
            System.Diagnostics.Debug.WriteLine($"Showing profile for user {userId}");
        }

       
        /// <summary>
        /// Start replying to a comment
        /// </summary>
       
        /// <summary>
        /// Adds a new main comment to the Comments collection
        /// </summary>
        public void AddComment(string content, string ownerName, string ownerId)
        {
            var newComment = new CommentModel
            {
                Id = Guid.NewGuid().ToString(),
                Content = content,
                OwnerName = ownerName,
                OwnerId = ownerId,
                CommentedOn = DateTimeOffset.Now,
                LikesCount = 0,
                IsLikedByCurrentUser = false,
                IsOwnedByCurrentUser = true,
                IsReply = false,
                ParentCommentId = null,
                HasAttachments = false,
                CanModify = true,
                PrivateRepliesAvailable = true,
                Replies = new ObservableCollection<IComment>(),
                IsLastReply = false,
                HasReply = false,
                Level = 0,
                HasNestedReply = false
            };

            Comments.Insert(0, newComment);
        }

        /// <summary>
        /// Add a reply to a specific comment
        /// </summary>
        public void AddReply(string parentCommentId, string content, string ownerName, string ownerId)
        {
            var newReply = new CommentModel
            {
                Id = Guid.NewGuid().ToString(),
                Content = content,
                OwnerName = ownerName,
                OwnerId = ownerId,
                CommentedOn = DateTimeOffset.Now,
                LikesCount = 0,
                IsLikedByCurrentUser = false,
                IsOwnedByCurrentUser = true,
                IsReply = true,
                ParentCommentId = parentCommentId,
                HasAttachments = false,
                CanModify = true,
                PrivateRepliesAvailable = false,
                Replies = new ObservableCollection<IComment>(),
                IsLastReply = false,
                HasReply = false
            };

            // Find the insertion point (after the parent comment and its existing replies)
            var parentIndex = Comments.ToList().FindIndex(c => c.Id == parentCommentId);
            if (parentIndex >= 0)
            {
                // Find the position after all existing replies to this parent
                int insertIndex = parentIndex + 1;
                while (insertIndex < Comments.Count && 
                       (Comments[insertIndex].ParentCommentId == parentCommentId ||
                        IsDescendantOf(Comments[insertIndex], parentCommentId, Comments.ToList())))
                {
                    insertIndex++;
                }

                Comments.Insert(insertIndex, newReply);

                // Recalculate threading properties for all comments
                CommentThreadingUtil.CalculateThreadingProperties(Comments.ToList());

                // Update HasReply flag for the parent comment
                var parentComment = Comments.FirstOrDefault(c => c.Id == parentCommentId);
                if (parentComment is CommentModel parent)
                {
                    parent.HasReply = true;
                }
            }
        }

        /// <summary>
        /// Check if a comment is a descendant of a parent comment
        /// </summary>
        private bool IsDescendantOf(CommentModel comment, string ancestorId, List<CommentModel> allComments)
        {
            var current = comment;
            while (!string.IsNullOrEmpty(current.ParentCommentId))
            {
                if (current.ParentCommentId == ancestorId)
                    return true;
                
                current = allComments.FirstOrDefault(c => c.Id == current.ParentCommentId);
                if (current == null)
                    break;
            }
            return false;
        }
    }
}
