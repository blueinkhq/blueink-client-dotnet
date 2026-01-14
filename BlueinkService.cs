using Blueink.Client.Net.v2.Common;
using Blueink.Client.Net.v2.Resource;
using System;

namespace Blueink.Client.Net.v2
{
    /// <summary>
    /// Main entry point for the Blueink API client.
    /// Provides access to all Blueink API resources including Bundles, Packets, Persons, Templates, and Webhooks.
    /// </summary>
    /// <example>
    /// <code>
    /// // Using environment variable (recommended)
    /// // Set BLUEINK_API_KEY environment variable before running
    /// using (var client = new BlueinkService())
    /// {
    ///     var bundles = client.BundleResource.List().Execute();
    /// }
    ///
    /// // Using explicit API key
    /// using (var client = new BlueinkService("your-api-key"))
    /// {
    ///     var bundles = client.BundleResource.List().Execute();
    /// }
    /// </code>
    /// </example>
    public class BlueinkService : BaseClientService
    {
        private static readonly string DefaultBaseUri = "https://api.blueink.com/api/v2/";
        private readonly string _baseUri;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlueinkService"/> class.
        /// The API key will be read from the BLUEINK_API_KEY environment variable.
        /// </summary>
        /// <exception cref="BlueinkConfigurationException">
        /// Thrown when the BLUEINK_API_KEY environment variable is not set.
        /// </exception>
        public BlueinkService() : this(null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlueinkService"/> class with an explicit API key.
        /// </summary>
        /// <param name="apiKey">
        /// The API key for authentication. If null or empty, the BLUEINK_API_KEY environment variable will be used.
        /// </param>
        /// <exception cref="BlueinkConfigurationException">
        /// Thrown when no API key is provided and BLUEINK_API_KEY environment variable is not set.
        /// </exception>
        public BlueinkService(string apiKey) : this(apiKey, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlueinkService"/> class with explicit API key and base URL.
        /// </summary>
        /// <param name="apiKey">
        /// The API key for authentication. If null or empty, the BLUEINK_API_KEY environment variable will be used.
        /// </param>
        /// <param name="baseUrl">
        /// The base URL for the API. If null or empty, uses the default production URL or BLUEINK_API_URL environment variable.
        /// </param>
        /// <exception cref="BlueinkConfigurationException">
        /// Thrown when no API key is provided and BLUEINK_API_KEY environment variable is not set.
        /// </exception>
        public BlueinkService(string apiKey, string baseUrl) : base(apiKey)
        {
            _baseUri = ResolveBaseUri(baseUrl);

            bundleRes = new BundleResource(this);
            packetRes = new PacketResource(this);
            templateRes = new TemplateResource(this);
            personRes = new PersonResource(this);
            ratelimitRes = new RateLimitResource(this);
            webhookRes = new WebhookResource(this);
        }

        /// <summary>
        /// Resolves the base URI from the provided value or environment variable.
        /// </summary>
        private string ResolveBaseUri(string baseUrl)
        {
            if (!String.IsNullOrWhiteSpace(baseUrl))
            {
                // Ensure URL ends with /
                return baseUrl.EndsWith("/") ? baseUrl : baseUrl + "/";
            }

            string envUrl = Environment.GetEnvironmentVariable(BaseUrlEnvironmentVariable);
            if (!String.IsNullOrWhiteSpace(envUrl))
            {
                return envUrl.EndsWith("/") ? envUrl : envUrl + "/";
            }

            return DefaultBaseUri;
        }

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        public override string Name
        {
            get { return "blueink"; }
        }

        /// <summary>
        /// Gets the base URI for API requests.
        /// </summary>
        public override string BaseUri
        {
            get { return _baseUri ?? DefaultBaseUri; }
        }

        /// <summary>
        /// Gets the base path for API requests.
        /// </summary>
        public override string BasePath
        {
            get { return "/v2/"; }
        }

        private readonly BundleResource bundleRes;
        /// <summary>
        /// Gets the Bundle resource for managing document bundles.
        /// </summary>
        public virtual BundleResource BundleResource
        {
            get { return bundleRes; }
        }

        private readonly PacketResource packetRes;
        /// <summary>
        /// Gets the Packet resource for managing signing packets.
        /// </summary>
        public virtual PacketResource PacketResource
        {
            get { return packetRes; }
        }

        private readonly TemplateResource templateRes;
        /// <summary>
        /// Gets the Template resource for managing document and envelope templates.
        /// </summary>
        public virtual TemplateResource TemplateResource
        {
            get { return templateRes; }
        }

        private readonly PersonResource personRes;
        /// <summary>
        /// Gets the Person resource for managing persons/contacts.
        /// </summary>
        public virtual PersonResource PersonResource
        {
            get { return personRes; }
        }

        private readonly RateLimitResource ratelimitRes;
        /// <summary>
        /// Gets the RateLimit resource for checking API rate limit status.
        /// </summary>
        public virtual RateLimitResource RateLimitResource
        {
            get { return ratelimitRes; }
        }

        private readonly WebhookResource webhookRes;
        /// <summary>
        /// Gets the Webhook resource for managing webhooks.
        /// </summary>
        public virtual WebhookResource WebhookResource
        {
            get { return webhookRes; }
        }
    }

    /// <summary>
    /// Base class for Blueink client service requests.
    /// </summary>
    /// <typeparam name="TResponse">The type of response expected from the request.</typeparam>
    public abstract class BlueinkClientBaseService<TResponse> : ClientServiceRequest<TResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlueinkClientBaseService{TResponse}"/> class.
        /// </summary>
        /// <param name="service">The client service instance.</param>
        protected BlueinkClientBaseService(IClientService service)
            : base(service)
        {
        }
    }
}
