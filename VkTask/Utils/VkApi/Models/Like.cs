using Newtonsoft.Json;

namespace VkTask.Utils.VkApi.Models
{
    internal class Like
    {
        [JsonProperty("liked")]
        public int Liked { get; set; }
        [JsonProperty("reposted")]
        public int Reposted { get; set; }
    }
}
