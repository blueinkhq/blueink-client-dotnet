using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Blueink.Client.Net.v2.Common;
using Blueink.Client.Net.v2.Helper;
using Blueink.Client.Net.v2.Model;

namespace Blueink.Client.Net.v2.Resource
{
    public class PacketResource
    {
        private readonly IClientService service;

        public PacketResource(IClientService service)
        {
            this.service = service;
        }

        public virtual GetRequest RetrieveCOE(string packetId)
        {
            return new GetRequest(service, packetId);
        }

        public class GetRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.Packet>
        {
            public GetRequest(IClientService service, string packetId)
                : base(service)
            {
                PacketId = packetId;
            }

            public virtual string PacketId { get; private set; }

            public override void BuildRequestContent(MultipartFormDataContent content)
            {
                base.BuildRequestContent(content);
            }
            public override string BuildJsonRequestBody()
            {
                return base.BuildJsonRequestBody();
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
                get { return String.Format("packets/{0}/", PacketId); }
            }

            public override string HttpMethod
            {
                get { return "get"; }
            }
        }

        public virtual PartialUpdateRequest UpdatePacket(string packetId)
        {
            return new PartialUpdateRequest(service, packetId);
        }
        public virtual PartialUpdateRequest UpdatePacket(string packetId,RequestModel.Packet packet)
        {
            return new PartialUpdateRequest(service, packetId, packet);
        }

        public class PartialUpdateRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.Packet>
        {
            public PartialUpdateRequest(IClientService service, string packetId)
                : base(service)
            {
                PacketId = packetId;
            }

            public PartialUpdateRequest(IClientService service,
                string packetId,
                RequestModel.Packet packet)
                : base(service)
            {
                PacketId = packetId;
                Packet = packet;            
            }

            public virtual string PacketId { get; private set; }
            public virtual RequestModel.Packet Packet { get; set; }
            public override IEnumerable<KeyValuePair<string, string>> BuildRequestBody()
            {                
                return base.BuildRequestBody();
            }
            public override string BuildJsonRequestBody()
            {
                return Service.SerializeObject(Packet);
            }

            public override string BuildUriRequest()
            {
                return RestPath;
            }

            public override string MethodName
            {
                get { return "update"; }
            }

            public override string RestPath
            {
                get { return String.Format("packets/{0}/", PacketId); }
            }

            public override string HttpMethod
            {
                get { return "patch"; }
            }

        }

        public virtual CreateEmbededSigningRequest CreateEmbeddedSigningUrl(string packetId)
        {
            return new CreateEmbededSigningRequest(service, packetId);
        }

        public class CreateEmbededSigningRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.EmbededSigning>
        {
            public CreateEmbededSigningRequest(IClientService service,string packetId)
                : base(service)
            {
                PacketId = packetId;
            }

            public virtual string PacketId
            {
                get;
                set;
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
                get { return "create"; }
            }

            public override string RestPath
            {
                get { return String.Format("packets/{0}/embed_url/",this.PacketId); }
            }

            public override string HttpMethod
            {
                get { return "post"; }
            }
        }

        public virtual SendReminderRequest SendReminder(string packetId)
        {
            return new SendReminderRequest(service, packetId);
        }

        public class SendReminderRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.Packet>
        {
            public SendReminderRequest(IClientService service, string packetId)
                : base(service)
            {
                PacketId = packetId;
            }

            public virtual string PacketId { get; private set; }
            public override void BuildRequestContent(MultipartFormDataContent content)
            {
                base.BuildRequestContent(content);
            }
            public override string BuildJsonRequestBody()
            {
                return base.BuildJsonRequestBody();
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
                get { return "update"; }
            }

            public override string RestPath
            {
                get { return String.Format("packets/{0}/remind/", PacketId); }
            }

            public override string HttpMethod
            {
                get { return "put"; }
            }

        }

        public virtual GetCertificateOfEvidenceRequest RetrieveCertificateOfEvidence(string packetId)
        {
            return new GetCertificateOfEvidenceRequest(service, packetId);
        }

        public class GetCertificateOfEvidenceRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.PacketCertificateOfEvidence>
        {
            public GetCertificateOfEvidenceRequest(IClientService service, string packetId)
                : base(service)
            {
                PacketId = packetId;
            }

            public virtual string PacketId { get; private set; }

            public override void BuildRequestContent(MultipartFormDataContent content)
            {
                base.BuildRequestContent(content);
            }
            public override string BuildJsonRequestBody()
            {
                return base.BuildJsonRequestBody();
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
                get { return String.Format("packets/{0}/coe/", PacketId); }
            }

            public override string HttpMethod
            {
                get { return "get"; }
            }

        }
    }
}
