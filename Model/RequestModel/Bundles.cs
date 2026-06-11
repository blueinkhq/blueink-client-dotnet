using Blueink.Client.Net.v2.Helper;
using Blueink.Client.Net.v2.Model;
using Blueink.Client.Net.v2.Serializer;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueink.Client.Net.v2.RequestModel
{
    public class AutoPlacement
    {
        [Newtonsoft.Json.JsonPropertyAttribute("kind")]
        [Newtonsoft.Json.JsonRequired]
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public virtual FieldKind Kind { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("search")]
        [Newtonsoft.Json.JsonRequired]
        public virtual string Search { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("w")]
        [Newtonsoft.Json.JsonRequired]
        public virtual int W { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("h")]
        [Newtonsoft.Json.JsonRequired]
        public virtual int H { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("offset_x",
        NullValueHandling=Newtonsoft.Json.NullValueHandling.Ignore,
        DefaultValueHandling =Newtonsoft.Json.DefaultValueHandling.Ignore)]
        public virtual int? OffsetX { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("offset_y",
        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
        DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
        public virtual int? OffsetY { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("v_pattern",
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual int? VPattern { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("v_min",
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual int? VMin { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("v_max",
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual int? VMax { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("v_attachment_types",
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual IList<string> VAttachmentTypes { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("v_regex",
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual string VRegex { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("v_regex_msg",
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual string VRegexMsg { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("format",
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual string Format { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("initial_value",
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual object InitialValue { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("editors")]
        [Newtonsoft.Json.JsonRequired]
        public virtual IList<string> Editors { get; set; }

        public AutoPlacement()
        {
        }

        public static AutoPlacement Create(
        FieldKind kind,
        string search,
        int w,
        int h,
        int? offsetX,
        int? offsetY,
        List<string> editors = null)
        {
            var obj = new AutoPlacement
            {
                Kind = kind,
                Search = search,
                W = w,
                H = h,
                OffsetX = offsetX,
                OffsetY = offsetY,
                Editors = editors
            };

            return obj;
        }

        public void AddEditor(string editor)
        {
            if (Editors == null)
            {
                Editors = new List<string>();
            }
            Editors.Add(editor);
        }
    }

    public class Field
    {
        [Newtonsoft.Json.JsonPropertyAttribute("kind")]
        [Newtonsoft.Json.JsonRequired]
        public virtual FieldKind Kind { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("key")]
        [Newtonsoft.Json.JsonRequired]
        public virtual string Key { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("label")]
        public virtual string Label { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("x")]
        [Newtonsoft.Json.JsonRequired]
        public virtual int X { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("y")]
        [Newtonsoft.Json.JsonRequired]
        public virtual int Y { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("w")]
        [Newtonsoft.Json.JsonRequired]
        public virtual int W { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("h")]
        [Newtonsoft.Json.JsonRequired]
        public virtual int H { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("page")]
        [Newtonsoft.Json.JsonRequired]
        public virtual int Page { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("v_pattern", 
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual int? VPattern { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("v_min", 
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual int? VMin { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("v_max",
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual int? VMax { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("v_attachment_types",
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual IList<string> VAttachmentTypes { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("v_regex",
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual string VRegex { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("v_regex_msg",
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual string VRegexMsg { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("format",
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual string Format { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("initial_value",
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual object InitialValue { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("editors",
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual IList<string> Editors { get; set; }

        public Field()
        {
        }

        public Field(FieldKind kind, string key, int x, int y, int w, int h, int page, string label = null,
                 int? vPattern = null, int? vMin = null, int? vMax = null, List<string> editors = null)
        {
            Kind = kind;
            Key = key;
            X = x;
            Y = y;
            W = w;
            H = h;
            Page = page;
            Label = label;
            VPattern = vPattern;
            VMin = vMin;
            VMax = vMax;
            Editors = editors;
        }

        public static Field Create(int x, int y, int w, int h, int page, FieldKind kind, string key = null)
        {
            if (string.IsNullOrEmpty(key))
            {
                key = ValidationHelper.generate_key("field",5);
            }

            return new Field(kind, key, x, y, w, h, page);
        }

        public void AddEditor(string editor)
        {
            if (Editors == null)
            {
                Editors = new List<string>();
            }
            Editors.Add(editor);
        }
    }

    public class Packet
    {
        [Newtonsoft.Json.JsonPropertyAttribute("key")]
        [Newtonsoft.Json.JsonRequired]
        public virtual string Key { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("name")]
        public virtual string Name { get; set; }
        
        private string _email;
        [EmailAddressAttribute()]
        [MaxLength(254)]
        [Newtonsoft.Json.JsonProperty("email")]
        public virtual string Email
        {
            get => _email;
            set
            {
                if (!String.IsNullOrWhiteSpace(value) && !ValidationHelper.IsValidEmail(value))
                        throw new ArgumentException("invalid email");
                _email = value;
            }
        }

        [Newtonsoft.Json.JsonPropertyAttribute("Phone")]
        [MaxLength(128)]
        public virtual string Phone { get; set; }
        [Newtonsoft.Json.JsonProperty("auth_sms")]
        public virtual bool? AuthSms { get; set; }
        [Newtonsoft.Json.JsonProperty("auth_selfie")]
        public virtual bool? AuthSelfie { get; set; }
        [Newtonsoft.Json.JsonProperty("auth_id")]
        public virtual bool? AuthId { get; set; }
        [Newtonsoft.Json.JsonProperty("signing_complete_redirect")]
        [MaxLength(400)]
        public virtual string SigningCompleteRedirect { get; set; }
        [Newtonsoft.Json.JsonProperty("suppress_all")]
        public virtual bool? SuppressAll { get; set; }
        [Newtonsoft.Json.JsonProperty("suppress_docs_ready")]
        public virtual bool? SuppressDocsReady { get; set; }
        [Newtonsoft.Json.JsonProperty("suppress_signing")]
        public virtual bool? SuppressSigning { get; set; }
        [Newtonsoft.Json.JsonProperty("suppress_reminder")]
        public virtual bool? SuppressReminder { get; set; }

        private string _personId = null;
        [Newtonsoft.Json.JsonPropertyAttribute("person_id",DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual string PersonId
        {
            get => _personId;
            set
            {
                if (!String.IsNullOrWhiteSpace(value) && !value.IsValidUUID())
                   throw new ArgumentException("invalid person_id");                
                _personId = value.NullIfWhiteSpace();
            }
        }

        private DeliveryVia _deliveryVia;
        [Newtonsoft.Json.JsonProperty("deliver_via")]
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public virtual DeliveryVia DeliverVia 
        { 
            get => _deliveryVia;
            set
            {
                if (value == DeliveryVia.Embed)
                    throw new ArgumentException("invalid DeliveryVia embed not allowed");
                _deliveryVia = value;
            }
        }
        [Newtonsoft.Json.JsonPropertyAttribute("order")]
        public virtual int? Order { get; set; }

        public static Packet Create(string key, string name )
        {
            if (String.IsNullOrWhiteSpace(key))
                key = ValidationHelper.generate_key("packet",5);

            return new Packet() { Key = key, Name = name };
        }
    }

    public class TemplateRefAssignment
    {
        [Newtonsoft.Json.JsonPropertyAttribute("role")]
        [Newtonsoft.Json.JsonRequired]
        public virtual string Role { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("signer")]
        [Newtonsoft.Json.JsonRequired]
        public virtual string Signer { get; set; }
        public static TemplateRefAssignment Create(string role, string signer)
        {
            return new TemplateRefAssignment() { Role = role, Signer = signer };
        }
    }

    public class TemplateRefFieldValue
    {
        [Newtonsoft.Json.JsonPropertyAttribute("key")]
        public virtual string Key { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("initial_value")]
        public virtual string InitialValue { get; set; }
        public static TemplateRefFieldValue Create(string key, string initialValue)
        {
            return new TemplateRefFieldValue() { Key = key, InitialValue = initialValue };
        }
    }

    public class EnvelopeTemplateFieldValue
    {
        [Newtonsoft.Json.JsonPropertyAttribute("key")]
        [Newtonsoft.Json.JsonRequired]
        public virtual string Key { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("initial_value")]
        public virtual string InitialValue { get; set; }

        public static EnvelopeTemplateFieldValue Create(string key, string initialValue)
        {
            return new EnvelopeTemplateFieldValue() { Key = key, InitialValue = initialValue };
        }
    }

    public class EnvelopeTemplate
    {
        [Newtonsoft.Json.JsonPropertyAttribute("template_id")]
        public virtual string TemplateId { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("field_values")]
        public virtual IList<EnvelopeTemplateFieldValue> FieldValues { get; set; }

        public static EnvelopeTemplate Create(string key, IList<EnvelopeTemplateFieldValue> fieldValues = null)
        {
            return new EnvelopeTemplate() { TemplateId = key, FieldValues = fieldValues };
        }
        public void AddFieldValue(EnvelopeTemplateFieldValue fieldValue)
        {
            if (this.FieldValues == null)
                this.FieldValues = new List<EnvelopeTemplateFieldValue>();
            this.FieldValues.Add(fieldValue);
        }
    
    }

    public interface IDocument
    {
        [Newtonsoft.Json.JsonProperty("key")]
        [Newtonsoft.Json.JsonRequired]
        string Key { get; set; }
    }

    public class TemplateRef : IDocument
    {
        [Newtonsoft.Json.JsonProperty("key")]
        [Newtonsoft.Json.JsonRequired]
        public virtual string Key { get; set; }
        [Newtonsoft.Json.JsonProperty("template_id")]
        [Newtonsoft.Json.JsonRequired]
        public virtual string TemplateId { get; set; }
        [Newtonsoft.Json.JsonProperty("assignments")]
        [Newtonsoft.Json.JsonRequired]
        public virtual IList<TemplateRefAssignment> Assignments { get; set; }
        [Newtonsoft.Json.JsonProperty("field_values")]
        public virtual IList<TemplateRefFieldValue> FieldValues { get; set; }

        public static TemplateRef Create(string key,string templateId, IList<TemplateRefAssignment> assignments = null, IList<TemplateRefFieldValue> fieldValues = null)
        {
            if (String.IsNullOrWhiteSpace(key))
                key = ValidationHelper.generate_key("tmpl",5);

            return new TemplateRef() { TemplateId = key, Assignments = assignments, FieldValues = fieldValues };
        }

        public void AddAssignment(TemplateRefAssignment assignment)
        {
            if (this.Assignments == null)
                this.Assignments = new List<TemplateRefAssignment>();
            this.Assignments.Add(assignment);
        }

        public void AddFieldValue(TemplateRefFieldValue fieldValue)
        {
            if (this.FieldValues == null)
                this.FieldValues = new List<TemplateRefFieldValue>();
            this.FieldValues.Add(fieldValue);
        }
    }

    
    public class DocumentRef : IDocument
    {
        [Newtonsoft.Json.JsonProperty("key")]
        [Newtonsoft.Json.JsonRequired]
        public virtual string Key { get; set; }
        [Newtonsoft.Json.JsonProperty("file_url", 
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual string FileUrl { get; set; }
        [Newtonsoft.Json.JsonProperty("file_b64",
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual string FileBinary64 { get; set; }
        [Newtonsoft.Json.JsonProperty("file_html",
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual string FileHtml { get; set; }
        [Newtonsoft.Json.JsonProperty("filename")]
        public virtual string Filename { get; set; }
        [Newtonsoft.Json.JsonProperty("file_index",
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual int? FileIndex { get; set; }
        [Newtonsoft.Json.JsonProperty("fields", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual IList<Field> Fields { get; set; }
        [Newtonsoft.Json.JsonProperty("auto_placements", DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]

        public virtual IList<AutoPlacement> AutoPlacements { get; set; }
        [Newtonsoft.Json.JsonProperty("converted_adobe_fields_to",
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual string ConvertedAdobeFieldsTo { get; set; }
        [Newtonsoft.Json.JsonProperty("html_fields_mode",
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual string HtmlFieldsMode { get; set; }
        [Newtonsoft.Json.JsonProperty("parse_tags",
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore,
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public virtual string ParseTags { get; set; }

        public static DocumentRef Create(string key)
        {
            if (String.IsNullOrWhiteSpace(key))
                key = ValidationHelper.generate_key("doc",5);

            return new DocumentRef() { Key = key };
        }

        public void AddField(Field field)
        {
            if (Fields == null)
            {
                Fields = new List<Field>();
            }
            Fields.Add(field);
        }

        public void AddAutoPlacement(AutoPlacement autoPlacement)
        {
            if (AutoPlacements == null)
            {
                AutoPlacements = new List<AutoPlacement>();
            }
            AutoPlacements.Add(autoPlacement);
        }

        
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

    public class Bundle
    {
        [Newtonsoft.Json.JsonPropertyAttribute("label")]
        public virtual string Label { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("is_test")]
        [Newtonsoft.Json.JsonRequired]
        public virtual bool IsTest { get; set; } = true;
        [Newtonsoft.Json.JsonPropertyAttribute("in_order")]
        public virtual bool InOrder { get; set; }
        private string _email_subject = null;
        [Newtonsoft.Json.JsonPropertyAttribute("email_subject",
        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
        DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
        [MaxLength(400)]
        public virtual string EmailSubject 
        { 
            get => _email_subject;
            set => _email_subject = value.NullIfWhiteSpace();          
        }
        private string _email_message = null;
        [Newtonsoft.Json.JsonPropertyAttribute("email_message",
        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
        DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
        public virtual string EmailMessage 
        {
            get => _email_message;
            set => _email_message = value.NullIfWhiteSpace();
        }
        private string _sms_message = null;
        [Newtonsoft.Json.JsonPropertyAttribute("sms_message",
        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
        DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
        public virtual string SMSMessage 
        { 
            get => _sms_message;
            set => _sms_message = value.NullIfWhiteSpace();            
        }
        private string _requester_name = null;
        [Newtonsoft.Json.JsonPropertyAttribute("requester_name",
        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
        DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
        public virtual string RequesterName
        {
            get => _requester_name;
            set => _requester_name = value.NullIfWhiteSpace();            
        }

        private string _requesterEmail = String.Empty;
        [Newtonsoft.Json.JsonPropertyAttribute("requester_email",
        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
        DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
        [MaxLength(254)]
        public virtual string RequesterEmail 
        { 
            get => _requesterEmail;
            set
            {               
                if (!String.IsNullOrWhiteSpace(value) && !ValidationHelper.IsValidEmail(value))
                    throw new ArgumentException("invalid requester email");                
                _requesterEmail = value.NullIfWhiteSpace();
            }
        }
        [Newtonsoft.Json.JsonPropertyAttribute("cc_emails")]
        public virtual IList<string> CCEmails { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("custom_key")]
        public virtual string CustomKey { get; set; }
        private string _team = null;
        [Newtonsoft.Json.JsonPropertyAttribute("team",
        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
        DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
        public virtual string Team 
        {
            get => _team;
            set => _team = value.NullIfWhiteSpace();
        }
        private string _signingBrand = null;
        [Newtonsoft.Json.JsonPropertyAttribute("signing_brand",
        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
        DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
        public virtual string SigningBrand 
        {
            get => _signingBrand;
            set => _signingBrand = value.NullIfWhiteSpace();
        }

        private BundleStatus? _status = null;
        [Newtonsoft.Json.JsonPropertyAttribute("status",
        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
        DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
        public virtual BundleStatus? Status 
        { 
            get => _status;
            set
            {
                if (value.HasValue && value.Value != BundleStatus.Draft)
                    throw new ArgumentException("Status can only be null or draft!");
                _status = value;
            }
        }

        [Newtonsoft.Json.JsonPropertyAttribute("expires",
        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
        DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
        public virtual DateTime? Expires { get; set; }

        [Newtonsoft.Json.JsonPropertyAttribute("payment",
        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
        DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
        public virtual BundlePayment Payment { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("packets")]
        public virtual IList<Packet> Packets { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("documents")]
        [Newtonsoft.Json.JsonConverter(typeof(DocumentOrTemplateConverter))]
        public virtual IList<IDocument> Documents { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("envelope_template",
        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
        DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
        public virtual EnvelopeTemplate EnvelopeTemplate { get; set; }

        public Bundle()
        {
            this.Packets = new List<Packet>();
            this.Documents = new List<IDocument>();
            this.CCEmails = new List<string>();
        }

        public static Bundle Create(IList<Packet> packets, IList<IDocument> documents)
        {
            return new Bundle() { Packets = packets, Documents = documents , CCEmails = new List<string>() };
        }

        public void AddPacket(Packet packet)
        {
            if (this.Packets == null)
                this.Packets = new List<Packet>();
            this.Packets.Add(packet);
        }
        public void AddDocument(IDocument document)
        {
            if (this.Documents == null)
                this.Documents = new List<IDocument>();
            this.Documents.Add(document);
        }
        public void AddCCEmail(string email)
        {
            if (String.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("email cannot be null!");

            if (email.Length > 254)
                throw new ArgumentOutOfRangeException("email as exceeded the maximum length 254 characters!");

            if (!ValidationHelper.IsValidEmail(email))
                throw new InvalidOperationException("email is not valid!");

            if (this.CCEmails == null)
                this.CCEmails = new List<string>();
            this.CCEmails.Add(email);
        }
    }

    /// <summary>
    /// Request model for creating an embedded document preparation session.
    /// At least one document source must be provided: upload_pdf=true, template_ids, or folder_ids.
    /// </summary>
    public class PreparationSessionRequest
    {
        [Newtonsoft.Json.JsonPropertyAttribute("draft_bundle",
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
        public virtual string DraftBundle { get; set; }

        [Newtonsoft.Json.JsonPropertyAttribute("folder_ids",
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
        public virtual IList<string> FolderIds { get; set; }

        [Newtonsoft.Json.JsonPropertyAttribute("redirect_url",
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
        public virtual string RedirectUrl { get; set; }

        [Newtonsoft.Json.JsonPropertyAttribute("template_ids",
         NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
         DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore)]
        public virtual IList<string> TemplateIds { get; set; }

        [Newtonsoft.Json.JsonPropertyAttribute("upload_pdf")]
        public virtual bool UploadPdf { get; set; } = true;

        [Newtonsoft.Json.JsonPropertyAttribute("allow_search_signers")]
        public virtual bool AllowSearchSigners { get; set; } = false;
    }
}
