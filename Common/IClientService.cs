using Blueink.Client.Net.v2.Serializer;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blueink.Client.Net.v2.Common
{
    /// <summary>
    /// Interface for Blueink client services providing HTTP communication capabilities.
    /// </summary>
    public interface IClientService : IDisposable
    {
        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the base URI for API requests.
        /// </summary>
        string BaseUri { get; }

        /// <summary>
        /// Gets the base path for API requests.
        /// </summary>
        string BasePath { get; }

        /// <summary>
        /// Gets the HTTP client used for making requests.
        /// </summary>
        HttpClient HttpClient { get; }

        /// <summary>
        /// Gets the serializer used for JSON serialization/deserialization.
        /// </summary>
        ISerializer Serializer { get; }

        /// <summary>
        /// Serializes an object to JSON string.
        /// </summary>
        /// <param name="data">The object to serialize.</param>
        /// <returns>JSON string representation of the object.</returns>
        string SerializeObject(object data);

        /// <summary>
        /// Deserializes the HTTP response content to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="response">The HTTP response message.</param>
        /// <returns>The deserialized object.</returns>
        Task<T> DeserializeResponse<T>(HttpResponseMessage response);

        /// <summary>
        /// Deserializes an error response to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="response">The HTTP response message.</param>
        /// <returns>The deserialized error object.</returns>
        Task<T> DeserializeError<T>(HttpResponseMessage response);
    }
}
