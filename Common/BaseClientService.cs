using Newtonsoft.Json;
using Blueink.Client.Net.v2.Model;
using Blueink.Client.Net.v2.Serializer;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Blueink.Client.Net.v2.Common
{
    public abstract class BaseClientService : IClientService
    {
        protected BaseClientService( string apikey)
        {
            this.ApiKey = apikey;
            this.Serializer = new NewtonsoftJsonSerializer();
            this.HttpClient = CreateHttpClient();
        }

        public string ApplicationName { get; private set; }
        public string ApiKey { get; private set; }      
        public ISerializer Serializer { get; private set; }

        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(this.BaseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", ApiKey);
            return client;
        }

        #region IClientService
        public HttpClient HttpClient { get; private set; }
        public abstract string Name { get; }
        public abstract string BaseUri { get; }
        public abstract string BasePath { get; }

        public string SerializeObject(object data)
        {
            return Serializer.Serialize(data);
        }

        public virtual async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {
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
                throw new BlueinkApiException(Name, "Failed to parse response from server as json [" + text + "]", e);
            }

            return result;
        }

        public virtual async Task<T> DeserializeError<T>(HttpResponseMessage response)
        {
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
            catch (Exception ex)
            {
                // exception will be thrown in case the response content is empty or it can't be deserialized to 
                // standard response (which contains data and error properties)
                throw new BlueinkApiException(Name,
                    "An Error occurred, but the error response could not be deserialized", ex);
            }

            return result;
        }

        public void Dispose()
        {
            if (HttpClient != null)
                HttpClient.Dispose();
        }

        #endregion
    }
}
