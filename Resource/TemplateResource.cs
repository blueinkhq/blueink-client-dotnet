using Blueink.Client.Net.v2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueink.Client.Net.v2.Resource
{
    /// <summary>
    /// Provides access to Template-related API operations.
    /// Templates are reusable document and envelope configurations.
    /// </summary>
    public class TemplateResource
    {
        private readonly IClientService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateResource"/> class.
        /// </summary>
        /// <param name="service">The client service instance.</param>
        public TemplateResource(IClientService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Lists all envelope templates with default pagination (page 1, 50 per page).
        /// </summary>
        /// <returns>A request object that can be executed to retrieve envelope templates.</returns>
        public virtual ListEnvelopeTemplateRequest ListEnvelopeTemplate()
        {
            return new ListEnvelopeTemplateRequest(service);
        }

        /// <summary>
        /// Lists envelope templates with specified pagination.
        /// </summary>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="per_page">The number of results per page.</param>
        /// <returns>A request object that can be executed to retrieve envelope templates.</returns>
        public virtual ListEnvelopeTemplateRequest ListEnvelopeTemplate(
            int? page,
            int? per_page)
        {
            return new ListEnvelopeTemplateRequest(service, page, per_page);
        }

        public class ListEnvelopeTemplateRequest : BlueinkClientBaseService<IList<Blueink.Client.Net.v2.ResponseModel.EnvelopeDocumentTemplate>>
        {
            public ListEnvelopeTemplateRequest(IClientService service)
                : base(service)
            {
                this.Page = 1;
                this.PerPage = 50;
            }
            public ListEnvelopeTemplateRequest(
                IClientService service,
                int? page,
                int? per_page
                )
                : base(service)
            {
                this.Page = page;
                this.PerPage = per_page;
            }

            #region Pagination Support
            public virtual Nullable<int> Page { get; set; }
            public virtual Nullable<int> PerPage { get; set; }
            #endregion
            
            public override string BuildUriRequest()
            {
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                list.Add(new KeyValuePair<string, string>("page", this.Page.HasValue ? this.Page.Value.ToString() : "1"));
                list.Add(new KeyValuePair<string, string>("per_page", this.PerPage.HasValue ? this.PerPage.Value.ToString() : "50"));

                StringBuilder builder = new StringBuilder(RestPath);
                if (list.Count > 0)
                {
                    builder.Append(builder.ToString().Contains("?") ? "&" : "?");
                    builder.Append(String.Join("&", list.Select(x => String.IsNullOrEmpty(x.Value) ? Uri.EscapeDataString(x.Key) : String.Format("{0}={1}", Uri.EscapeDataString(x.Key), Uri.EscapeDataString(x.Value))).ToArray()));
                }

                return builder.ToString();
            }

            public override IEnumerable<KeyValuePair<string, string>> BuildRequestBody()
            {
                return base.BuildRequestBody();
            }

            public override string MethodName
            {
                get { return "list"; }
            }
            public override string RestPath
            {
                get { return "envelope-templates/"; }
            }

            public override string HttpMethod
            {
                get { return "get"; }
            }
        }

        public virtual GetEnvelopeTemplateRequest GetEnvelopeTemplate(string envelopeTemplateId)
        {
            return new GetEnvelopeTemplateRequest(service, envelopeTemplateId);
        }

        public class GetEnvelopeTemplateRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.EnvelopeDocumentTemplate>
        {
            public GetEnvelopeTemplateRequest(IClientService service, string envelopeTemplateId)
                : base(service)
            {
                EnvelopeTemplateId = envelopeTemplateId;
            }

            public virtual string EnvelopeTemplateId { get; private set; }
            
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
                get { return String.Format("envelope-templates/{0}/", EnvelopeTemplateId); }
            }

            public override string HttpMethod
            {
                get { return "get"; }
            }

        }


        public virtual ListTemplateRequest ListTemplate()
        {
            return new ListTemplateRequest(service);
        }

        public virtual ListTemplateRequest ListTemplate(
            int? page,
            int? per_page)
        {
            return new ListTemplateRequest(service, page, per_page);
        }

        public class ListTemplateRequest : BlueinkClientBaseService<IList<Blueink.Client.Net.v2.ResponseModel.DocumentTemplate>>
        {
            public ListTemplateRequest(IClientService service)
                : base(service)
            {
                this.Page = 1;
                this.PerPage = 50;
            }
            public ListTemplateRequest(
                IClientService service,
                int? page,
                int? per_page
                )
                : base(service)
            {
                this.Page = page;
                this.PerPage = per_page;
            }

            #region Pagination Support
            public virtual Nullable<int> Page { get; set; }
            public virtual Nullable<int> PerPage { get; set; }
            #endregion

            public override string BuildUriRequest()
            {
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                list.Add(new KeyValuePair<string, string>("page", this.Page.HasValue ? this.Page.Value.ToString() : "1"));
                list.Add(new KeyValuePair<string, string>("per_page", this.PerPage.HasValue ? this.PerPage.Value.ToString() : "50"));

                StringBuilder builder = new StringBuilder(RestPath);
                if (list.Count > 0)
                {
                    builder.Append(builder.ToString().Contains("?") ? "&" : "?");
                    builder.Append(String.Join("&", list.Select(x => String.IsNullOrEmpty(x.Value) ? Uri.EscapeDataString(x.Key) : String.Format("{0}={1}", Uri.EscapeDataString(x.Key), Uri.EscapeDataString(x.Value))).ToArray()));
                }

                return builder.ToString();
            }

            public override IEnumerable<KeyValuePair<string, string>> BuildRequestBody()
            {
                return base.BuildRequestBody();
            }

            public override string MethodName
            {
                get { return "list"; }
            }
            public override string RestPath
            {
                get { return "templates/"; }
            }

            public override string HttpMethod
            {
                get { return "get"; }
            }
        }

        public virtual GetTemplateRequest GetTemplate(string templateId)
        {
            return new GetTemplateRequest(service, templateId);
        }

        public class GetTemplateRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.DocumentTemplate>
        {
            public GetTemplateRequest(IClientService service, string templateId)
                : base(service)
            {
                TemplateId = templateId;
            }

            public virtual string TemplateId { get; private set; }

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
                get { return String.Format("templates/{0}/", TemplateId); }
            }

            public override string HttpMethod
            {
                get { return "get"; }
            }

        }
    }
}
