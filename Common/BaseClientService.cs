using Newtonsoft.Json;
using Blueink.Client.Net.v2.Model;
using Blueink.Client.Net.v2.Serializer;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Blueink.Client.Net.v2.Common
{
    /// <summary>
    /// Base class for Blueink client services providing HTTP communication capabilities.
    /// Uses a singleton HttpClient pattern to prevent socket exhaustion.
    /// </summary>
    public abstract class BaseClientService : IClientService
    {
        /// <summary>
        /// Environment variable name for the Blueink API key.
        /// </summary>
        public const string ApiKeyEnvironmentVariable = "BLUEINK_API_KEY";

        /// <summary>
        /// Environment variable name for a custom Blueink API base URL.
        /// </summary>
        public const string BaseUrlEnvironmentVariable = "BLUEINK_API_URL";

        private static readonly object _httpClientLock = new object();
        private static HttpClient _sharedHttpClient;
        private static string _currentApiKey;
        private static string _currentBaseUri;
        private readonly string _apiKey;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseClientService"/> class.
        /// </summary>
        /// <param name="apiKey">
        /// The API key for authentication. If null or empty, attempts to read from
        /// the BLUEINK_API_KEY environment variable.
        /// </param>
        /// <exception cref="BlueinkConfigurationException">
        /// Thrown when no API key is provided and none is found in environment variables.
        /// </exception>
        protected BaseClientService(string apiKey = null)
        {
            _apiKey = ResolveApiKey(apiKey);
            ValidateApiKey(_apiKey);

            this.Serializer = new NewtonsoftJsonSerializer();
            this.HttpClient = GetOrCreateHttpClient();
        }

        /// <summary>
        /// Gets the application name.
        /// </summary>
        public string ApplicationName { get; private set; }

        /// <summary>
        /// Gets the serializer used for JSON serialization/deserialization.
        /// </summary>
        public ISerializer Serializer { get; private set; }

        /// <summary>
        /// Resolves the API key from the provided value or environment variable.
        /// </summary>
        /// <param name="apiKey">The explicitly provided API key, or null to use environment variable.</param>
        /// <returns>The resolved API key.</returns>
        /// <exception cref="BlueinkConfigurationException">Thrown when no API key can be resolved.</exception>
        private string ResolveApiKey(string apiKey)
        {
            // If API key is provided, use it
            if (!String.IsNullOrWhiteSpace(apiKey))
            {
                return apiKey;
            }

            // Try to get from environment variable
            string envApiKey = Environment.GetEnvironmentVariable(ApiKeyEnvironmentVariable);
            if (!String.IsNullOrWhiteSpace(envApiKey))
            {
                return envApiKey;
            }

            throw new BlueinkConfigurationException(
                $"No API key provided. Either pass an API key to the constructor or set the {ApiKeyEnvironmentVariable} environment variable.");
        }

        /// <summary>
        /// Validates the API key format.
        /// </summary>
        /// <param name="apiKey">The API key to validate.</param>
        /// <exception cref="BlueinkConfigurationException">Thrown when the API key is invalid.</exception>
        private void ValidateApiKey(string apiKey)
        {
            if (String.IsNullOrWhiteSpace(apiKey))
            {
                throw new BlueinkConfigurationException("API key cannot be null or empty.");
            }

            // Blueink API keys are typically 80 characters (hex string)
            if (apiKey.Length < 32)
            {
                throw new BlueinkConfigurationException(
                    "API key appears to be invalid. Blueink API keys are typically 80 characters long.");
            }

            // Check for common mistakes - API key should be alphanumeric
            foreach (char c in apiKey)
            {
                if (!Char.IsLetterOrDigit(c))
                {
                    throw new BlueinkConfigurationException(
                        "API key contains invalid characters. API keys should only contain letters and numbers.");
                }
            }
        }

        /// <summary>
        /// Gets or creates the shared HttpClient instance.
        /// Uses a singleton pattern to prevent socket exhaustion.
        /// </summary>
        /// <returns>The shared HttpClient instance.</returns>
        private HttpClient GetOrCreateHttpClient()
        {
            // Check if we need to create a new client (first time or credentials changed)
            lock (_httpClientLock)
            {
                if (_sharedHttpClient == null ||
                    _currentApiKey != _apiKey ||
                    _currentBaseUri != this.BaseUri)
                {
                    // Dispose old client if it exists and credentials changed
                    if (_sharedHttpClient != null && (_currentApiKey != _apiKey || _currentBaseUri != this.BaseUri))
                    {
                        _sharedHttpClient.Dispose();
                    }

                    _sharedHttpClient = CreateHttpClient();
                    _currentApiKey = _apiKey;
                    _currentBaseUri = this.BaseUri;
                }
            }
            return _sharedHttpClient;
        }

        /// <summary>
        /// Creates a new HttpClient instance with the appropriate configuration.
        /// </summary>
        /// <returns>A configured HttpClient instance.</returns>
        private HttpClient CreateHttpClient()
        {
            var handler = new HttpClientHandler
            {
                // Enable automatic decompression for better performance
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            };

            var client = new HttpClient(handler);
            client.BaseAddress = new Uri(this.BaseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", _apiKey);

            // Set reasonable timeout
            client.Timeout = TimeSpan.FromSeconds(100);

            return client;
        }

        #region IClientService

        /// <summary>
        /// Gets the HTTP client used for making requests.
        /// </summary>
        public HttpClient HttpClient { get; private set; }

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the base URI for API requests.
        /// </summary>
        public abstract string BaseUri { get; }

        /// <summary>
        /// Gets the base path for API requests.
        /// </summary>
        public abstract string BasePath { get; }

        /// <summary>
        /// Serializes an object to JSON string.
        /// </summary>
        /// <param name="data">The object to serialize.</param>
        /// <returns>JSON string representation of the object.</returns>
        /// <exception cref="ArgumentNullException">Thrown when data is null.</exception>
        public string SerializeObject(object data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "Cannot serialize null object.");
            }
            return Serializer.Serialize(data);
        }

        /// <summary>
        /// Deserializes the HTTP response content to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="response">The HTTP response message.</param>
        /// <returns>The deserialized object.</returns>
        /// <exception cref="ArgumentNullException">Thrown when response is null.</exception>
        /// <exception cref="BlueinkApiException">Thrown when deserialization fails.</exception>
        public virtual async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response), "HTTP response cannot be null.");
            }

            var text = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (Type.Equals(typeof(T), typeof(string)))
            {
                return (T)(object)text;
            }

            T result = default(T);
            try
            {
                result = Serializer.Deserialize<T>(text);
            }
            catch (JsonReaderException e)
            {
                throw new BlueinkApiException(Name, "Failed to parse response from server as JSON [" + text + "]", e);
            }
            catch (JsonSerializationException e)
            {
                throw new BlueinkApiException(Name, "Failed to deserialize response from server [" + text + "]", e);
            }

            return result;
        }

        /// <summary>
        /// Deserializes an error response to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="response">The HTTP response message.</param>
        /// <returns>The deserialized error object.</returns>
        /// <exception cref="ArgumentNullException">Thrown when response is null.</exception>
        /// <exception cref="BlueinkApiException">Thrown when error deserialization fails.</exception>
        public virtual async Task<T> DeserializeError<T>(HttpResponseMessage response)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response), "HTTP response cannot be null.");
            }

            var text = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (Type.Equals(typeof(Error), typeof(string)))
            {
                return (T)(object)text;
            }
            T result = default(T);
            try
            {
                result = Serializer.Deserialize<T>(text);
            }
            catch (JsonReaderException ex)
            {
                throw new BlueinkApiException(Name,
                    $"An error occurred (HTTP {(int)response.StatusCode}), but the error response could not be parsed as JSON: {text}", ex);
            }
            catch (JsonSerializationException ex)
            {
                throw new BlueinkApiException(Name,
                    $"An error occurred (HTTP {(int)response.StatusCode}), but the error response could not be deserialized: {text}", ex);
            }
            catch (Exception ex)
            {
                throw new BlueinkApiException(Name,
                    $"An error occurred (HTTP {(int)response.StatusCode}), but the error response could not be processed", ex);
            }

            return result;
        }

        /// <summary>
        /// Disposes the service resources.
        /// Note: The shared HttpClient is intentionally NOT disposed to maintain the singleton pattern.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the service resources.
        /// </summary>
        /// <param name="disposing">True if disposing managed resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Note: We intentionally do NOT dispose the shared HttpClient here
                    // as it's designed to be reused across the application lifetime.
                    // The HttpClient will be disposed when the application shuts down.
                }
                _disposed = true;
            }
        }

        #endregion
    }
}
