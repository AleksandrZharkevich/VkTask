using Newtonsoft.Json;

namespace VkTask.Utils.VkApi.Models
{
    internal class PhotoSize
    {
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
