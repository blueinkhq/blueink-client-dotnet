using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Blueink.Client.Net.v2.Common;
using Blueink.Client.Net.v2.Helper;
using Blueink.Client.Net.v2.Model;
using Blueink.Client.Net.v2.ResponseModel;

namespace Blueink.Client.Net.v2.Resource
{
    public class WebhookResource
    {
        private const string Resource = "webhooks";

        private readonly IClientService service;

        public WebhookResource(IClientService service)
        {
            this.service = service;
        }

        public virtual ListRequest List()
        {
            return new ListRequest(service);
        }

        public virtual ListRequest List(int? page, int? per_page,bool? enabled,EventType? eventtype)
        {
            return new ListRequest(service, page, per_page, enabled, eventtype);
        }

        public class ListRequest : BlueinkClientBaseService<IList<Blueink.Client.Net.v2.ResponseModel.Webhook>>
        {
            public ListRequest(IClientService service)
                : base(service)
            {
            }

            public ListRequest(IClientService service,
                int? page,
                int? per_page,
                bool? enabled,
                EventType? eventtype)
                : base(service)
            {
                this.Page = page;
                this.PerPage = per_page;
                this.Enabled = enabled;
                this.EventType = eventtype;
            }

            #region Pagination Support
            public virtual Nullable<int> Page { get; set; }

            public virtual Nullable<int> PerPage { get; set; }
            #endregion

            #region Query Parameters
            public virtual Nullable<bool> Enabled { get; set; }
            public virtual EventType? EventType { get; set; }
            #endregion

            public override string BuildUriRequest()
            {
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                if (Enabled.HasValue)
                    list.Add(new KeyValuePair<string, string>("enabled", this.Enabled.Value.ToString()));
                if (EventType.HasValue)
                    list.Add(new KeyValuePair<string, string>("event_type",EnumTypeHelper.ConvertEventTypeToString(this.EventType.Value)));

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
                get { return "webhooks"; }
            }

            public override string HttpMethod
            {
                get { return "get"; }
            }

        }

        public virtual GetRequest Get(string webhookid)
        {
            return new GetRequest(service, webhookid);
        }

        public class GetRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.Webhook>
        {
            public GetRequest(IClientService service, string webhookId)
                : base(service)
            {
                WebhookId = webhookId;
            }

            public virtual string WebhookId { get; private set; }

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
                get { return String.Format("webhooks/{0}/", WebhookId.ToString()); }
            }

            public override string HttpMethod
            {
                get { return "get"; }
            }

        }

        public virtual CreateRequest Create()
        {
            return new CreateRequest(service);
        }

        public virtual CreateRequest Create(
            string id,
            string url,
            bool? enabled,
            bool? json,
            IList<EventType> eventTypes,
            IList<WebhookExtraHeader> extraheaders)
        {
            return new CreateRequest(service, id, url,enabled,json,eventTypes,extraheaders);
        }

        public class CreateRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.Webhook>
        {
            public CreateRequest(IClientService service)
                : base(service)
            {

            }

            public CreateRequest(IClientService service,
                string id, 
                string url,
                bool? enabled,
                bool? json,
                IList<EventType> eventTypes,
                IList<WebhookExtraHeader> extraHeaders)
                : base(service)
            {
                Id = id;
                Url = url;
                Enabled = enabled;
                Json = json;
                EventTypes = eventTypes;
                ExtraHeaders = extraHeaders;
            }

            public virtual string Id
            {
                get;
                set;
            }

