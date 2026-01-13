using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blueink.Client.Net.v2.Model;

namespace Blueink.Client.Net.v2.Helper
{
    public static class EnumTypeHelper
    {
        public static string ConvertErrorCodeToString(ErrorCode value)
        {
            string result = "authentication_failed";
            switch (value)
            {
                case ErrorCode.AuthenticationFailed: result = "authentication_failed"; break;
                case ErrorCode.Conflict: result = "conflict"; break;
                case ErrorCode.Error: result = "error"; break;
                case ErrorCode.Invalid: result = "invalid"; break;
                case ErrorCode.MethodNotAllowed: result = "method_not_allowed"; break;
                case ErrorCode.NotAcceptable: result = "not_acceptable"; break;
                case ErrorCode.NotAuthenticated: result = "not_authenticated"; break;
                case ErrorCode.NotFound: result = "not_found"; break;
                case ErrorCode.PermissionDenied: result = "permission_denied"; break;
                case ErrorCode.ParseError: result = "parse_error"; break;
                case ErrorCode.ServiceUnavailable: result = "service_unavailable"; break;
                case ErrorCode.Throttled: result = "throttled"; break;
                case ErrorCode.UnsupportedMediaType: result = "unsupported_media_type"; break;
            }
            return result;
        }
        public static ErrorCode ConvertStringToErrorCode(string value)
        {
            ErrorCode code = ErrorCode.AuthenticationFailed;
            if (String.IsNullOrWhiteSpace(value))
                return code;

            if (value.Equals("authentication_failed", StringComparison.CurrentCultureIgnoreCase))
                code = ErrorCode.AuthenticationFailed;
            else if (value.Equals("conflict", StringComparison.CurrentCultureIgnoreCase))
                code = ErrorCode.Conflict;
            else if (value.Equals("error", StringComparison.CurrentCultureIgnoreCase))
                code = ErrorCode.Error;
            else if (value.Equals("invalid", StringComparison.CurrentCultureIgnoreCase))
                code = ErrorCode.Invalid;
            else if (value.Equals("method_not_allowed", StringComparison.CurrentCultureIgnoreCase))
                code = ErrorCode.MethodNotAllowed;
            else if (value.Equals("not_acceptable", StringComparison.CurrentCultureIgnoreCase))
                code = ErrorCode.NotAcceptable;
            else if (value.Equals("not_authenticated", StringComparison.CurrentCultureIgnoreCase))
                code = ErrorCode.NotAuthenticated;
            else if (value.Equals("not_found", StringComparison.CurrentCultureIgnoreCase))
                code = ErrorCode.NotFound;
            else if (value.Equals("permission_denied", StringComparison.CurrentCultureIgnoreCase))
                code = ErrorCode.PermissionDenied;
            else if (value.Equals("parse_error", StringComparison.CurrentCultureIgnoreCase))
                code = ErrorCode.ParseError;
            else if (value.Equals("service_unavailable", StringComparison.CurrentCultureIgnoreCase))
                code = ErrorCode.ServiceUnavailable;
            else if (value.Equals("throttled", StringComparison.CurrentCultureIgnoreCase))
                code = ErrorCode.Throttled;
            else if (value.Equals("unsupported_media_type", StringComparison.CurrentCultureIgnoreCase))
                code = ErrorCode.UnsupportedMediaType;

            return code;
        }

        public static string ConvertEventTypeToString(EventType value)
        {
            string result = "bundle_sent";
            switch (value)
            {
                case EventType.BundleSent: result = "bundle_sent"; break;
                case EventType.BundleComplete: result = "bundle_complete"; break;
                case EventType.BundleDocsReady: result = "bundle_docs_ready"; break;
                case EventType.BundleError: result = "bundle_error"; break;
                case EventType.BundleCancelled: result = "bundle_cancelled"; break;
                case EventType.PacketViewed: result = "packet_viewed"; break;
                case EventType.PacketComplete: result = "packet_complete"; break;
            }
            return result;
        }

