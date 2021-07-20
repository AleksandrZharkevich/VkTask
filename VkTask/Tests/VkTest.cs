using NUnit.Framework;
using VkTask.Utils.DataManager;

namespace VkTask.Tests
{
    public class VkTest : BaseTest
    {
        [Test]
        public void VkWallTest()
        {
            AppContext.StartVkThenAuthorizeAndGoToProfilePage();
            int userId = JsonDataReader.ReadProperty<int>(Constants.TestDataPath, "user_id");
            string message = AppContext.GenerateRandomString();
            int postId = AppContext.CreateNewPostOnTheWall(message);
            Assert.True(AppContext.CheckPostIsDisplayed(postId), "Post isn't displayed");
            Assert.AreEqual(userId, AppContext.GetPostAuthorId(postId), "Post author_id doesn't match");
            Assert.AreEqual(message, AppContext.GetPostText(postId), "Post's text doesn't match");

            message = AppContext.GenerateRandomString();
            var (_, PhotoId) = AppContext.EditPostOnTheWall(postId, message, Constants.PhotoPath);
            Assert.AreEqual(message, AppContext.GetEditedPostText(postId), "Edited post's text doesn't match");
            Assert.AreEqual(PhotoId, AppContext.GetPostImageId(postId), "Image doesn't match");

            message = AppContext.GenerateRandomString();
            int commentId = AppContext.CommentPostOnTheWall(postId, message);

            Assert.True(AppContext.CheckCommentOnPostIsDisplayed(postId, commentId), "Comment isn't displayed");
            Assert.AreEqual(userId, AppContext.GetCommentAuthorId(postId, commentId), "Comment author_id doesn't match");
            Assert.AreEqual(message, AppContext.GetCommentText(postId, commentId), "Comment's text doesn't match");

            AppContext.LikePost(postId);
            bool isPostLiked = AppContext.CheckPostIsLiked(postId);
            Assert.True(isPostLiked);

            AppContext.DeletePostOnTheWall(postId);
            Assert.True(AppContext.CheckPostIsDeleted(postId), "Post isn't deleted");
        }
    }
}