            public virtual string Url
            {
                get;
                set;
            }
            public virtual bool? Enabled
            { 
                get; 
                set;
            }
            public virtual bool? Json
            {
                get;
                set;
            }
            public virtual IList<EventType> EventTypes
            {
                get;
                set;
            }
            public virtual IList<WebhookExtraHeader> ExtraHeaders
            {
                get;
                set;
            }
            public override IEnumerable<KeyValuePair<string, string>> BuildRequestBody()
            {
                var list = new List<KeyValuePair<string, string>>();
                list.Add(new KeyValuePair<string, string>("id", this.Id));
                list.Add(new KeyValuePair<string, string>("url", this.Url));
                list.Add(new KeyValuePair<string, string>("enabled", this.Enabled.HasValue ? this.Enabled.Value.ToString() : "true"));
                list.Add(new KeyValuePair<string, string>("json", this.Json.HasValue ? this.Json.Value.ToString() : "true"));

                StringBuilder builder = new StringBuilder("[");
                foreach (var type in EventTypes)
                {
                    if (EventTypes.IndexOf(type) != 0)
                        builder.Append(",");
                    builder.Append(Service.SerializeObject(type));
                }
                builder.Append("]");
                list.Add(new KeyValuePair<string, string>("event_types", builder.ToString()));

                builder.Clear();
                builder.Append("[");
                foreach (var header in ExtraHeaders)
                {
                    if (ExtraHeaders.IndexOf(header) != 0)
                        builder.Append(",");
                    builder.Append(Service.SerializeObject(header));
                }
                builder.Append("]");
                list.Add(new KeyValuePair<string, string>("extra_headers", builder.ToString()));

                return list;
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
                get { return "webhooks/"; }
            }

            public override string HttpMethod
            {
                get { return "post"; }
            }

        }

        public virtual UpdateRequest Update(string webhookId)
        {
            return new UpdateRequest(service, webhookId);
        }

        public virtual UpdateRequest Update(
            string webhookId,
            string id,
            string url,
            bool? enabled,
            bool? json,
            IList<EventType> eventTypes,
            IList<WebhookExtraHeader> extraheaders)
        {
            return new UpdateRequest(service, webhookId, id, url,enabled,json,eventTypes,extraheaders);
        }

        public class UpdateRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.Webhook>
        {
            public UpdateRequest(IClientService service,
                string webhookId)
                : base(service)
            {
                WebhookId = webhookId;
            }

            public UpdateRequest(IClientService service, 
                string webhookId, 
                string id,
                string url,
                bool? enabled,
                bool? json,
                IList<EventType> eventTypes,
                IList<WebhookExtraHeader> extraHeaders
                )
                : base(service)
            {
                WebhookId = webhookId;
                Id = id;
                Url = url;
                Enabled = enabled;
                Json = json;
                EventTypes = eventTypes;
                ExtraHeaders = extraHeaders;
            }

            public virtual string WebhookId { get; private set; }
            public virtual string Id
            {
                get;
                set;
            }

            public virtual string Url
            {
                get;
                set;
            }
            public virtual bool? Enabled
            {
                get;
                set;
            }
            public virtual bool? Json
            {
                get;
                set;
            }
            public virtual IList<EventType> EventTypes
            {
                get;
                set;
            }
            public virtual IList<WebhookExtraHeader> ExtraHeaders
            {
                get;
                set;
            }

            public override IEnumerable<KeyValuePair<string, string>> BuildRequestBody()
            {
                var list = new List<KeyValuePair<string, string>>();
                list.Add(new KeyValuePair<string, string>("id", this.Id));
                list.Add(new KeyValuePair<string, string>("url", this.Url));
                list.Add(new KeyValuePair<string, string>("enabled", this.Enabled.HasValue ? this.Enabled.Value.ToString() : "true"));
                list.Add(new KeyValuePair<string, string>("json", this.Json.HasValue ? this.Json.Value.ToString() : "true"));

                StringBuilder builder = new StringBuilder("[");
                foreach (var type in EventTypes)
                {
                    if (EventTypes.IndexOf(type) != 0)
                        builder.Append(",");
                    builder.Append(Service.SerializeObject(type));
                }
                builder.Append("]");
                list.Add(new KeyValuePair<string, string>("event_types", builder.ToString()));

                builder.Clear();
                builder.Append("[");
                foreach (var header in ExtraHeaders)
                {
                    if (ExtraHeaders.IndexOf(header) != 0)
                        builder.Append(",");
                    builder.Append(Service.SerializeObject(header));
                }
                builder.Append("]");
                list.Add(new KeyValuePair<string, string>("extra_headers", builder.ToString()));

                return list;
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
                get { return String.Format("webhooks/{0}/", WebhookId); }
            }

