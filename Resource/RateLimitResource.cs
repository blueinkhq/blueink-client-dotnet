using Blueink.Client.Net.v2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Blueink.Client.Net.v2.Resource
{
    public class RateLimitResource
    {
        private readonly IClientService service;

        public RateLimitResource(IClientService service)
        {
            this.service = service;
        }

        public virtual CheckRateLimitStatusRequest CheckRateLimitStatus()
        {
            return new CheckRateLimitStatusRequest(service);
        }

        public class CheckRateLimitStatusRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.RateLimit>
        {
            public CheckRateLimitStatusRequest(IClientService service)
                : base(service)
            {
            }

            public override void BuildRequestContent(MultipartFormDataContent content)
            {
                base.BuildRequestContent(content);
            }

            public override IEnumerable<KeyValuePair<string, string>> BuildRequestBody()
            {
                return base.BuildRequestBody();
            }

            public override string BuildUriRequest()
            {
                return RestPath;
            }

            public override string MethodName
            {
                get { return "get"; }
            }

            public override string RestPath
            {
                get { return "rate_limit/"; }
            }

            public override string HttpMethod
            {
                get { return "get"; }
            }
        }

    }
}
