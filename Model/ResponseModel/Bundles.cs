using Blueink.Client.Net.v2.Model;
using Blueink.Client.Net.v2.Serializer;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace Blueink.Client.Net.v2.ResponseModel
{
    public class EmbededSigning
    {
        [Newtonsoft.Json.JsonPropertyAttribute("url")]
        public virtual string Url { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("expires")]
        public virtual DateTime Expires { get; set; }
    }

    public class PacketCertificateOfEvidence
    {
        [Newtonsoft.Json.JsonPropertyAttribute("file_url")]
        public virtual string FileUrl { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("expires")]
        public virtual DateTime Expires { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("sha256")]
        public virtual string SHA256 { get; set; }
    }


    public class Packet
    {
        [Newtonsoft.Json.JsonPropertyAttribute("key")]
        public virtual string Key { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("name")]
        public virtual string Name { get; set; }
        [Newtonsoft.Json.JsonProperty("email")]
        public virtual string Email { get; set; }
        [Newtonsoft.Json.JsonProperty("phone")]
        public virtual string Phone { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("auth_sms")]
        public virtual bool? AuthSms { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("auth_selfie")]
        public virtual bool? AuthSelfie { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("auth_id")]
        public virtual bool? AuthId { get; set; }
        [Newtonsoft.Json.JsonProperty("signing_complete_redirect")]
        public virtual string SigningCompleteRedirect { get; set; }
        public virtual bool? SuppressAll { get; set; }
        [Newtonsoft.Json.JsonProperty("suppress_docs_ready")]
        public virtual bool? SuppressDocsReady { get; set; }
        [Newtonsoft.Json.JsonProperty("suppress_signing")]
        public virtual bool? SuppressSigning { get; set; }
        [Newtonsoft.Json.JsonProperty("suppress_reminder")]
        public virtual bool? SuppressReminder { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("id")]
        public virtual string Id { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("person_id")]
        public virtual string PersonId { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("status")]
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public virtual PacketStatus Status { get; set; }
        [Newtonsoft.Json.JsonProperty("deliver_via")]
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public virtual DeliveryVia? DeliverVia { get; set; }
        [Newtonsoft.Json.JsonProperty("order")]
        public virtual string Order { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("completed_at")]
        public virtual DateTime? CompletedAt { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("last_accessed_at")]
        public virtual DateTime? LastAccessedAt { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("requires_witness")]
        public virtual bool? RequiresWitness { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("witness_nominated_by")]
        public virtual string WitnessNominatedBy { get; set; }
    }
    public class BundlePayment
    {
        [Newtonsoft.Json.JsonPropertyAttribute("billed_to")]
        [Newtonsoft.Json.JsonRequired()]
        public virtual string BilledTo { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("amount_due")]
        [Newtonsoft.Json.JsonRequired()]
        public virtual decimal AmountDue { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("payment_method_types")]
        public virtual IList<string> PaymentMethodTypes { get; set; }
    }
    public class BundleDocument
    {
        [Newtonsoft.Json.JsonPropertyAttribute("template_id")]
        public virtual string TemplateId { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("key")]
        public virtual string Key { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("name")]
        public virtual string Name { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("order")]
        public virtual int Order { get; set; }
    }
    public class Bundle
    {
        [Newtonsoft.Json.JsonPropertyAttribute("label")]
        public virtual string Label { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("in_order")]
        public virtual bool InOrder { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("email_subject")]
        public virtual string EmailSubject { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("email_message")]
        public virtual string EmailMessage { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("sms_message")]
        public virtual string SMSMessage { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("requester_name")]
        public virtual string RequesterName { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("requester_email")]
        public virtual string RequesterEmail { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("cc_emails")]
        public virtual IList<string> CorrespondaceEmails { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("custom_key")]
        public virtual string CustomKey { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("team")]
        public virtual string Team { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("signing_brand")]
        public virtual string SigningBrand { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("is_test")]
        public virtual bool IsTest { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("status")]
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public virtual BundleStatus Status { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("payments")]
        public virtual IList<BundlePayment> Payments { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("id")]
        public virtual string Id { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("created")]
        public virtual DateTime Created { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("sent")]
        public virtual DateTime? Sent { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("completed_at")]
        public virtual DateTime? CompletedAt { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("docs_ready")]
        public virtual bool DocumentsReady { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("errors")]
        public virtual IList<Error> Errors { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("packets")]
        public virtual IList<Packet> Packets { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("documents")]
        public virtual IList<BundleDocument> Documents { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("tags")]
        public virtual IList<string> Tags { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("send_reminders")]
        public virtual bool SendReminders { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("reminder_offset")]
        public virtual int ReminderOffset { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("reminder_expires")]
        public virtual int ReminderExpires { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("expires")]
        public virtual DateTime? Expires { get; set; }  
        [Newtonsoft.Json.JsonPropertyAttribute("data")]
        public virtual IList<BundleData> Data { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("events")]
        public virtual IList<BundleEvent> Events { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("files")]
        public virtual IList<BundleFile> Files { get; set; }
    }

    public class BundleFile
    {
        [Newtonsoft.Json.JsonPropertyAttribute("file_url")]
        public virtual string FileUrl { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("doc_key")]
        public virtual string DocKey { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("expires")]
        public virtual DateTime Expires { get; set; }
    }

    public class BundleEvent
    {
        [Newtonsoft.Json.JsonPropertyAttribute("kind")]
        public virtual string Kind { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("description")]
        public virtual string Description { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("timestamp")]
        public virtual DateTime Timestamp { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("packet_id")]
        public virtual string PacketId { get; set; }
    }

    public class BundleAttachment
    {
        [Newtonsoft.Json.JsonPropertyAttribute("url")]
        public virtual string Url { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("name")]
        public virtual string Name { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("size")]
        public virtual int Size { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("num")]
        public virtual int Number { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("ext")]
        public virtual string Ext { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("is_image")]
        public virtual bool IsImage { get; set; }
    }
    public class BundleData
    {
        [Newtonsoft.Json.JsonPropertyAttribute("doc_key")]
        public virtual string DocKey { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("field_key")]
        public virtual string FieldKey { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("label")]
        public virtual string Label { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("kind")]
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public virtual FieldKind Kind { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("value")]
        [Newtonsoft.Json.JsonConverter(typeof(StringOrBooleanOrNumberConverter))]
        public virtual object Value { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("filled_by")]
        public virtual string FilledBy { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("packet_id", NullValueHandling = NullValueHandling.Include)]
        public virtual string PacketId { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("attachments", NullValueHandling = NullValueHandling.Include)]
        public virtual IList<BundleAttachment> Attachments { get; set; }
    }
}
