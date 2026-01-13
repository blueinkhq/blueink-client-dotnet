using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueink.Client.Net.v2.RequestModel
{
    public class WebhooSecret
    {
        [Newtonsoft.Json.JsonPropertyAttribute("secret")]
        public virtual string Secret { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("create_date")]
        public virtual DateTime CreateDate { get; set; }
    }
    public class WebhookDelivery
    {
        [Newtonsoft.Json.JsonPropertyAttribute("pk")]
        public virtual string PK { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("timestamp")]
        public virtual DateTime Timestamp { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("status")]
        public virtual int Status { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("id")]
        public virtual string Message { get; set; }
    }
    public class WebhookEvent
    {
        [Newtonsoft.Json.JsonPropertyAttribute("pk")]
        public virtual string PK { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("webhook")]
        public virtual string Webhook { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("event_type")]
        public virtual string EventType { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("created")]
        public virtual DateTime Created { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("status")]
        public virtual int Status { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("success")]
        public virtual bool Success { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("payload")]
        public virtual string Payload { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("deliveries")]
        public virtual IList<WebhookDelivery> Deliveries { get; set; }
    }
    public class WebhookExtraHeader
    {
        [Newtonsoft.Json.JsonPropertyAttribute("id")]
        public virtual string Id { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("webhook")] 
        public virtual string Webhook { get; set; }
        // must adhere to RFC 7230
        [Newtonsoft.Json.JsonPropertyAttribute("name")]
        public virtual string Name { get; set; }
        // must adhere to RFC 7230
        [Newtonsoft.Json.JsonPropertyAttribute("value")] 
        public virtual string Value { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("order")] 
        public virtual int Order { get; set; }
    }
    public class Webhook
    {
        [Newtonsoft.Json.JsonPropertyAttribute("id")]
        public virtual string Id { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("url")]
        public virtual string Url { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("enable")]
        public virtual bool Enable { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("json")]
        public virtual bool Json { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("event_types")]
        public virtual IList<string> EventTypes { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("event_headers")]
        public virtual IList<WebhookExtraHeader> ExtraHeaders { get; set; }
    }
}
