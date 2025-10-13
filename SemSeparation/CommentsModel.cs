using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zoho.Common.Util;
using Zoho.Components.Comments.Entities.Contract;

namespace SemSeparation
{
    /// <summary>
    /// Implementation of IComment interface
    /// </summary>
    public class CommentModel : ObservableObject, IComment
    {
        private string _id;
        private string _content;
        private string _ownerName;
        private string _ownerId;
        private DateTimeOffset _createdOn;
        private int _likesCount;
        private bool _isLikedByCurrentUser;
        private bool _isOwnedByCurrentUser;
        private bool _isReply;
        private string _parentCommentId;
        private bool _hasAttachments;
        private ObservableCollection<IComment> _replies;
        private bool _canModify;
        private bool _privateRepliesAvailable;
        private bool _islastReply;
        private bool _hasReply;
        private int _level;
        private bool _hasNestedReply;

        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        public string OwnerName
        {
            get => _ownerName;
            set => SetProperty(ref _ownerName, value);
        }

        public string OwnerId
        {
            get => _ownerId;
            set => SetProperty(ref _ownerId, value);
        }

        public DateTimeOffset CommentedOn
        {
            get => _createdOn;
            set => SetProperty(ref _createdOn, value);
        }

        public int LikesCount
        {
            get => _likesCount;
            set => SetProperty(ref _likesCount, value);
        }

        public bool IsLikedByCurrentUser
        {
            get => _isLikedByCurrentUser;
            set => SetProperty(ref _isLikedByCurrentUser, value);
        }

        public bool IsOwnedByCurrentUser
        {
            get => _isOwnedByCurrentUser;
            set => SetProperty(ref _isOwnedByCurrentUser, value);
        }

        public bool IsReply
        {
            get => _isReply;
            set => SetProperty(ref _isReply, value);
        }

        public string ParentCommentId
        {
            get => _parentCommentId;
            set => SetProperty(ref _parentCommentId, value);
        }

        public bool HasAttachments
        {
            get => _hasAttachments;
            set => SetProperty(ref _hasAttachments, value);
        }

        public bool CanModify
        {
            get => _canModify;
            set => SetProperty(ref _canModify, value);
        }

        public bool PrivateRepliesAvailable
        {
            get => _privateRepliesAvailable;
            set => SetProperty(ref _privateRepliesAvailable, value);
        }

        public IEnumerable<IComment> Replies
        {
            get => _replies;
            set => SetProperty(ref _replies, new ObservableCollection<IComment>(value));
        }

        public bool IsLastReply
        {
            get => _islastReply;
            set => SetProperty(ref _islastReply, value);
        }

        public bool HasReply
        {
            get => _hasReply;
            set => SetProperty(ref _hasReply, value);
        }

        /// <summary>
        /// Gets or sets the nesting level of the comment. 0 = root comment, 1 = first level reply, 2 = second level reply, etc.
        /// </summary>
        public int Level
        {
            get => _level;
            set => SetProperty(ref _level, value);
        }

        /// <summary>
        /// Gets or sets whether this comment has nested replies that require thread line continuation
        /// </summary>
        public bool HasNestedReply
        {
            get => _hasNestedReply;
            set => SetProperty(ref _hasNestedReply, value);
        }

        public string CommentId => Id;

        public string CommentOwnerZUID => OwnerId;

        public string CommentOwnerName => OwnerName;

        public string CommentContent => Content;

        public string OwnerZUID => "743880918";

        public string ParentCommentOwnerName => "KaidenBlake";

        public string ParentCommentOwnerZUID => ParentCommentId;

        public CommentModel()
        {
            _replies = new ObservableCollection<IComment>();
        }
    }

}
