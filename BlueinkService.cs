using Blueink.Client.Net.v2.Common;
using Blueink.Client.Net.v2.Resource;

namespace Blueink.Client.Net.v2
{
    public class BlueinkService : BaseClientService
    {
        public BlueinkService(string apikey) : base(apikey)
        {
            bundleRes = new BundleResource(this);
            packetRes = new PacketResource(this);
            templateRes = new TemplateResource(this);
            personRes = new PersonResource(this);
            ratelimitRes = new RateLimitResource(this);
            webhookRes = new WebhookResource(this);
        }

        public override string Name
        {
            get { return "blueink"; }
        }

        public override string BaseUri
        {
            get { return "https://api.blueink.com/api/v2/"; }
        }

        public override string BasePath
        {
            get { return "/v2/"; }
        }

        private readonly BundleResource bundleRes;
        public virtual BundleResource BundleResource
        {
            get { return bundleRes; }
        }

        private readonly PacketResource packetRes;
        public virtual PacketResource PacketResource
        {
            get { return packetRes; }
        }

        private readonly TemplateResource templateRes;
        public virtual TemplateResource TemplateResource
        {
            get { return templateRes; }
        }

        private readonly PersonResource personRes;
        public virtual PersonResource PersonResource
        {
            get { return personRes; }
        }

        private readonly RateLimitResource ratelimitRes;
        public virtual RateLimitResource RateLimitResource
        {
            get { return ratelimitRes; }
        }

        private readonly WebhookResource webhookRes;
        public virtual WebhookResource WebhookResource
        {
            get { return webhookRes; }
        }
    }

    public abstract class BlueinkClientBaseService<TResponse> : ClientServiceRequest<TResponse>
    {
        protected BlueinkClientBaseService(IClientService service)
            : base(service)
        {
        }
    }

}
