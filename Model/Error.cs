using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Blueink.Client.Net.v2.Model
{
    public class ErrorField
    {
        [Newtonsoft.Json.JsonPropertyAttribute("field")]
        public virtual string Field { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("message")]
        public virtual string Message { get; set; }
    }

    public class Error
    {
        //Enum: "authentication_failed" "conflict" "error" "invalid" "method_not_allowed" "not_acceptable" "not_authenticated" "not_found" "permission_denied" "parse_error" "service_unavailable" "throttled" "unsupported_media_type"
        [Newtonsoft.Json.JsonPropertyAttribute("code")]
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public virtual ErrorCode Code { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("message")]
        public virtual string Message { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("detail")]
        public virtual string Detail { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("errors")]
        public virtual IList<ErrorField> Errors { get; set; }
    }
}