        public static EventType ConvertStringToEventType(string value)
        {
            EventType type = EventType.BundleSent;
            if (String.IsNullOrWhiteSpace(value))
                return type;

            if (value.Equals("bundle_sent", StringComparison.CurrentCultureIgnoreCase))
                type = EventType.BundleSent;
            else if (value.Equals("bundle_complete", StringComparison.CurrentCultureIgnoreCase))
                type = EventType.BundleComplete;
            else if (value.Equals("bundle_docs_ready", StringComparison.CurrentCultureIgnoreCase))
                type = EventType.BundleDocsReady;
            else if (value.Equals("bundle_error", StringComparison.CurrentCultureIgnoreCase))
                type = EventType.BundleError;
            else if (value.Equals("bundle_cancelled", StringComparison.CurrentCultureIgnoreCase))
                type = EventType.BundleCancelled;
            else if (value.Equals("packet_viewed", StringComparison.CurrentCultureIgnoreCase))
                type = EventType.PacketViewed;
            else if (value.Equals("packet_complete", StringComparison.CurrentCultureIgnoreCase))
                type = EventType.PacketComplete;

            return type;
        }

        public static string ConvertAttachmentTypeToString(AttachmentType value)
        {
            string result = "jpg";
            switch (value)
            {
                case AttachmentType.Jpg: result = "jpg"; break;
                case AttachmentType.Jpeg: result = "jpeg"; break;
                case AttachmentType.Png: result = "png"; break;
                case AttachmentType.Gif: result = "gif"; break;
                case AttachmentType.Svg: result = "svg"; break;
                case AttachmentType.Pdf: result = "pdf"; break;
                case AttachmentType.Doc: result = "doc"; break;
                case AttachmentType.Docx: result = "docx"; break;
                case AttachmentType.Ppt: result = "ppt"; break;
                case AttachmentType.Pptx: result = "pptx"; break;
                case AttachmentType.Xls: result = "xls"; break;
                case AttachmentType.Xlsx: result = "xlsx"; break;
                case AttachmentType.Rtf: result = "rtf"; break;
                case AttachmentType.Txt: result = "txt"; break;
            }
            return result;
        }
        public static AttachmentType ConvertStringToAttachmentType(string value)
        {
            AttachmentType type = AttachmentType.Jpg;
            if (String.IsNullOrWhiteSpace(value))
                return type;

            if (value.Equals("jpg", StringComparison.CurrentCultureIgnoreCase))
                type = AttachmentType.Jpg;
            else if (value.Equals("jpeg", StringComparison.CurrentCultureIgnoreCase))
                type = AttachmentType.Jpeg;
            else if (value.Equals("png", StringComparison.CurrentCultureIgnoreCase))
                type = AttachmentType.Png;
            else if (value.Equals("gif", StringComparison.CurrentCultureIgnoreCase))
                type = AttachmentType.Gif;
            else if (value.Equals("svg", StringComparison.CurrentCultureIgnoreCase))
                type = AttachmentType.Svg;
            else if (value.Equals("pdf", StringComparison.CurrentCultureIgnoreCase))
                type = AttachmentType.Pdf;
            else if (value.Equals("doc", StringComparison.CurrentCultureIgnoreCase))
                type = AttachmentType.Doc;
            else if (value.Equals("docx", StringComparison.CurrentCultureIgnoreCase))
                type = AttachmentType.Docx;
            else if (value.Equals("ppt", StringComparison.CurrentCultureIgnoreCase))
                type = AttachmentType.Ppt;
            else if (value.Equals("pptx", StringComparison.CurrentCultureIgnoreCase))
                type = AttachmentType.Pptx;
            else if (value.Equals("xls", StringComparison.CurrentCultureIgnoreCase))
                type = AttachmentType.Xls;
            else if (value.Equals("xlsx", StringComparison.CurrentCultureIgnoreCase))
                type = AttachmentType.Xlsx;
            else if (value.Equals("rtf", StringComparison.CurrentCultureIgnoreCase))
                type = AttachmentType.Rtf;
            else if (value.Equals("txt", StringComparison.CurrentCultureIgnoreCase))
                type = AttachmentType.Txt;

            return type;
        }

