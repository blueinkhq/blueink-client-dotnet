using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Blueink.Client.Net.v2.Model
{
    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public enum ErrorCode
    {
        [EnumMember(Value ="authentication_failed")]
        AuthenticationFailed,
        [EnumMember(Value = "conflict")]
        Conflict,
        [EnumMember(Value = "error")]
        Error,
        [EnumMember(Value = "invalid")]
        Invalid,
        [EnumMember(Value = "method_not_allowed")]
        MethodNotAllowed,
        [EnumMember(Value = "not_acceptable")]
        NotAcceptable,
        [EnumMember(Value = "not_authenticated")]
        NotAuthenticated,
        [EnumMember(Value = "not_found")]
        NotFound,
        [EnumMember(Value = "permission_denied")]
        PermissionDenied,
        [EnumMember(Value = "parse_error")]
        ParseError,
        [EnumMember(Value = "service_unavailable")]
        ServiceUnavailable,
        [EnumMember(Value = "throttled")]
        Throttled,
        [EnumMember(Value = "unsupported_media_type")]
        UnsupportedMediaType
    }

    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public enum EventType
    {
        [EnumMember(Value = "bundle_sent")]
        BundleSent,
        [EnumMember(Value = "bundle_complete")]
        BundleComplete,
        [EnumMember(Value = "bundle_docs_ready")]
        BundleDocsReady,
        [EnumMember(Value = "bundle_error")]
        BundleError,
        [EnumMember(Value = "bundle_cancelled")]
        BundleCancelled,
        [EnumMember(Value = "packet_viewed")]
        PacketViewed,
        [EnumMember(Value = "packet_complete")]
        PacketComplete
    }

    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public enum AttachmentType
    {
        [EnumMember(Value = "jpg")]
        Jpg,
        [EnumMember(Value = "jpeg")]
        Jpeg,
        [EnumMember(Value = "png")]
        Png,
        [EnumMember(Value = "gif")]
        Gif,
        [EnumMember(Value = "svg")]
        Svg,
        [EnumMember(Value = "pdf")]
        Pdf,
        [EnumMember(Value = "doc")]
        Doc,
        [EnumMember(Value = "docx")]
        Docx,
        [EnumMember(Value = "ppt")]
        Ppt,
        [EnumMember(Value = "pptx")]
        Pptx,
        [EnumMember(Value = "xls")]
        Xls,
        [EnumMember(Value = "xlsx")]
        Xlsx,
        [EnumMember(Value = "rtf")]
        Rtf,
        [EnumMember(Value = "txt")]
        Txt
    }

    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public enum ValidatePatternValue
    {
        [EnumMember(Value = "email")]
        Email,
        [EnumMember(Value = "bank_routing")]
        BankRouting,
        [EnumMember(Value = "bank_account")]
        BankAccount,
        [EnumMember(Value = "letters")]
        Letters,
        [EnumMember(Value = "numbers")]
        Numbers,
        [EnumMember(Value = "phone")]
        Phone,
        [EnumMember(Value = "ssn")]
        Ssn,
        [EnumMember(Value = "zip_code")]
        ZipCode
    }

    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public enum FieldKind
    {
        [EnumMember(Value = "sig")]
        ESignature, 
        [EnumMember(Value = "ini")]
        Initials,
        [EnumMember(Value = "snm")]
        SignerName,
        [EnumMember(Value = "sdt")]
        SignerDate,
        [EnumMember(Value = "inp")]
        Input,
        [EnumMember(Value = "txt")]
        MultilineText,
        [EnumMember(Value = "dat")]
        Date,
        [EnumMember(Value = "chk")]
        Checkbox,
        [EnumMember(Value = "cbg")]
        CheckboxGroup, 
        [EnumMember(Value = "cbx")]
        Checkboxes,
        [EnumMember(Value = "att")]
        Attachment,              
        [EnumMember(Value = "sel")]
        Dropdown,
        [EnumMember(Value = "tms")]
        SigningDate
    }

    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public enum ContactChannelKind
    {
        [EnumMember(Value = "em")]
        Email,
        [EnumMember(Value = "mp")]
        Phone
    }

    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public enum BundleStatus
    { 
        [EnumMember(Value = "ne")]
        New,
        [EnumMember(Value = "dr")]
        Draft,
        [EnumMember(Value = "pe")]
        Pending,
        [EnumMember(Value = "se")]
        Sent,
        [EnumMember(Value = "st")]
        Started,
        [EnumMember(Value = "co")]
        Complete,
        [EnumMember(Value = "ca")]
        Cancelled,
        [EnumMember(Value = "ex")]
        Expired,
        [EnumMember(Value = "fa")]
        Failed
    }

    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public enum BundleOrdering
    {
        [EnumMember(Value = "created")]
        Created,
        [EnumMember(Value = "sent")]
        Sent,
        [EnumMember(Value = "completed_at")]
        CompletedAt,
        [EnumMember(Value = "-created")]
        ReverseCreated,
        [EnumMember(Value = "-sent")]
        ReverseSent,
        [EnumMember(Value = "-completed_at")]
        ReverseCompletedAt
    }

    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public enum DeliveryVia
    {
        [EnumMember(Value = "email")]
        Email,
        [EnumMember(Value = "phone")]
        Phone,
        [EnumMember(Value = "embed")]
        Embed
    }

    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public enum SendVia
    {
        [EnumMember(Value = "em")]
        Email,
        [EnumMember(Value = "sm")]
        Sms,
        [EnumMember(Value = "ki")]
        Kios,
        [EnumMember(Value = "bo")]
        Both
    }

    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public enum PacketStatus
    {
        [EnumMember(Value = "ne")]
        New,
        [EnumMember(Value = "re")]
        Ready,
        [EnumMember(Value = "se")]
        Sent,
        [EnumMember(Value = "st")]
        Started,
        [EnumMember(Value = "ca")]
        Cancelled,
        [EnumMember(Value = "ex")]
        Expired,
        [EnumMember(Value = "co")]
        Complete,
        [EnumMember(Value = "fa")]
        Failed
    }

    [Flags]
    public enum BundleIncludeFlag
    {
        None = 0,
        Data = 1,
        Events = 2,
        Files = 4,
        All = Data|Events|Files
    }
}
