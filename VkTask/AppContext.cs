using Aquality.Selenium.Browsers;
using VkTask.Forms;
using VkTask.Utils.DataManager;
using VkTask.Utils.RandomData;
using VkTask.Utils.VkApi;
using VkTask.Utils.VkApi.Models;

namespace VkTask
{
    public class AppContext
    {
        private static ProfilePage _profilePage = new();
        private static int _wallOwnerId = JsonDataReader.ReadProperty<int>(Constants.TestDataPath, "user_id");

        public static void StartVkThenAuthorizeAndGoToProfilePage()
        {
            AqualityServices.Browser.GoTo(JsonDataReader.ReadProperty<string>(Constants.TestDataPath, "start_url"));
            AqualityServices.Browser.WaitForPageToLoad();
            (new AuthorizationForm()).Authorize(JsonDataReader.ReadProperty<string>(Constants.TestDataPath, "account_email"),
                JsonDataReader.ReadProperty<string>(Constants.TestDataPath, "account_password"));
            (new LeftMenuForm()).OpenProfilePage();
        }

        public static bool CheckPostIsDisplayed(int postId) => _profilePage.IsPostDisplayed(postId);

        public static bool CheckPostIsDeleted(int postId) => _profilePage.IsPostDeleted(postId);

        public static void LikePost(int postId) => _profilePage.LikePost(postId);

        public static int GetPostAuthorId(int postId) => _profilePage.GetPostAuthorId(_wallOwnerId, postId);

        public static string GetPostText(int postId) => _profilePage.GetTextOfPost(_wallOwnerId, postId);

        public static string GetPostImageId(int postId) => _profilePage.GetPostImageId(_wallOwnerId, postId);

        public static string GetEditedPostText(int postId) => _profilePage.GetEditedTextOfPost(_wallOwnerId, postId);

        public static int CreateNewPostOnTheWall(string message) => VkApiUtils.CreateNewPost(_wallOwnerId, message);

        public static double GetCommentAuthorId(int commentedPostId, int commentId) => _profilePage.GetCommentAuthorId(_wallOwnerId, commentedPostId, commentId);

        public static string GetCommentText(int commentedPostId, int commentId) => _profilePage.GetTextOfComment(_wallOwnerId, commentedPostId, commentId);

        public static bool CheckCommentOnPostIsDisplayed(int commentedPostId, int commentId) => _profilePage.ShowNextPostComment(_wallOwnerId, commentedPostId).IsCommentOnPostDisplayed(commentedPostId, commentId);

        public static (int EditedId, string PhotoId) EditPostOnTheWall(int postId, string editedMessage, string pathToPhoto = null) => VkApiUtils.EditPost(_wallOwnerId, postId, editedMessage, pathToPhoto);

        public static int CommentPostOnTheWall(int postId, string editedMessage) => VkApiUtils.AddCommentOnPost(_wallOwnerId, postId, editedMessage);

        public static bool CheckPostIsLiked(int postId) => VkApiUtils.IsLiked(_wallOwnerId, LikedType.Post, postId, _wallOwnerId);

        public static int DeletePostOnTheWall(int postId) => VkApiUtils.DeletePost(_wallOwnerId, postId);

        public static string GenerateRandomString() => RandomDataGenerator.RandomString(15);
    }
}