        public static string ConvertValidatePatternValueToString(ValidatePatternValue value)
        {
            string result = "email";
            switch (value)
            {
                case ValidatePatternValue.Email: result = "email"; break;
                case ValidatePatternValue.BankRouting: result = "bank_routing"; break;
                case ValidatePatternValue.BankAccount: result = "bank_account"; break;
                case ValidatePatternValue.Letters: result = "letters"; break;
                case ValidatePatternValue.Numbers: result = "numbers"; break;
                case ValidatePatternValue.Phone: result = "phone"; break;
                case ValidatePatternValue.Ssn: result = "ssn"; break;
                case ValidatePatternValue.ZipCode: result = "zip_code"; break;
            }
            return result;
        }

        public static ValidatePatternValue ConvertStringToValidatePatternValue(string value)
        {
            ValidatePatternValue type = ValidatePatternValue.Email;
            if (String.IsNullOrWhiteSpace(value))
                return type;

            if (value.Equals("email", StringComparison.CurrentCultureIgnoreCase))
                type = ValidatePatternValue.Email;
            else if (value.Equals("bank_routing", StringComparison.CurrentCultureIgnoreCase))
                type = ValidatePatternValue.BankRouting;
            else if (value.Equals("bank_account", StringComparison.CurrentCultureIgnoreCase))
                type = ValidatePatternValue.BankAccount;
            else if (value.Equals("letters", StringComparison.CurrentCultureIgnoreCase))
                type = ValidatePatternValue.Letters;
            else if (value.Equals("numbers", StringComparison.CurrentCultureIgnoreCase))
                type = ValidatePatternValue.Numbers;
            else if (value.Equals("phone", StringComparison.CurrentCultureIgnoreCase))
                type = ValidatePatternValue.Phone;
            else if (value.Equals("ssn", StringComparison.CurrentCultureIgnoreCase))
                type = ValidatePatternValue.Ssn;
            else if (value.Equals("zip_code", StringComparison.CurrentCultureIgnoreCase))
                type = ValidatePatternValue.ZipCode;

            return type;
        }

        public static string ConvertFieldKindToString(FieldKind value)
        {
            string result = "att";
            switch (value)
            {
                case FieldKind.Attachment: result = "att"; break;
                case FieldKind.Checkboxes: result = "cbx"; break;
                case FieldKind.CheckboxGroup: result = "cbg"; break;
                case FieldKind.Checkbox: result = "chk"; break;
                case FieldKind.Date: result = "dat"; break;
                case FieldKind.Initials: result = "ini"; break;
                case FieldKind.Input: result = "inp"; break;
                case FieldKind.SignerDate: result = "sdt"; break;
                case FieldKind.Dropdown: result = "sel"; break;
                case FieldKind.ESignature: result = "sig"; break;
                case FieldKind.SigningDate: result = "tms"; break;
                case FieldKind.MultilineText: result = "txt"; break;
                case FieldKind.SignerName: result = "snm"; break;
            }
            return result;
        }

        public static FieldKind ConvertStringToFieldKind(string value)
        {
            FieldKind kind = FieldKind.Attachment;
            if (String.IsNullOrWhiteSpace(value))
                return kind;

            if (value.Equals("att", StringComparison.CurrentCultureIgnoreCase))
                kind = FieldKind.Attachment;
            else if (value.Equals("cbx", StringComparison.CurrentCultureIgnoreCase))
                kind = FieldKind.Checkboxes;
            else if (value.Equals("cbg", StringComparison.CurrentCultureIgnoreCase))
                kind = FieldKind.CheckboxGroup;
            else if (value.Equals("chk", StringComparison.CurrentCultureIgnoreCase))
                kind = FieldKind.Checkbox;
            else if (value.Equals("dat", StringComparison.CurrentCultureIgnoreCase))
                kind = FieldKind.Date;
            else if (value.Equals("ini", StringComparison.CurrentCultureIgnoreCase))
                kind = FieldKind.Initials;
            else if (value.Equals("inp", StringComparison.CurrentCultureIgnoreCase))
                kind = FieldKind.Input;
            else if (value.Equals("sdt", StringComparison.CurrentCultureIgnoreCase))
                kind = FieldKind.SignerDate;
            else if (value.Equals("sel", StringComparison.CurrentCultureIgnoreCase))
                kind = FieldKind.Dropdown;
            else if (value.Equals("sig", StringComparison.CurrentCultureIgnoreCase))
                kind = FieldKind.ESignature;
            else if (value.Equals("tms", StringComparison.CurrentCultureIgnoreCase))
                kind = FieldKind.SigningDate;
            else if (value.Equals("txt", StringComparison.CurrentCultureIgnoreCase))
                kind = FieldKind.MultilineText;
            else if (value.Equals("snm", StringComparison.CurrentCultureIgnoreCase))
                kind = FieldKind.SignerName;

            return kind;
        }

