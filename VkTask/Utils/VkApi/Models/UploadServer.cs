using Newtonsoft.Json;

namespace VkTask.Utils.VkApi.Models
{
    internal class UploadServer
    {
        [JsonProperty("album_id")]
        public int AlbumId { get; set; }
        [JsonProperty("upload_url")]
        public string UploadUrl { get; set; }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
    }
}
