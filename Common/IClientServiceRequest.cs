using System.Collections.Generic;
using System.Net.Http;

namespace Blueink.Client.Net.v2.Common
{
    public interface IClientServiceRequest
    {
        string PayloadContentType { get; }
        string MethodName { get; }
        string HttpMethod { get; }
        string RestPath { get; }
        IClientService Service { get; }
        string BuildUriRequest();
        string BuildJsonRequestBody();
        IEnumerable<KeyValuePair<string, string>> BuildRequestBody();
        void BuildRequestContent(MultipartFormDataContent context);
        HttpRequestMessage CreateRequest();
    }

    public interface IClientServiceRequest<TResponse> : IClientServiceRequest
    {
        TResponse Execute();

        int? CurrentPageNumber { get; }
        int? TotalPagesCount { get; }
        int? ResultsPerPage { get; }
        int? TotalResultsCount { get; }
    }
}
