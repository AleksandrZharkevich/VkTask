using Newtonsoft.Json;

namespace VkTask.Utils.VkApi.Models
{
    internal class Comment
    {
        [JsonProperty("comment_id")]
        public int Id { get; set; }
    }
}
