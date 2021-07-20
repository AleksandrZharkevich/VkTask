using Newtonsoft.Json;

namespace VkTask.Utils.VkApi.Models
{
    internal class Post
    {
        [JsonProperty("post_id")]
        public int Id { get; set; }
    }
}
