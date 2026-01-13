using Newtonsoft.Json;
using Blueink.Client.Net.v2.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blueink.Client.Net.v2.Common
{
    public abstract class ClientServiceRequest<TResponse> : IClientServiceRequest<TResponse>
    {
        private readonly IClientService service;

        #region IClientServiceRequest Properties
        public abstract string MethodName { get; }
        public abstract string RestPath { get; }
        public abstract string HttpMethod { get; }

        // Can be multipartformdata , formdata, json, 
        public virtual string PayloadContentType { get; } = "json";
        public IClientService Service
        {
            get { return service; }
        }

        #endregion

        #region Pagination Properties
        public virtual int? CurrentPageNumber { get; private set; }
        public virtual int? TotalPagesCount { get; private set; }
        public virtual int? ResultsPerPage { get; private set; }
        public virtual int? TotalResultsCount { get; private set; }
        #endregion

        public ClientServiceRequest(IClientService service)
        {
            this.service = service;
        }

        public virtual TResponse Execute()
        {
            try
            {
                using(var response = ExecuteUnparsedAsync(CancellationToken.None).Result)
                {
                    // Bug out if no content 204 status code
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        return default(TResponse);

                    // if this request is a list then check the response headers for pagination
                    if (this.MethodName == "list")
                    {
                        // retrieve the pagination from the header
                        IEnumerable<string> headervalues;
                        if (response.Headers.TryGetValues("X-BlueInk-Pagination", out headervalues))
                        {
                            string pagination = headervalues.FirstOrDefault();
                            if (!String.IsNullOrWhiteSpace(pagination))
                            {
                                string[] values = pagination.Split(',');
                                if (values.Length == 4)
                                {
                                    try
                                    {
                                        this.CurrentPageNumber = Convert.ToInt32(values[0]);
                                        this.TotalPagesCount = Convert.ToInt32(values[1]);
                                        this.ResultsPerPage = Convert.ToInt32(values[2]);
                                        this.TotalResultsCount = Convert.ToInt32(values[3]);
                                    }
                                    catch (FormatException e)
                                    {
                                        throw new BlueinkApiException(Service.Name, "Failed to parse pagination from response header as FormatException [" + e.ToString() + "]", e);
                                    }
                                    catch (OverflowException e)
                                    {
                                        throw new BlueinkApiException(Service.Name, "Failed to parse pagination from response header as OverflowException [" + e.ToString() + "]", e);
                                    }
                                }
                            }
                        }
                    }

                    return ParsedResponse(response).Result;
                }
            }
            catch(NullReferenceException ex)
            {
                throw ex;
            }
            catch(AggregateException aex)
            {
                throw aex.InnerException;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #region Helper
        private async Task<HttpResponseMessage> ExecuteUnparsedAsync(CancellationToken token)
        {
            using (var request = CreateRequest())
            {                
                return await service.HttpClient.SendAsync(request,token).ConfigureAwait(false);
            }
        }

        private async Task<TResponse> ParsedResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return await service.DeserializeResponse<TResponse>(response).ConfigureAwait(false);
            }
            var error = await service.DeserializeError<Error>(response).ConfigureAwait(false);
            throw new BlueinkApiException(service.Name, error.ToString())
            {
                Error = error
            };
        }

        #endregion

        public virtual string BuildUriRequest()
        {
            return String.Empty;
        }

        public System.Net.Http.HttpRequestMessage CreateRequest()
        {
            Uri baseUri = new Uri(service.BaseUri);

            Uri uri = new Uri(baseUri, BuildUriRequest() );
            HttpContent content = null;
            switch (MethodName)
            {
                case "create":
                case "update":
                    {
                        switch (PayloadContentType)
                        {
                            case "formdata":
                                content = new MultipartFormDataContent();
                                BuildRequestContent((MultipartFormDataContent)content);
                                break;
                            case "urlencodedformdata": content = new LargeDataFormUrlEncodedContent(BuildRequestBody()); break;
                            case "json":
                            default: content = new StringContent(BuildJsonRequestBody(),Encoding.UTF8,"application/json"); break;
                        }                        
                    } break;           
                case "get":
                case "list": 
                case "delete":
                case "cancel":
                case "expire":
                case "remind":
                    {
                    } break;
            }

            var request = new HttpRequestMessage(new HttpMethod(HttpMethod), uri);
            request.Content = content;
            return request;
        }

        public virtual void BuildRequestContent(MultipartFormDataContent content)
        {
        }

        public virtual string BuildJsonRequestBody()
        {
            return String.Empty;
        }

        public virtual IEnumerable<KeyValuePair<string, string>> BuildRequestBody()
        {
            return null;
        }

    }
}
