using System;
using System.Collections.Generic;
using System.Linq;

namespace SemSeparation
{
    /// <summary>
    /// Utility class for calculating comment threading properties
    /// </summary>
    public static class CommentThreadingUtil
    {
        /// <summary>
        /// Calculates threading properties (Level and HasNestedReply) for a flat list of comments
        /// </summary>
        /// <param name="allComments">Flat list of all comments including main comments and replies</param>
        public static void CalculateThreadingProperties(List<CommentModel> allComments)
        {
            if (allComments == null || !allComments.Any())
                return;

            // First pass: Calculate levels for all comments
            foreach (var comment in allComments)
            {
                comment.Level = CalculateCommentLevel(comment, allComments);
            }

            // Second pass: Calculate HasNestedReply for each comment
            foreach (var comment in allComments)
            {
                comment.HasNestedReply = CalculateHasNestedReply(comment, allComments);
            }

            // Third pass: Update IsLastReply flags
            UpdateLastReplyFlags(allComments);
        }

        /// <summary>
        /// Validates that the threading structure is correct
        /// </summary>
        /// <param name="allComments">List of comments to validate</param>
        /// <returns>True if structure is valid, false otherwise</returns>
        public static bool ValidateThreadingStructure(List<CommentModel> allComments)
        {
            if (allComments == null)
                return false;

            foreach (var comment in allComments)
            {
                // Validate parent-child relationships
                if (!string.IsNullOrEmpty(comment.ParentCommentId))
                {
                    var parent = allComments.FirstOrDefault(c => c.Id == comment.ParentCommentId);
                    if (parent == null)
                    {
                        // Parent comment not found
                        return false;
                    }

                    // Check for circular references
                    if (HasCircularReference(comment, allComments))
                    {
                        return false;
                    }

                    // Validate that reply comes after its parent in the list
                    var parentIndex = allComments.IndexOf(parent);
                    var commentIndex = allComments.IndexOf(comment);
                    if (commentIndex <= parentIndex)
                    {
                        // Reply should come after parent
                        return false;
                    }
                }
                else
                {
                    // Root comment should not be marked as reply
                    if (comment.IsReply)
                    {
                        return false;
                    }
                }

                // Validate Level property
                var expectedLevel = CalculateCommentLevel(comment, allComments);
                if (comment.Level != expectedLevel)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Calculates the nesting level for a comment
        /// </summary>
        /// <param name="comment">The comment to calculate level for</param>
        /// <param name="allComments">All comments in the thread</param>
        /// <returns>The nesting level (0 for root comments, 1+ for replies)</returns>
        private static int CalculateCommentLevel(CommentModel comment, List<CommentModel> allComments)
        {
            if (string.IsNullOrEmpty(comment.ParentCommentId))
            {
                return 0; // Root comment
            }

            var parent = allComments.FirstOrDefault(c => c.Id == comment.ParentCommentId);
            if (parent == null)
            {
                return 0; // Orphaned comment, treat as root
            }

            return CalculateCommentLevel(parent, allComments) + 1;
        }

        /// <summary>
        /// Determines if a comment has nested replies (replies to its replies)
        /// </summary>
        /// <param name="comment">The comment to check</param>
        /// <param name="allComments">All comments in the thread</param>
        /// <returns>True if the comment has nested replies</returns>
        private static bool CalculateHasNestedReply(CommentModel comment, List<CommentModel> allComments)
        {
            // Get direct replies to this comment
            var directReplies = allComments.Where(c => c.ParentCommentId == comment.Id).ToList();
            
            // Check if any direct reply has its own replies
            foreach (var reply in directReplies)
            {
                var nestedReplies = allComments.Where(c => c.ParentCommentId == reply.Id).ToList();
                if (nestedReplies.Any())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Updates the IsLastReply flag for all replies
        /// </summary>
        /// <param name="allComments">All comments in the thread</param>
        private static void UpdateLastReplyFlags(List<CommentModel> allComments)
        {
            // Reset all IsLastReply flags
            foreach (var comment in allComments)
            {
                comment.IsLastReply = false;
            }

            // Group replies by their parent
            var replyGroups = allComments
                .Where(c => !string.IsNullOrEmpty(c.ParentCommentId))
                .GroupBy(c => c.ParentCommentId);

            foreach (var group in replyGroups)
            {
                var repliesForParent = group.OrderBy(c => allComments.IndexOf(c)).ToList();
                if (repliesForParent.Any())
                {
                    // Mark the last reply in the sequence as last
                    var lastReply = repliesForParent.Last();
                    lastReply.IsLastReply = true;
                }
            }
        }

        /// <summary>
        /// Checks if there's a circular reference in the comment hierarchy
        /// </summary>
        /// <param name="comment">The comment to check</param>
        /// <param name="allComments">All comments in the thread</param>
        /// <returns>True if circular reference exists</returns>
        private static bool HasCircularReference(CommentModel comment, List<CommentModel> allComments)
        {
            var visited = new HashSet<string>();
            var current = comment;

            while (current != null && !string.IsNullOrEmpty(current.ParentCommentId))
            {
                if (visited.Contains(current.Id))
                {
                    return true; // Circular reference detected
                }

                visited.Add(current.Id);
                current = allComments.FirstOrDefault(c => c.Id == current.ParentCommentId);
            }

            return false;
        }
    }
}