using Newtonsoft.Json;

namespace VkTask.Utils.VkApi.Models
{
    class ApiResponse<T>
    {
        [JsonProperty("response")]
        public T Response { get; set; }
    }
}
