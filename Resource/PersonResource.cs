using Blueink.Client.Net.v2.Common;
using Blueink.Client.Net.v2.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Blueink.Client.Net.v2.Resource
{
    /// <summary>
    /// Provides access to Person-related API operations.
    /// Persons represent contacts/signers in the Blueink system.
    /// </summary>
    public class PersonResource
    {
        private readonly IClientService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonResource"/> class.
        /// </summary>
        /// <param name="service">The client service instance.</param>
        public PersonResource(IClientService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Lists all persons with default pagination (page 1, 50 per page).
        /// </summary>
        /// <returns>A request object that can be executed to retrieve persons.</returns>
        public virtual ListRequest List()
        {
            return new ListRequest(service);
        }

        /// <summary>
        /// Lists persons with specified filters and pagination.
        /// </summary>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="per_page">The number of results per page.</param>
        /// <param name="search">Search term to filter persons.</param>
        /// <param name="email">Filter by email address.</param>
        /// <param name="phone">Filter by phone number.</param>
        /// <returns>A request object that can be executed to retrieve persons.</returns>
        public virtual ListRequest List(int? page, int? per_page,string search, string email, string phone)
        {
            return new ListRequest(service,page,per_page, search, email, phone);
        }

        public class ListRequest : BlueinkClientBaseService<IList<Blueink.Client.Net.v2.ResponseModel.Person>>
        {
            public ListRequest(IClientService service)
                : base(service)
            {
                this.Page = 1;
                this.PerPage = 50;
            }
            public ListRequest(IClientService service,int? page,int? per_page, string search, string email, string phone)
                : base(service)
            {
                this.Page = page;
                this.PerPage = per_page;
                this.Search = search;
                this.Email = email;
                this.Phone = phone;
            }

            #region Pagination Support
            public virtual Nullable<int> Page { get; set; }
            public virtual Nullable<int> PerPage { get; set; }
            #endregion

            #region Query Parameters
            public virtual string Search { get; set; }
            public virtual string Email { get; set; }
            public virtual string Phone { get; set; }
            #endregion
            public override void BuildRequestContent(MultipartFormDataContent content)
            {
                base.BuildRequestContent(content);
            }
            public override string BuildUriRequest()
            {
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                if (!String.IsNullOrWhiteSpace(Search))
                    list.Add(new KeyValuePair<string, string>("search",this.Search));
                if (!String.IsNullOrWhiteSpace(Email))
                    list.Add(new KeyValuePair<string, string>("email",this.Email));
                if (!String.IsNullOrWhiteSpace(Phone))
                    list.Add(new KeyValuePair<string, string>("phone", this.Phone));
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
                get { return "persons/"; }
            }

            public override string HttpMethod
            {
                get { return "get"; }
            }
        }

        public virtual GetRequest RetrievePerson(string personId)
        {
            return new GetRequest(service, personId);
        }

        public class GetRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.Person>
        {
            public GetRequest(IClientService service, string personId)
                : base(service)
            {
                PersonId = personId;
            }

            public virtual string PersonId { get; private set; }
            public override  void BuildRequestContent(MultipartFormDataContent content)
            {
                base.BuildRequestContent(content);
            }
            public override string BuildJsonRequestBody()
            {
                return base.BuildJsonRequestBody();
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
                get { return String.Format("persons/{0}/", PersonId); }
            }

            public override string HttpMethod
            {
                get { return "get"; }
            }

        }

        public virtual CreateRequest CreatePerson(
            string name, 
            Dictionary<string,string> metadata,
            List<string> emails,
            List<string> phones)
        {
            var personHelper = new PersonHelper();
            
            personHelper.Name = name;
            personHelper.ReplaceMetadata(metadata);
            personHelper.ReplaceEmails(emails);
            personHelper.ReplacePhones(phones);

            return CreateFromPersonHelper(personHelper);
        }

        public virtual CreateRequest CreateFromPersonHelper(PersonHelper personHelper)
        {
            if (personHelper == null) return null;

            return new CreateRequest(service, personHelper);
        }

        public class CreateRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.Person>
        {
            public CreateRequest(IClientService service,
                PersonHelper personHelper)
                : base(service)
            {
                PersonHelper = personHelper;
            }
            public virtual PersonHelper PersonHelper { get; set; }
            public override void BuildRequestContent(MultipartFormDataContent content)
            {
                base.BuildRequestContent(content);
            }
            public override string BuildJsonRequestBody()
            {
                return Service.SerializeObject(PersonHelper.CreatePerson());
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
                get { return "create"; }
            }

            public override string RestPath
            {
                get { return "persons/"; }
            }

            public override string HttpMethod
            {
                get { return "post"; }
            }

        }

        public virtual UpdateRequest UpdatePerson(string personId, 
            string name, 
            Dictionary<string, string> metadata,
            List<string> emails,
            List<string> phones)
        {
            var personHelper = new PersonHelper();

            personHelper.Name = name;
            personHelper.ReplaceMetadata(metadata);
            personHelper.ReplaceEmails(emails);
            personHelper.ReplacePhones(phones);

            return UpdatePersonFromPersonHelper(personId, personHelper);
        }

        public virtual UpdateRequest UpdatePersonFromPersonHelper(string personId,PersonHelper helper)
        {
            if (helper == null) return null;

            return new UpdateRequest(service,personId, helper);
        }

        public class UpdateRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.Person>
        {
            public UpdateRequest(IClientService service, string personId, PersonHelper personHelper)
                : base(service)
            {
                PersonId = personId;
                PersonHelper = personHelper;
            }

            public virtual string PersonId 
            {
                get; 
                private set;
            }

            public virtual PersonHelper PersonHelper 
            {
                get;
                set;
            }

            public override void BuildRequestContent(MultipartFormDataContent content)
            {
                base.BuildRequestContent(content);
            }
            public override string BuildJsonRequestBody()
            {
                return Service.SerializeObject(PersonHelper.CreatePerson());
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
                get { return "update"; }
            }

            public override string RestPath
            {
                get { return String.Format("persons/{0}/", PersonId); }
            }

            public override string HttpMethod
            {
                get { return "put"; }
            }

        }

        public virtual PartialUpdateRequest PartialUpdatePerson(string personId,
            string name,
            Dictionary<string, string> metadata,
            List<string> emails,
            List<string> phones )
        {
            var personHelper = new PersonHelper();

            personHelper.Name = name;
            personHelper.ReplaceMetadata(metadata);
            personHelper.ReplaceEmails(emails);
            personHelper.ReplacePhones(phones);

            return PartialUpdatePersonFromPersonHelper(personId, personHelper);
        }

        public virtual PartialUpdateRequest PartialUpdatePersonFromPersonHelper(string personId, PersonHelper personHelper)
        {
            if (personHelper == null) return null;

            return new PartialUpdateRequest(service, personId, personHelper);
        }

        public class PartialUpdateRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.Person>
        {
            
            public PartialUpdateRequest(IClientService service, string personId, PersonHelper personHelper)
                : base(service)
            {
                PersonId = personId;
                PersonHelper = personHelper;
            }

            public virtual string PersonId
            {
                get;
                private set;
            }

            public virtual PersonHelper PersonHelper
            {
                get;
                set;
            }

            public override void BuildRequestContent(MultipartFormDataContent content)
            {
                base.BuildRequestContent(content);
            }

            public override string BuildJsonRequestBody()
            {
                return Service.SerializeObject(PersonHelper.CreatePerson());
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
                get { return "update"; }
            }

            public override string RestPath
            {
                get { return String.Format("persons/{0}/", PersonId); }
            }

            public override string HttpMethod
            {
                get { return "patch"; }
            }

        }

        public virtual DeleteRequest DeletePerson(string personId)
        {
            return new DeleteRequest(service, personId);
        }

        public class DeleteRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.Person>
        {
            public DeleteRequest(IClientService service, string personId)
                : base(service)
            {
                PersonId = personId;
            }

            public virtual string PersonId { get; private set; }

            public override void BuildRequestContent(MultipartFormDataContent content)
            {
                base.BuildRequestContent(content);
            }
            public override string BuildJsonRequestBody()
            {
                return base.BuildJsonRequestBody();
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
                get { return "delete"; }
            }

            public override string RestPath
            {
                get { return String.Format("persons/{0}/", PersonId); }
            }

            public override string HttpMethod
            {
                get { return "delete"; }
            }

        }

    }
}
