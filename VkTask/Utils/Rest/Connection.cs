using RestSharp;

namespace RestApiTask.Utils.Rest
{
    internal class Connection
    {
        private static RestClient _client;

        public static RestClient GetConnection(string baseUrl)
        {
            if (_client == null || _client.BaseUrl.AbsoluteUri != baseUrl)
            {
                Init(baseUrl);
            }
            return _client;
        }

        static void Init(string url)
        {
            _client = new RestClient(url);
        }
    }
}
