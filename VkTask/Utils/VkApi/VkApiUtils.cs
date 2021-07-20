using Aquality.Selenium.Core.Logging;
using Newtonsoft.Json.Linq;
using RestApiTask.Utils.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using VkTask.Utils.DataManager;
using VkTask.Utils.VkApi.Models;

namespace VkTask.Utils.VkApi
{
    internal class VkApiUtils
    {
        private static readonly string _apiUrl = JsonDataReader.ReadProperty<string>(Constants.TestDataPath, "api_url");
        private static readonly string _apiVersion = JsonDataReader.ReadProperty<string>(Constants.TestDataPath, "api_version");
        private static readonly string _accessToken = JsonDataReader.ReadProperty<string>(Constants.TestDataPath, "access_token");

        public static int CreateNewPost(int ownerId, string message)
        {
            Logger.Instance.Info("Creating new post.");
            Dictionary<string, string> parameters = new()
            {
                { "owner_id", ownerId.ToString() },
                { "message", message },
                { "v", _apiVersion },
                { "access_token", _accessToken }
            };
            Request request = new(_apiUrl, JsonDataReader.ReadProperty<string>(Constants.TestDataPath, "wall_post"), parameters);
            Response<string> response = APIUtils.Get<string>(request);
            return JObject.Parse(response.Data).ToObject<ApiResponse<Post>>().Response.Id;
        }

        public static (int EditedId, string PhotoId) EditPost(int ownerId, int postId, string newMessage, string pathToPhoto)
        {
            Logger.Instance.Info("Editing post.");
            Dictionary<string, string> parameters = new()
            {
                { "owner_id", ownerId.ToString() },
                { "post_id", postId.ToString() },
                { "message", newMessage },
                { "v", _apiVersion },
                { "access_token", _accessToken }
            };
            string photoId = null;
            if (pathToPhoto != null)
            {
                string attach = UploadPhoto(pathToPhoto);
                parameters.Add("attachments", $"photo{attach}");
                photoId = attach;
            }
            Request request = new(_apiUrl, JsonDataReader.ReadProperty<string>(Constants.TestDataPath, "wall_edit"), parameters);
            Response<string> response = APIUtils.Get<string>(request);
            return (JObject.Parse(response.Data).ToObject<ApiResponse<int>>().Response, photoId);
        }

        private static string UploadPhoto(string pathToPhotoAttachment)
        {
            Logger.Instance.Info("Uploading photo.");
            Dictionary<string, string> parameters = new()
            {
                { "v", _apiVersion },
                { "access_token", _accessToken }
            };
            Request request = new(_apiUrl, JsonDataReader.ReadProperty<string>(Constants.TestDataPath, "wall_upload"), parameters);
            Response<string> response = APIUtils.Get<string>(request);
            UploadServer uploadServer = JObject.Parse(response.Data).ToObject<ApiResponse<UploadServer>>().Response;
            string uploadUrl = uploadServer.UploadUrl;
            int userId = uploadServer.UserId;
            request = new(uploadUrl, "", parameters);
            Response<string> stringResponse = APIUtils.Post<String>(request, pathToPhotoAttachment);
            UploadResponse uploadResponse = JObject.Parse(stringResponse.Content).ToObject<UploadResponse>();
            parameters.Add("server", uploadResponse.Server.ToString());
            parameters.Add("hash", uploadResponse.Hash);
            parameters.Add("photo", uploadResponse.Photo);
            parameters.Add("user_id", userId.ToString());
            request = new(_apiUrl, JsonDataReader.ReadProperty<string>(Constants.TestDataPath, "wall_savePhoto"), parameters);
            Response<string> newResponse = APIUtils.Get<string>(request);
            List<Photo> photoInfoList = JObject.Parse(newResponse.Content).ToObject<ApiResponse<List<Photo>>>().Response;
            return $"{photoInfoList.First().OwnerId}_{photoInfoList.First().Id}";
        }

        public static int AddCommentOnPost(int ownerId, int postId, string message)
        {
            Logger.Instance.Info("Adding comment on post.");
            Dictionary<string, string> parameters = new()
            {
                { "owner_id", ownerId.ToString() },
                { "post_id", postId.ToString() },
                { "message", message },
                { "v", _apiVersion },
                { "access_token", _accessToken }
            };
            Request request = new(_apiUrl, JsonDataReader.ReadProperty<string>(Constants.TestDataPath, "wall_create_comment"), parameters);
            Response<string> response = APIUtils.Get<string>(request);
            return JObject.Parse(response.Data).ToObject<ApiResponse<Comment>>().Response.Id;
        }

        public static bool IsLiked(int userId, LikedType type, int likedItemId, int ownerId)
        {
            Logger.Instance.Info($"Checking if {type} is liked.");
            Dictionary<string, string> parameters = new()
            {
                { "user_id", userId.ToString() },
                { "owner_id", ownerId.ToString() },
                { "type", type.ToString().ToLower() },
                { "item_id", likedItemId.ToString() },
                { "v", _apiVersion },
                { "access_token", _accessToken }
            };
            Request request = new(_apiUrl, JsonDataReader.ReadProperty<string>(Constants.TestDataPath, "likes_isLiked"), parameters);
            Response<string> response = APIUtils.Get<string>(request);
            return JObject.Parse(response.Data).ToObject<ApiResponse<Like>>().Response.Liked == 1;
        }

        public static int DeletePost(int ownerId, int postId)
        {
            Logger.Instance.Info("Deleting post.");
            Dictionary<string, string> parameters = new()
            {
                { "owner_id", ownerId.ToString() },
                { "post_id", postId.ToString() },
                { "v", _apiVersion },
                { "access_token", _accessToken }
            };
            Request request = new(_apiUrl, JsonDataReader.ReadProperty<string>(Constants.TestDataPath, "wall_delete"), parameters);
            Response<string> response = APIUtils.Get<string>(request);
            return JObject.Parse(response.Data).ToObject<ApiResponse<int>>().Response;
        }
    }
}
