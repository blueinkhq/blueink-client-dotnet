using System.Collections.Generic;
using Newtonsoft.Json.Converters;
using Blueink.Client.Net.v2.Model;
using System.ComponentModel.DataAnnotations;

namespace Blueink.Client.Net.v2.RequestModel
{
    public class ContactChannel
    {
        [Newtonsoft.Json.JsonPropertyAttribute("id")]
        public virtual string Id { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("email")]
        [MaxLength(254)]
        public virtual string Email { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("phone")]
        [MaxLength(128)]
        public virtual string Phone { get; set; }
        // Enum: "em" "mp"
        [Newtonsoft.Json.JsonPropertyAttribute("kind")]
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public virtual ContactChannelKind Kind { get; set; }
    }

    public class Person
    {
        [Newtonsoft.Json.JsonPropertyAttribute("name")]
        public virtual string Name { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("metadata")]
        public virtual Dictionary<string, string> Metadata { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("channels")]
        public virtual IList<ContactChannel> Channels { get; set; }

        public Person()
        {
            this.Metadata = new Dictionary<string, string>();
            this.Channels = new List<ContactChannel>();
        }

        public void AddEmail(string email)
        {
            if (this.Channels == null)
                this.Channels = new List<ContactChannel>();
            this.Channels.Add(new ContactChannel() { Email = email, Kind = ContactChannelKind.Email });
        }
        public void AddPhone(string phone)
        {
            if (this.Channels == null)
                this.Channels = new List<ContactChannel>();
            this.Channels.Add(new ContactChannel() { Phone = phone, Kind = ContactChannelKind.Phone });
        }
    }
}