            public override string HttpMethod
            {
                get { return "put"; }
            }

        }

        public virtual PartialUpdateRequest PartialUpdate(string webhookId)
        {
            return new PartialUpdateRequest(service, webhookId);
        }

        public virtual PartialUpdateRequest PartialUpdate(
            string webhookId,
            string id,
            string url,
            bool? enabled,
            bool? json,
            IList<EventType> eventTypes,
            IList<WebhookExtraHeader> extraHeaders)
        {
            return new PartialUpdateRequest(service, webhookId, id, url, enabled, json, eventTypes, extraHeaders);
        }

        public class PartialUpdateRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.Webhook>
        {
            public PartialUpdateRequest(IClientService service, string webhookId)
                : base(service)
            {
                WebhookId = webhookId;
            }

            public PartialUpdateRequest(IClientService service,
                string webhookId,
                string id,
                string url,
                bool? enabled,
                bool? json,
                IList<EventType> eventTypes,
                IList<WebhookExtraHeader> extraHeaders)
                : base(service)
            {
                WebhookId = webhookId;
                Id = id;
                Url = url;
                Enabled = enabled;
                Json = json;
                EventTypes = eventTypes;
                ExtraHeaders = extraHeaders;
            }

            public virtual string WebhookId { get; private set; }
            public virtual string Id
            {
                get;
                set;
            }

            public virtual string Url
            {
                get;
                set;
            }
            public virtual bool? Enabled
            {
                get;
                set;
            }
            public virtual bool? Json
            {
                get;
                set;
            }
            public virtual IList<EventType> EventTypes
            {
                get;
                set;
            }
            public virtual IList<WebhookExtraHeader> ExtraHeaders
            {
                get;
                set;
            }

            public override IEnumerable<KeyValuePair<string, string>> BuildRequestBody()
            {
                var list = new List<KeyValuePair<string, string>>();

                list.Add(new KeyValuePair<string, string>("id", this.Id));
                list.Add(new KeyValuePair<string, string>("url", this.Url));
                list.Add(new KeyValuePair<string, string>("enabled", this.Enabled.HasValue ? this.Enabled.Value.ToString() : "true"));
                list.Add(new KeyValuePair<string, string>("json", this.Json.HasValue ? this.Json.Value.ToString() : "true"));

                StringBuilder builder = new StringBuilder("[");
                foreach (var type in EventTypes)
                {
                    if (EventTypes.IndexOf(type) != 0)
                        builder.Append(",");
                    builder.Append(Service.SerializeObject(type));
                }
                builder.Append("]");
                list.Add(new KeyValuePair<string, string>("event_types", builder.ToString()));

                builder.Clear();
                builder.Append("[");
                foreach (var header in ExtraHeaders)
                {
                    if (ExtraHeaders.IndexOf(header) != 0)
                        builder.Append(",");
                    builder.Append(Service.SerializeObject(header));
                }
                builder.Append("]");
                list.Add(new KeyValuePair<string, string>("extra_headers", builder.ToString()));

                return list;
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
                get { return String.Format("webhooks/{0}/", WebhookId); }
            }

            public override string HttpMethod
            {
                get { return "patch"; }
            }

        }

        public virtual DeleteRequest Delete(string webhookId)
        {
            return new DeleteRequest(service, webhookId);
        }

        public class DeleteRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.Webhook>
        {
            public DeleteRequest(IClientService service, string webhookId)
                : base(service)
            {
                WebhookId = webhookId;
            }

            public virtual string WebhookId { get; private set; }

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
                get { return "delete"; }
            }

            public override string RestPath
            {
                get { return String.Format("webhooks/{0}/", WebhookId); }
            }

