using Newtonsoft.Json;

namespace VkTask.Utils.VkApi.Models
{
    internal class Photo
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("album_id")]
        public int AlbumId { get; set; }
        [JsonProperty("owner_id")]
        public int OwnerId { get; set; }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("date")]
        public int Date { get; set; }
        [JsonProperty("sizes")]
        public PhotoSize[] Sizes { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }

    }
}
