using RestSharp;

namespace RestApiTask.Utils.Rest
{
    internal class APIUtils
    {
        public static Response<T> Get<T>(Request request)
        {
            var restClient = Connection.GetConnection(request.BaseUrl);
            var restRequest = request.ToRestRequest();
            restRequest.Method = Method.GET;
            var result = restClient.Execute<T>(restRequest);
            return new Response<T>().FromRestResult(result);
        }

        public static Response<T> Post<T>(Request request, T postBody)
        {
            var restClient = Connection.GetConnection(request.BaseUrl);
            var restRequest = request.ToRestRequest();
            if (request.DataFormat == RequestDataFormat.JSON)
            {
                restRequest.AddJsonBody(postBody);
            }
            else
            {
                restRequest.AddXmlBody(postBody);
            }
            var result = restClient.Post<T>(restRequest);
            return new Response<T>().FromRestResult(result);
        }

        public static Response<T> Post<T>(Request request, object partialBodyObject)
        {
            var restClient = Connection.GetConnection(request.BaseUrl);
            var restRequest = request.ToRestRequest();
            if (request.DataFormat == RequestDataFormat.JSON)
            {
                restRequest.AddJsonBody(partialBodyObject);
            }
            else
            {
                restRequest.AddXmlBody(partialBodyObject);
            }
            var result = restClient.Post<T>(restRequest);
            return new Response<T>().FromRestResult(result);
        }

        public static Response<T> Post<T>(Request request, string pathToFile)
        {
            var restClient = Connection.GetConnection(request.BaseUrl);
            var restRequest = request.ToRestRequest();
            restRequest.Method = Method.POST;
            restRequest.AddFile("photo", pathToFile, "multipart/form-data");
            var result = restClient.Execute<T>(restRequest);
            return new Response<T>().FromRestResult(result);
        }
    }
}
