using RestSharp;
using System.Net;

namespace RestApiTask.Utils.Rest
{
    internal class Response<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; set; }
        public string ContentEncoding { get; set; }
        public string Content { get; set; }
        public long ContentLength { get; set; }
        public T Data { get; set; }

        public Response<T> FromRestResult(IRestResponse<T> restResponse)
        {
            Response<T> response = new Response<T>();
            if (restResponse.Data != null)
            {
                response.Data = restResponse.Data;
            }
            response.ContentType = restResponse.ContentType;
            response.ContentLength = restResponse.ContentLength;
            response.ContentEncoding = restResponse.ContentEncoding;
            response.Content = restResponse.Content;
            response.StatusCode = restResponse.StatusCode;
            return response;
        }
    }
}