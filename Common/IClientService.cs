using Blueink.Client.Net.v2.Serializer;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blueink.Client.Net.v2.Common
{
    public interface IClientService : IDisposable
    {
        string Name { get; }
        string BaseUri { get; }
        string BasePath { get; }
        string ApiKey { get; }

        HttpClient HttpClient { get; }

        ISerializer Serializer { get; }

        string SerializeObject(object data);
        Task<T> DeserializeResponse<T>(HttpResponseMessage response);
        Task<T> DeserializeError<T>(HttpResponseMessage response);
    }
}