        public static string ConvertContactChannelKindToString(ContactChannelKind value)
        {
            string result = "em";
            switch (value)
            {
                case ContactChannelKind.Email: result = "em"; break;
                case ContactChannelKind.Phone: result = "mp"; break;
            }
            return result;
        }
        public static ContactChannelKind ConvertStringToContactChannelKind(string value)
        {
            ContactChannelKind kind = ContactChannelKind.Email;
            if (String.IsNullOrWhiteSpace(value))
                return kind;

            if (value.Equals("em", StringComparison.CurrentCultureIgnoreCase))
                kind = ContactChannelKind.Email;
            else if (value.Equals("mp", StringComparison.CurrentCultureIgnoreCase))
                kind = ContactChannelKind.Phone;

            return kind;
        }


        public static string ConvertBundleStatusToString(BundleStatus value)
        {
            string result = "dr";
            switch (value)
            {
                case BundleStatus.Draft: result = "dr"; break;
                case BundleStatus.Sent: result = "se"; break;
                case BundleStatus.Started: result = "st"; break;
                case BundleStatus.Complete: result = "co"; break;
                case BundleStatus.Cancelled: result = "ca"; break;
                case BundleStatus.Expired: result = "ex"; break;
                case BundleStatus.Failed: result = "fa"; break;
            }
            return result;
        }

        public static BundleStatus ConvertStringToBundleStatus(string value)
        {
            BundleStatus status = BundleStatus.Draft;
            if (String.IsNullOrWhiteSpace(value))
                return status;

            if (value.Equals("dr", StringComparison.CurrentCultureIgnoreCase))
                status = BundleStatus.Draft;
            else if (value.Equals("se", StringComparison.CurrentCultureIgnoreCase))
                status = BundleStatus.Sent;
            else if (value.Equals("st", StringComparison.CurrentCultureIgnoreCase))
                status = BundleStatus.Started;
            else if (value.Equals("co", StringComparison.CurrentCultureIgnoreCase))
                status = BundleStatus.Complete;
            else if (value.Equals("ca", StringComparison.CurrentCultureIgnoreCase))
                status = BundleStatus.Cancelled;
            else if (value.Equals("ex", StringComparison.CurrentCultureIgnoreCase))
                status = BundleStatus.Expired;
            else if (value.Equals("fa", StringComparison.CurrentCultureIgnoreCase))
                status = BundleStatus.Failed;

            return status;
        }

        public static string ConvertBundleOrderingToString(BundleOrdering value)
        {
            string result = "created";
            switch (value)
            {
                case BundleOrdering.Created: result = "created"; break;
                case BundleOrdering.Sent: result = "sent"; break;
                case BundleOrdering.CompletedAt: result = "completed_at"; break;
                case BundleOrdering.ReverseCreated: result = "-created"; break;
                case BundleOrdering.ReverseSent: result = "-sent"; break;
                case BundleOrdering.ReverseCompletedAt: result = "-completed_at"; break;
            }
            return result;
        }

        public static BundleOrdering ConvertStringToBundleOrdering(string value)
        {
            BundleOrdering order = BundleOrdering.Created;
            if (String.IsNullOrWhiteSpace(value))
                return order;

            if (value.Equals("created", StringComparison.CurrentCultureIgnoreCase))
                order = BundleOrdering.Created;
            else if (value.Equals("sent", StringComparison.CurrentCultureIgnoreCase))
                order = BundleOrdering.Sent;
            else if (value.Equals("completed_at", StringComparison.CurrentCultureIgnoreCase))
                order = BundleOrdering.CompletedAt;
            else if (value.Equals("-created", StringComparison.CurrentCultureIgnoreCase))
                order = BundleOrdering.ReverseCreated;
            else if (value.Equals("-sent", StringComparison.CurrentCultureIgnoreCase))
                order = BundleOrdering.ReverseSent;
            else if (value.Equals("-completed_at", StringComparison.CurrentCultureIgnoreCase))
                order = BundleOrdering.ReverseCompletedAt;

            return order;
        }

