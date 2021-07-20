using RestSharp;
using System.Collections.Generic;

namespace RestApiTask.Utils.Rest
{
    internal class Request
    {
        public string BaseUrl { get; set; }
        public string Resource { get; set; }
        public RequestDataFormat DataFormat { get; set; }
        public IDictionary<string, string> Parameters { get; set; }

        public Request(string baseUrl, string resource, RequestDataFormat dataFormat, Dictionary<string, string> parameters)
        {
            this.BaseUrl = baseUrl;
            this.Resource = resource;
            this.DataFormat = dataFormat;
            this.Parameters = parameters;
        }

        public Request(string baseUrl, string resource, Dictionary<string, string> parameters)
            : this(baseUrl, resource, RequestDataFormat.JSON, parameters)
        {
        }

        public Request(string baseUrl, string resource, RequestDataFormat dataFormat)
            : this(baseUrl, resource, dataFormat, null)
        {
            BaseUrl = baseUrl;
            Resource = resource;
            DataFormat = dataFormat;
        }

        public Request(string baseUrl, string resource)
        : this(baseUrl, resource, RequestDataFormat.JSON)
        { }

        public Request(string baseUrl)
            : this(baseUrl, "", RequestDataFormat.JSON)
        {
        }

        public RestRequest ToRestRequest()
        {
            string resource = this.Resource;
            DataFormat dataFormat = this.DataFormat switch
            {
                RequestDataFormat.JSON => RestSharp.DataFormat.Json,
                RequestDataFormat.XML => RestSharp.DataFormat.Xml,
                _ => RestSharp.DataFormat.None
            };
            RestRequest restRequest = new(resource, dataFormat);
            if (Parameters != null)
            {
                foreach (var item in Parameters)
                {
                    restRequest.AddQueryParameter(item.Key, item.Value, false);
                }
            }
            return restRequest;
        }
    }
}
