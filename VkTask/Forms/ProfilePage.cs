using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using VkTask.Utils.DataManager;

namespace VkTask.Forms
{
    public class ProfilePage : Form
    {
        public ILabel Post(int wallOwnerId, int postId, string elementName) => ElementFactory.GetLabel(By.Id($"post{wallOwnerId}_{postId}"), elementName);
        public ILabel EditedTestLabel(int wallOwnerId, int postId, string elementName) => ElementFactory.GetLabel(By.CssSelector($"#wpt{wallOwnerId}_{postId}[style]"), elementName);
        public IButton LikeBtn(int wallOwnerId, int postId, string elementName) => ElementFactory.GetButton(By.CssSelector($"._like_wall{wallOwnerId}_{postId} .like"), elementName);
        public ILabel Comment(int wallOwnerId, int commentedPostId, int commentId, string elementName) => ElementFactory.GetLabel(By.CssSelector($"#replies{wallOwnerId}_{commentedPostId} #post{wallOwnerId}_{commentId}"), elementName);

        public ProfilePage() : base(By.Id("profile_wall"), "Profile wall")
        { }

        public bool IsPostDisplayed(int postId) => Post(JsonDataReader.ReadProperty<int>(Constants.TestDataPath, "user_id"), postId, "Post").State.WaitForDisplayed();

        public bool IsPostDeleted(int postId) => Post(JsonDataReader.ReadProperty<int>(Constants.TestDataPath, "user_id"), postId, "Post").State.WaitForNotDisplayed();

        public bool IsCommentOnPostDisplayed(int commentedPostId, int commentId) => Comment(JsonDataReader.ReadProperty<int>(Constants.TestDataPath, "user_id"), commentedPostId, commentId, "Comment").State.WaitForDisplayed();

        public void LikePost(int postId) => LikeBtn(JsonDataReader.ReadProperty<int>(Constants.TestDataPath, "user_id"), postId, "Like").Click();

        public int GetPostAuthorId(int wallOwnerId, int postId)
        {
            string href = Post(wallOwnerId, postId, "Post").FindChildElement<ILink>(By.CssSelector("a.author"), "Post author link").Href;
            return int.Parse(href[(href.LastIndexOf("/id") + "/id".Length)..]);
        }

        public string GetPostImageId(int wallOwnerId, int postId) => Post(wallOwnerId, postId, "Post").FindChildElement<ILink>(By.CssSelector("a[href*='photo']"), "POst image link").GetAttribute("data-photo-id");

        public string GetTextOfPost(int wallOwnerId, int postId) => Post(wallOwnerId, postId, "Post")
            .FindChildElement<ILabel>(By.ClassName("wall_post_text"), "Post's text").Text;

        public string GetEditedTextOfPost(int wallOwnerId, int postId)
        {
            EditedTestLabel(wallOwnerId, postId, "").State.WaitForDisplayed();
            return GetTextOfPost(wallOwnerId, postId);
        }

        public ProfilePage ShowNextPostComment(int wallOwnerId, int postId)
        {
            ILink nextComment = Post(wallOwnerId, postId, "Post").FindChildElement<ILink>(By.ClassName("replies_next"), "Show next comment link");
            AqualityServices.ConditionalWait.WaitFor(() => nextComment.State.IsDisplayed);
            nextComment.Click();
            return this;
        }

        public double GetCommentAuthorId(int wallOwnerId, int commentedPostId, int commentId)
        {
            string href = Comment(wallOwnerId, commentedPostId, commentId, "Comment").FindChildElement<ILink>(By.CssSelector("a.author"), "Comment author link").Href;
            return int.Parse(href[(href.LastIndexOf("/id") + "/id".Length)..]);
        }

        public string GetTextOfComment(int wallOwnerId, int commentedPostId, int commentId) => Comment(wallOwnerId, commentedPostId, commentId, "Comment")
            .FindChildElement<ILabel>(By.ClassName("wall_reply_text"), "Comment's text").Text;
    }
}
