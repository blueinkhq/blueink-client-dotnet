using Blueink.Client.Net.v2.Common;
using System;

namespace Blueink.Client.Net.v2.Resource
{
    /// <summary>
    /// Provides access to the public PDF verification endpoint.
    /// Verifies that a document was signed through Blueink by its SHA256 hash.
    /// </summary>
    public class VerifyResource
    {
        private readonly IClientService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="VerifyResource"/> class.
        /// </summary>
        /// <param name="service">The client service instance.</param>
        public VerifyResource(IClientService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Verifies a document by its SHA256 hash.
        /// </summary>
        /// <param name="hash">The SHA256 hash of the document to verify.</param>
        /// <returns>A request object that can be executed to verify the document.</returns>
        public virtual VerifyRequest Verify(string hash)
        {
            if (String.IsNullOrWhiteSpace(hash))
                throw new ArgumentNullException("hash");

            return new VerifyRequest(service, new RequestModel.VerifyRequest { Hash = hash });
        }

        public class VerifyRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.VerifyResult>
        {
            public VerifyRequest(IClientService service, RequestModel.VerifyRequest request)
                : base(service)
            {
                Request = request;
            }

            public virtual RequestModel.VerifyRequest Request { get; set; }

            public override string BuildJsonRequestBody()
            {
                return Service.SerializeObject(Request);
            }

            public override string BuildUriRequest()
            {
                return RestPath;
            }

            public override string PayloadContentType
            {
                get { return "json"; }
            }

            public override string MethodName
            {
                get { return "create"; }
            }

            public override string RestPath
            {
                get { return "verify/"; }
            }

            public override string HttpMethod
            {
                get { return "post"; }
            }
        }
    }
}