            public override string HttpMethod
            {
                get { return "delete"; }
            }

        }

        public virtual ListEventRequest ListEvent()
        {
            return new ListEventRequest(service);
        }

        public virtual ListEventRequest ListEvent(
            int? page,
            int? per_page,
            string webhook,
            EventType? eventtype,
            int? status,
            bool? success,
            DateTime? beforedDate,
            DateTime? afterDate)
        {
            return new ListEventRequest(service, page, per_page, webhook, eventtype, status, success, beforedDate, afterDate);
        }

        public class ListEventRequest : BlueinkClientBaseService<IList<Blueink.Client.Net.v2.ResponseModel.WebhookEvent>>
        {
            public ListEventRequest(IClientService service)
                : base(service)
            {
                this.Page = 1;
                this.PerPage = 50;
            }
            public ListEventRequest(
                IClientService service,
                int? page,
                int? per_page,
                string webhook,
                EventType? eventtype,
                int? status,
                bool? success,
                DateTime? beforeDate,
                DateTime? afterDate
                )
                : base(service)
            {
                this.Page = page;
                this.PerPage = per_page;
                this.Webhook = webhook;
                this.EventType = eventtype;
                this.Status = status;
                this.Success = success;
                this.BeforeDate = beforeDate;
                this.AfterDate = afterDate;
            }

            #region Pagination Support
            public virtual Nullable<int> Page { get; set; }
            public virtual Nullable<int> PerPage { get; set; }
            #endregion

            #region Query Parameters
            public virtual string Webhook { get; set; }
            public virtual EventType? EventType { get; set; }
            public virtual int? Status { get; set; }
            public virtual bool? Success { get; set; }
            public virtual DateTime? BeforeDate { get; set; }
            public virtual DateTime? AfterDate { get; set; }
            #endregion
            public override void BuildRequestContent(MultipartFormDataContent content)
            {
                base.BuildRequestContent(content);
            }
            public override string BuildUriRequest()
            {
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                if (!String.IsNullOrWhiteSpace(Webhook) && ValidationHelper.IsValidUUID(Webhook))
                    list.Add(new KeyValuePair<string, string>("webhook", this.Webhook));
                if (EventType.HasValue)
                    list.Add(new KeyValuePair<string, string>("event_type", EnumTypeHelper.ConvertEventTypeToString(this.EventType.Value)));
                if (Status.HasValue)
                    list.Add(new KeyValuePair<string, string>("status", Status.Value.ToString()));
                if (Success.HasValue)
                    list.Add(new KeyValuePair<string, string>("success", this.Success.Value.ToString()));
                if (this.BeforeDate.HasValue)
                    list.Add(new KeyValuePair<string, string>("before_date", this.BeforeDate.Value.ToString("yyyy-MM-dd")));
                if (this.AfterDate.HasValue)
                    list.Add(new KeyValuePair<string, string>("after_date", this.AfterDate.Value.ToString("yyyy-MM-dd")));
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
                get { return "webhooks/events/"; }
            }

            public override string HttpMethod
            {
                get { return "get"; }
            }
        }

        public virtual ListDeliveryRequest ListDelivery()
        {
            return new ListDeliveryRequest(service);
        }

        public virtual ListDeliveryRequest ListDelivery(
            int? page,
            int? per_page,
            string webhook,
            string webhook_event,
            string event_type,
            int? status,
            DateTime? beforeDate,
            DateTime? afterDate)
        {
            return new ListDeliveryRequest(service, page, per_page, webhook, webhook_event, event_type, status, beforeDate, afterDate);
        }

        public class ListDeliveryRequest : BlueinkClientBaseService<IList<Blueink.Client.Net.v2.ResponseModel.WebhookEvent>>
        {
            public ListDeliveryRequest(IClientService service)
                : base(service)
            {
                this.Page = 1;
                this.PerPage = 50;
            }
            public ListDeliveryRequest(
                IClientService service,
                int? page,
                int? per_page,
                string webhook,
                string webhook_event,
                string event_type,
                int? status,
                DateTime? beforeDate,
                DateTime? afterDate
                )
                : base(service)
            {
                this.Page = page;
                this.PerPage = per_page;
                this.Webhook = webhook;
                this.WebhookEvent = webhook_event;
                this.EventType = event_type;
                this.Status = status;
                this.BeforeDate = beforeDate;
                this.AfterDate = afterDate;
            }


            #region Pagination Support
            public virtual Nullable<int> Page { get; set; }
            public virtual Nullable<int> PerPage { get; set; }
            #endregion

            #region Query Parameters
            public virtual string Webhook { get; set; }
            public virtual string WebhookEvent { get; set; }
            // Enum: "bundle_sent" "bundle_complete" "bundle_docs_ready" "bundle_error" "bundle_cancelled" "packet_viewed" "packet_complete"
            public virtual string EventType { get; set; }
            public virtual int? Status { get; set; }
            public virtual DateTime? BeforeDate { get; set; }
            public virtual DateTime? AfterDate { get; set; }
            #endregion

            public override string BuildUriRequest()
            {
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                if (!String.IsNullOrWhiteSpace(Webhook))
                    list.Add(new KeyValuePair<string, string>("webhook", this.Webhook));
                if (!String.IsNullOrWhiteSpace(WebhookEvent))
                    list.Add(new KeyValuePair<string, string>("webhook_event", this.WebhookEvent));
                if (!String.IsNullOrWhiteSpace(EventType))
                    list.Add(new KeyValuePair<string, string>("event_type", this.EventType));
                if (this.Status.HasValue)
                    list.Add(new KeyValuePair<string, string>("status", this.Status.Value.ToString()));
                if (this.BeforeDate.HasValue)
                    list.Add(new KeyValuePair<string, string>("before_date", this.BeforeDate.Value.ToString("yyyy-MM-dd")));
                if (this.AfterDate.HasValue)
                    list.Add(new KeyValuePair<string, string>("after_date", this.AfterDate.Value.ToString("yyyy-MM-dd")));
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
                get { return "webhooks/deliveries/"; }
            }

            public override string HttpMethod
            {
                get { return "get"; }
            }
        }

        public virtual GetSecretRequest GetSecret()
        {
            return new GetSecretRequest(service);
        }

        public class GetSecretRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.WebhooSecret>
        {
            public GetSecretRequest(IClientService service)
                : base(service)
            {
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
                get { return "webhooks/secret/"; }
            }

            public override string HttpMethod
            {
                get { return "get"; }
            }

        }

        public virtual CreateSecretRequest CreateSecret()
        {
            return new CreateSecretRequest(service);
        }

        public class CreateSecretRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.WebhooSecret>
        {
            public CreateSecretRequest(IClientService service)
                : base(service)
            {

            }

            public override IEnumerable<KeyValuePair<string, string>> BuildRequestBody()
            {
                var list = new List<KeyValuePair<string, string>>();
                return list;
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
                get { return "webhooks/secret/regenerate/"; }
            }

            public override string HttpMethod
            {
                get { return "post"; }
            }

        }


        public virtual ListHeaderRequest ListHeaders()
        {
            return new ListHeaderRequest(service);
        }

        public virtual ListHeaderRequest ListHeaders(int? page, int? per_page, string webhook, EventType? eventtype)
        {
            return new ListHeaderRequest(service, page, per_page, webhook, eventtype);
        }

        public class ListHeaderRequest : BlueinkClientBaseService<IList<Blueink.Client.Net.v2.ResponseModel.WebhookExtraHeader>>
        {
            public ListHeaderRequest(IClientService service)
                : base(service)
            {
                this.Page = 1;
                this.PerPage = 50;
            }

            public ListHeaderRequest(IClientService service, int? page, int? per_page, string webhook, EventType? eventtype)
                : base(service)
            {
                this.Page = page;
                this.PerPage = per_page;
                this.Webhook = webhook;
                this.EventType = eventtype;
            }


            #region Pagination Support
            public virtual Nullable<int> Page { get; set; }
            public virtual Nullable<int> PerPage { get; set; }
            #endregion

            #region Query Parameters
            public virtual string Webhook { get; set; }

            public virtual EventType? EventType { get; set; }
            #endregion

            public override string BuildUriRequest()
            {
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                if (String.IsNullOrWhiteSpace(this.Webhook) && ValidationHelper.IsValidUUID(this.Webhook))
                    list.Add(new KeyValuePair<string, string>("webhook", this.Webhook));
                if (this.EventType.HasValue)
                    list.Add(new KeyValuePair<string, string>("event_type", EnumTypeHelper.ConvertEventTypeToString(this.EventType.Value)));
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
                get { return "webhooks/headers/"; }
            }

            public override string HttpMethod
            {
                get { return "get"; }
            }

        }

        public virtual GetHeaderRequest GetHeader(string webhookExtraHeaderId)
        {
            return new GetHeaderRequest(service, webhookExtraHeaderId);
        }

        public class GetHeaderRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.WebhookExtraHeader>
        {
            public GetHeaderRequest(IClientService service, string webhookExtraHeaderId)
                : base(service)
            {
                WebhookExtraHeaderId = webhookExtraHeaderId;
            }

            public virtual string WebhookExtraHeaderId { get; private set; }

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
                get { return String.Format("webhooks/headers/{0}/", WebhookExtraHeaderId); }
            }

            public override string HttpMethod
            {
                get { return "get"; }
            }

        }

        public virtual CreateHeaderRequest CreateHeader()
        {
            return new CreateHeaderRequest(service);
        }

        public virtual CreateHeaderRequest CreateHeader(
            string id,
            string webhook,
            string name,
            string value,
            int? order)
        {
            return new CreateHeaderRequest(service, id, webhook, name, value, order);
        }

        public class CreateHeaderRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.WebhookExtraHeader>
        {
            public CreateHeaderRequest(IClientService service)
                : base(service)
            {

            }

            public CreateHeaderRequest(IClientService service,
                string id,
                string webhook,
                string name,
                string value,
                int? order)
                : base(service)
            {
                Id = id;
                Webhook = webhook;
                Name = name;
                Value = value;
                Order = order;
            }

            public virtual string Id
            {
                get;
                set;
            }
            public virtual string Webhook
            {
                get;
                set;
            }
            public virtual string Name
            {
                get;
                set;
            }
            public virtual string Value
            {
                get;
                set;
            }
            public virtual int? Order
            {
                get;
                set;
            }
            public override IEnumerable<KeyValuePair<string, string>> BuildRequestBody()
            {
                var list = new List<KeyValuePair<string, string>>();

                list.Add(new KeyValuePair<string, string>("id", this.Id));
                list.Add(new KeyValuePair<string, string>("webhook", this.Webhook));
                list.Add(new KeyValuePair<string, string>("name", this.Name.TruncateString(80)));
                list.Add(new KeyValuePair<string, string>("value", this.Value.TruncateString(240)));
                list.Add(new KeyValuePair<string, string>("order", this.Order.HasValue ? this.Order.Value.ToString() : "1"));

                return list;
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
                get { return "webhooks/headers/"; }
            }

            public override string HttpMethod
            {
                get { return "post"; }
            }

        }

        public virtual UpdateHeaderRequest UpdateHeader(string webhookExtraHeaderId)
        {
            return new UpdateHeaderRequest(service, webhookExtraHeaderId);
        }

        public virtual UpdateHeaderRequest Update(
            string webhookExtraHeaderId,
            string id,
            string webhook,
            string name,
            string value,
            int? order)
        {
            return new UpdateHeaderRequest(service, webhookExtraHeaderId, id, webhook, name, value, order);
        }

        public class UpdateHeaderRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.WebhookExtraHeader>
        {
            public UpdateHeaderRequest(IClientService service, string webhookExtraHeaderId)
                : base(service)
            {
                WebhookExtraHeaderId = webhookExtraHeaderId;
            }

            public UpdateHeaderRequest(IClientService service,
                string webhookExtraHeaderId,
                string id,
                string webhook,
                string name,
                string value,
                int? order)
                : base(service)
            {
                WebhookExtraHeaderId = webhookExtraHeaderId;
                Id = id;
                Webhook = webhook;
                Name = name;
                Value = value;
                Order = order;
            }

            public virtual string WebhookExtraHeaderId
            {
                get;
                private set;
            }
            public virtual string Id
            {
                get;
                set;
            }
            public virtual string Webhook
            {
                get;
                set;
            }
            public virtual string Name
            {
                get;
                set;
            }
            public virtual string Value
            {
                get;
                set;
            }
            public virtual int? Order
            {
                get;
                set;
            }

            public override IEnumerable<KeyValuePair<string, string>> BuildRequestBody()
            {
                var list = new List<KeyValuePair<string, string>>();

                list.Add(new KeyValuePair<string, string>("id", this.Id));
                list.Add(new KeyValuePair<string, string>("webhook", this.Webhook));
                list.Add(new KeyValuePair<string, string>("name", this.Name));
                list.Add(new KeyValuePair<string, string>("value", this.Value));
                list.Add(new KeyValuePair<string, string>("order", this.Order.HasValue ? this.Order.Value.ToString() : "1"));

                return list;
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
                get { return String.Format("webhooks/headers/{0}/", WebhookExtraHeaderId); }
            }

            public override string HttpMethod
            {
                get { return "put"; }
            }

        }

        public virtual PartialUpdateHeaderRequest PartialUpdateHeader(string webhookExtraHeaderId)
        {
            return new PartialUpdateHeaderRequest(service, webhookExtraHeaderId);
        }

        public virtual PartialUpdateHeaderRequest PartialUpdateHeader(
            string webhookExtraHeaderId,
                string id,
                string webhook,
                string name,
                string value,
                int? order)
        {
            return new PartialUpdateHeaderRequest(service, webhookExtraHeaderId, id, webhook, name, value, order);
        }

        public class PartialUpdateHeaderRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.WebhookExtraHeader>
        {
            public PartialUpdateHeaderRequest(IClientService service, string webhookExtraHeaderId)
                : base(service)
            {
                WebhookExtraHeaderId = webhookExtraHeaderId;
            }

            public PartialUpdateHeaderRequest(IClientService service,
                string webhookExtraHeaderId,
                string id,
                string webhook,
                string name,
                string value,
                int? order)
                : base(service)
            {
                WebhookExtraHeaderId = webhookExtraHeaderId;
                Id = id;
                Webhook = webhook;
                Name = name;
                Value = value;
                Order = order;
            }

            public virtual string WebhookExtraHeaderId
            {
                get;
                private set;
            }
            public virtual string Id
            {
                get;
                set;
            }
            public virtual string Webhook
            {
                get;
                set;
            }
            public virtual string Name
            {
                get;
                set;
            }
            public virtual string Value
            {
                get;
                set;
            }
            public virtual int? Order
            {
                get;
                set;
            }

            public override IEnumerable<KeyValuePair<string, string>> BuildRequestBody()
            {
                var list = new List<KeyValuePair<string, string>>();

                list.Add(new KeyValuePair<string, string>("id", this.Id));
                list.Add(new KeyValuePair<string, string>("webhook", this.Webhook));
                list.Add(new KeyValuePair<string, string>("name", this.Name));
                list.Add(new KeyValuePair<string, string>("value", this.Value));
                list.Add(new KeyValuePair<string, string>("order", this.Order.HasValue ? this.Order.Value.ToString() : "1"));

                return list;
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
                get { return String.Format("persons/{0}/", WebhookExtraHeaderId); }
            }

            public override string HttpMethod
            {
                get { return "patch"; }
            }

        }

        public virtual DeleteHeaderRequest DeleteHeader(string webhookExtraHeaderId)
        {
            return new DeleteHeaderRequest(service, webhookExtraHeaderId);
        }

        public class DeleteHeaderRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.WebhookExtraHeader>
        {
            public DeleteHeaderRequest(IClientService service, string webhookExtraHeaderId)
                : base(service)
            {
                WebhookExtraHeaderId = webhookExtraHeaderId;
            }

            public virtual string WebhookExtraHeaderId { get; private set; }

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
                get { return "delete"; }
            }

            public override string RestPath
            {
                get { return String.Format("webhooks/headers/{0}/", WebhookExtraHeaderId); }
            }

            public override string HttpMethod
            {
                get { return "delete"; }
            }

        }
    }
}