        public static string ConvertPacketStatusToString(PacketStatus value)
        {
            string result = "ne";
            switch (value)
            {
                case PacketStatus.New: result = "ne"; break;
                case PacketStatus.Ready: result = "re"; break;
                case PacketStatus.Sent: result = "se"; break;
                case PacketStatus.Started: result = "st"; break;
                case PacketStatus.Cancelled: result = "ca"; break;
                case PacketStatus.Expired: result = "ex"; break;
                case PacketStatus.Complete: result = "co"; break;
                case PacketStatus.Failed: result = "fa"; break;
            }
            return result;
        }

        public static PacketStatus ConvertStringToPacketStatus(string value)
        {
            PacketStatus status = PacketStatus.New;
            if (String.IsNullOrWhiteSpace(value))
                return status;

            if (value.Equals("ne", StringComparison.CurrentCultureIgnoreCase))
                status = PacketStatus.New;
            else if (value.Equals("re", StringComparison.CurrentCultureIgnoreCase))
                status = PacketStatus.Ready;
            else if (value.Equals("se", StringComparison.CurrentCultureIgnoreCase))
                status = PacketStatus.Sent;
            else if (value.Equals("st", StringComparison.CurrentCultureIgnoreCase))
                status = PacketStatus.Started;
            else if (value.Equals("ca", StringComparison.CurrentCultureIgnoreCase))
                status = PacketStatus.Cancelled;
            else if (value.Equals("ex", StringComparison.CurrentCultureIgnoreCase))
                status = PacketStatus.Expired;
            else if (value.Equals("co", StringComparison.CurrentCultureIgnoreCase))
                status = PacketStatus.Complete;
            else if (value.Equals("fa", StringComparison.CurrentCultureIgnoreCase))
                status = PacketStatus.Failed;

            return status;
        }

        public static string ConvertSignerSendViaToString(SendVia value)
        {
            string result = "em";
            switch (value)
            {
                case SendVia.Email: result = "em"; break;
                case SendVia.Sms: result = "sm"; break;
                case SendVia.Kios: result = "ki"; break;
                case SendVia.Both: result = "bo"; break;
            }

            return result;
        }

        public static SendVia ConvertStringToSendVia(string value)
        {
            SendVia send = SendVia.Email;
            if (String.IsNullOrWhiteSpace(value))
                return send;

            if (value.Equals("em", StringComparison.CurrentCultureIgnoreCase))
                send = SendVia.Email;
            else if (value.Equals("sm", StringComparison.CurrentCultureIgnoreCase))
                send = SendVia.Sms;
            else if (value.Equals("ki", StringComparison.CurrentCultureIgnoreCase))
                send = SendVia.Kios;
            else if (value.Equals("bo",StringComparison.CurrentCultureIgnoreCase))
                send = SendVia.Both;
            return send;
        }

        public static string ConvertDeliverViaToString(DeliveryVia value)
        {
            string result = "email";
            switch (value)
            {
                case DeliveryVia.Email: result = "email"; break;
                case DeliveryVia.Phone: result = "phone"; break;
                case DeliveryVia.Embed: result = "embed"; break;
            }

            return result;
        }

        public static DeliveryVia ConvertStringToSignerDeliveryVia(string value)
        {
            DeliveryVia delivery = DeliveryVia.Email;
            if (String.IsNullOrWhiteSpace(value))
                return delivery;

            if (value.Equals("email", StringComparison.CurrentCultureIgnoreCase))
                delivery = DeliveryVia.Embed;
            else if (value.Equals("phone", StringComparison.CurrentCultureIgnoreCase))
                delivery = DeliveryVia.Phone;
            else if (value.Equals("embed", StringComparison.CurrentCultureIgnoreCase))
                delivery = DeliveryVia.Embed;

            return delivery;
        }
    }

}
