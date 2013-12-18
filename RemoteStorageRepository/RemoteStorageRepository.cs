using Ninject_FakeItEasyDemo.Infrastructure;
using RestSharp;

namespace RemoteStorage
{
    /// <summary>
    /// Używa RestSharp do requestów REST
    /// </summary>
    public class RemoteStorageRepository : IStorage
    {
        private readonly RestClient _client;

        public RemoteStorageRepository(string url)
        {
            _client = new RestClient(url);
        }

        public void Set(string name, string value)
        {
            var request = new RestRequest(Method.POST);
            request.AddParameter("Name", name);
            request.AddParameter("Value", value);
            _client.Execute(request);
        }

        public string Get(string name)
        {
            var request = new RestRequest(Method.GET);
            request.AddParameter("Name", name);
            return _client.Execute(request).Content;
        }
    }
}
