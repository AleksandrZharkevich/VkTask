using Newtonsoft.Json;

namespace VkTask.Utils.VkApi.Models
{
    internal class UploadResponse
    {
        [JsonProperty("server")]
        public int Server { get; set; }
        [JsonProperty("photo")]
        public string Photo { get; set; }
        [JsonProperty("hash")]
        public string Hash { get; set; }
    }
}
