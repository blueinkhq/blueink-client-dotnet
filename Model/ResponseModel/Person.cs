using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Blueink.Client.Net.v2.Model;
using Newtonsoft.Json.Converters;

namespace Blueink.Client.Net.v2.ResponseModel
{
    public class ContactChannel
    {
        [Newtonsoft.Json.JsonPropertyAttribute("id")]
        public virtual string Id { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("email")]
        public virtual string Email { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("phone")]
        public virtual string Phone { get; set; }
        // Enum: "em" "mp"
        [Newtonsoft.Json.JsonPropertyAttribute("kind")]
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public virtual ContactChannelKind Kind { get; set; }
    }
    public class Person
    {
        [Newtonsoft.Json.JsonPropertyAttribute("id")]
        public virtual string Id { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("name")]
        public virtual string Name { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("metadata")]
        public virtual Dictionary<string,string> Metadata { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("is_user")]
        public virtual bool IsUser { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("channels")]
        public virtual IList<ContactChannel> Channels { get; set; }
    }
}
