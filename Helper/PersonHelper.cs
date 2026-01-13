using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blueink.Client.Net.v2.Model;
using Blueink.Client.Net.v2.RequestModel;

namespace Blueink.Client.Net.v2.Helper
{
    public class PersonHelper
    {
        public PersonHelper()
        {
        }

        public Person CreatePerson()
        {
            var person = new Person();
            person.Name = this.Name;
            
            foreach (var email in this.Emails)
                person.Channels.Add(new ContactChannel() { Email = email, Kind = ContactChannelKind.Email } );
                        
            foreach (var phone in this.Phones)
                person.Channels.Add(new ContactChannel() { Phone = phone, Kind = ContactChannelKind.Phone } );
                  
            foreach (var data in this.Metadata)
                person.Metadata[data.Key] = data.Value;
            
            return person;
        }

        public List<Person> CreatePersons()
        {
            return new List<Person>() { CreatePerson() };
        }

        public void ReplaceEmails(List<string> emails)
        {
            if (emails == null) return;

            this.Emails.Clear();
            this.Emails.AddRange(emails);
        }

        public void ReplacePhones(List<string> phones)
        {
            if (phones == null) return;

            this.Phones.Clear();
            this.Phones.AddRange(phones);
        }

        public void ReplaceMetadata(Dictionary<string,string> data)
        {
            if (data == null) return;

            this.Metadata.Clear();
            foreach(var pair in data)
                this.Metadata[pair.Key] = pair.Value;
        }

        public string Name { get; set; }
        public List<string> Emails { get; } = new List<string>();
        public List<string> Phones { get; } = new List<string>();
        public Dictionary<string, string> Metadata { get; } = new Dictionary<string, string>();
    }
}
