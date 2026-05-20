using Blueink.Client.Net.v2.Common;
using Blueink.Client.Net.v2.Helper;
using Blueink.Client.Net.v2.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Blueink.Client.Net.v2.Resource
{
    /// <summary>
    /// Provides access to Bundle-related API operations.
    /// Bundles are collections of documents that are sent to one or more signers.
    /// </summary>
    public class BundleResource
    {
        private readonly IClientService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="BundleResource"/> class.
        /// </summary>
        /// <param name="service">The client service instance.</param>
        public BundleResource(IClientService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Lists all bundles with default pagination (page 1, 50 per page).
        /// </summary>
        /// <returns>A request object that can be executed to retrieve bundles.</returns>
        public virtual ListBundleRequest ListBundles()
        {
            return new ListBundleRequest(service);
        }

        /// <summary>
        /// Lists bundles with specified filters and pagination.
        /// </summary>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="per_page">The number of results per page.</param>
        /// <param name="search">Search term to filter bundles.</param>
        /// <param name="tag">Filter by tag.</param>
        /// <param name="template">Filter by template ID.</param>
        /// <param name="createdBefore">Filter bundles created before this date.</param>
        /// <param name="createdAfter">Filter bundles created after this date.</param>
        /// <param name="sentBefore">Filter bundles sent before this date.</param>
        /// <param name="sentAfter">Filter bundles sent after this date.</param>
        /// <param name="completedBefore">Filter bundles completed before this date.</param>
        /// <param name="completedAfter">Filter bundles completed after this date.</param>
        /// <param name="ordering">The ordering of results.</param>
        /// <param name="status">Filter by bundle status.</param>
        /// <param name="statusIn">Filter by multiple statuses.</param>
        /// <param name="tagIn">Filter by multiple tags.</param>
        /// <returns>A request object that can be executed to retrieve bundles.</returns>
        public virtual ListBundleRequest ListBundles(
            int? page,
            int? per_page,
            string search,
            string tag,
            string template,
            DateTime? createdBefore,
            DateTime? createdAfter,
            DateTime? sentBefore,
            DateTime? sentAfter,
            DateTime? completedBefore,
            DateTime? completedAfter,
            BundleOrdering? ordering,
            BundleStatus? status,
            IList<BundleStatus> statusIn = null,
            IList<string> tagIn = null
            )
        {
            return new ListBundleRequest(service, page, per_page, search, tag, template, createdAfter, createdBefore,sentAfter, sentBefore, completedAfter, completedBefore, ordering, status, statusIn, tagIn);
        }

        public class ListBundleRequest : BlueinkClientBaseService<IList<Blueink.Client.Net.v2.ResponseModel.Bundle>>
        {
            public ListBundleRequest(IClientService service)
                : base(service)
            {
                this.Page = 1;
                this.PerPage = 50;
            }
            public ListBundleRequest(IClientService service, 
                int? page, 
                int? per_page,
                string search,
                string tag,
                string template,
                DateTime? createdBefore,
                DateTime? createdAfter,
                DateTime? sentBefore,
                DateTime? sentAfter,
                DateTime? completedBefore,
                DateTime? completedAfter,
                BundleOrdering? ordering,
                BundleStatus? status,
                IList<BundleStatus> statusIn = null,
                IList<string> tagIn = null)
                : base(service)
            {
                this.Page = page;
                this.PerPage = per_page;
                this.Search = search;
                this.Tag = tag;
                this.Status = status;
                this.StatusIn = statusIn;
                this.TagIn = tagIn;
                this.Template = template;
                this.CreatedBefore = createdBefore;
                this.CreatedAfter = createdAfter;
                this.SentBefore = sentBefore;
                this.SentAfter = sentAfter;
                this.CompletedBefore = completedBefore;
                this.CompletedAfter = completedAfter;
            }

            #region Pagination Support
            public virtual Nullable<int> Page { get; set; }
            public virtual Nullable<int> PerPage { get; set; }
            #endregion

            #region Query Parameters
            public virtual string Search { get; set; }
            public virtual string Tag { get; set; }
            public virtual string Template { get; set; }
            public virtual DateTime? CreatedBefore { get; set; }
            public virtual DateTime? CreatedAfter { get; set; }
            public virtual DateTime? SentBefore { get; set; }
            public virtual DateTime? SentAfter { get; set; }
            public virtual DateTime? CompletedBefore { get; set; }
            public virtual DateTime? CompletedAfter { get; set; }
            public virtual BundleOrdering? Ordering { get; set; }
            public virtual BundleStatus? Status { get; set; }
            public virtual IList<BundleStatus> StatusIn { get; set; }
            public virtual IList<string> TagIn { get; set; }
            #endregion

            public override string BuildUriRequest()
            {
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                if (!String.IsNullOrWhiteSpace(Search))
                    list.Add(new KeyValuePair<string, string>("search", this.Search));
                if (!String.IsNullOrWhiteSpace(Tag))
                    list.Add(new KeyValuePair<string, string>("tag", this.Tag));
                if (!String.IsNullOrWhiteSpace(Template))
                    list.Add(new KeyValuePair<string, string>("template", this.Template));
                if (CreatedAfter.HasValue)
                    list.Add(new KeyValuePair<string, string>("created_after",this.CreatedAfter.Value.ToString("yyyy-MM-dd")));
                if (CreatedBefore.HasValue)
                    list.Add(new KeyValuePair<string, string>("created_before", this.CreatedBefore.Value.ToString("yyyy-MM-dd")));

                if (SentAfter.HasValue)
                    list.Add(new KeyValuePair<string, string>("sent_after", this.SentAfter.Value.ToString("yyyy-MM-dd")));
                if (SentBefore.HasValue)
                    list.Add(new KeyValuePair<string, string>("sent_before", this.SentBefore.Value.ToString("yyyy-MM-dd")));

                if (CompletedAfter.HasValue)
                    list.Add(new KeyValuePair<string, string>("completed_after", this.CompletedAfter.Value.ToString("yyyy-MM-dd")));
                if (CompletedBefore.HasValue)
                    list.Add(new KeyValuePair<string, string>("completed_before", this.CompletedBefore.Value.ToString("yyyy-MM-dd")));

                if (Ordering.HasValue)
                    list.Add(new KeyValuePair<string, string>("ordering", EnumTypeHelper.ConvertBundleOrderingToString(Ordering.Value)));

                if (Status.HasValue)
                    list.Add(new KeyValuePair<string, string>("status", EnumTypeHelper.ConvertBundleStatusToString(this.Status.Value)));

                list.Add(new KeyValuePair<string, string>("page", this.Page.HasValue ? this.Page.Value.ToString() : "1"));
                list.Add(new KeyValuePair<string, string>("per_page", this.PerPage.HasValue ? this.PerPage.Value.ToString() : "50"));

                if (StatusIn != null)
                {
                    StringBuilder sb = new StringBuilder("");
                    foreach (var status in StatusIn)
                    {
                        if (StatusIn.IndexOf(status) != 0)
                            sb.Append(",");
                        sb.Append(EnumTypeHelper.ConvertBundleStatusToString(status));
                    }
                    list.Add(new KeyValuePair<string, string>("status_in", sb.ToString()));
                }

                if (TagIn != null)
                {
                    StringBuilder sb = new StringBuilder("");
                    foreach (var tag in TagIn)
                    {
                        if (TagIn.IndexOf(tag) != 0)
                            sb.Append(",");
                        sb.Append(tag);
                    }                    
                    list.Add(new KeyValuePair<string, string>("tag_in", sb.ToString()));
                }

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
                get { return "bundles/"; }
            }
            public override string HttpMethod
            {
                get { return "get"; }
            }
        }

        public virtual ListBundleEventsRequest ListBundleEvents(string bundleSlug)
        {
            return new ListBundleEventsRequest(service, bundleSlug);
        }

        public class ListBundleEventsRequest : BlueinkClientBaseService<IList<Blueink.Client.Net.v2.ResponseModel.BundleEvent>>
        {           
            public ListBundleEventsRequest(IClientService service,string bundleSlug)
                : base(service)
            {
                this.BundleSlug = bundleSlug;
            }

            #region Query Parameters
            public virtual string BundleSlug { get; set; }
            #endregion
            public override string BuildUriRequest()
            {
                return RestPath;
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
                get { return String.Format("bundles/{0}/events/",BundleSlug); }
            }

            public override string HttpMethod
            {
                get { return "get"; }
            }
        }

        public virtual ListBundleFilesRequest ListBundleFiles(string bundleSlug)
        {
            return new ListBundleFilesRequest(service, bundleSlug);
        }

        public class ListBundleFilesRequest : BlueinkClientBaseService<IList<Blueink.Client.Net.v2.ResponseModel.BundleFile>>
        {
            public ListBundleFilesRequest(IClientService service, string bundleSlug)
                : base(service)
            {
                this.BundleSlug = bundleSlug;
            }

            #region Query Parameters
            public virtual string BundleSlug { get; set; }
            #endregion
            public override string BuildUriRequest()
            {
                return RestPath;
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
                get { return String.Format("bundles/{0}/files/", BundleSlug); }
            }

            public override string HttpMethod
            {
                get { return "get"; }
            }
        }

        public virtual ListBundleDataRequest ListBundleData(string bundleSlug)
        {
            return new ListBundleDataRequest(service, bundleSlug);
        }

        public class ListBundleDataRequest : BlueinkClientBaseService<IList<Blueink.Client.Net.v2.ResponseModel.BundleData>>
        {
            public ListBundleDataRequest(IClientService service, string bundleSlug)
                : base(service)
            {
                this.BundleSlug = bundleSlug;
            }

            #region Query Parameters
            public virtual string BundleSlug { get; set; }
            #endregion
            public override string BuildUriRequest()
            {
                return RestPath;
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
                get { return String.Format("bundles/{0}/data/", BundleSlug); }
            }

            public override string HttpMethod
            {
                get { return "get"; }
            }
        }

        public virtual GetBundleRequest GetBundle(string bundleSlug, BundleIncludeFlag include = BundleIncludeFlag.None)
        {
            return new GetBundleRequest(service, bundleSlug, include);
        }

        public class GetBundleRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.Bundle>
        {
            public GetBundleRequest(IClientService service, string bundleSlug, BundleIncludeFlag include = BundleIncludeFlag.None)
                : base(service)
            {
                BundleSlug = bundleSlug;
                Include = include;
            }

            public virtual string BundleSlug { get; private set; }
            public virtual BundleIncludeFlag Include { get; set; }
            public override IEnumerable<KeyValuePair<string, string>> BuildRequestBody()
            {
                return base.BuildRequestBody();
            }

            public override string BuildUriRequest()
            {
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                if (Include != BundleIncludeFlag.None)
                {
                    string sInclude = String.Empty;
                    if (Include.HasFlag(BundleIncludeFlag.Data))
                    {
                        sInclude = "data";
                    }
                    if (Include.HasFlag(BundleIncludeFlag.Events))
                    {
                        sInclude += String.Format("{0}", String.IsNullOrEmpty(sInclude) ? "events" : ",events");
                    }
                    if (Include.HasFlag(BundleIncludeFlag.Files))
                    {
                        sInclude += String.Format("{0}", String.IsNullOrEmpty(sInclude) ? "files" : ",files");
                    }
                    list.Add(new KeyValuePair<string, string>("include", sInclude));
                }

                StringBuilder builder = new StringBuilder(RestPath);
                if (list.Count > 0)
                {
                    builder.Append(builder.ToString().Contains("?") ? "&" : "?");
                    builder.Append(String.Join("&", list.Select(x => String.IsNullOrEmpty(x.Value) ? Uri.EscapeDataString(x.Key) : String.Format("{0}={1}", Uri.EscapeDataString(x.Key), Uri.EscapeDataString(x.Value))).ToArray()));
                }

                return builder.ToString();
            }

            public override string MethodName
            {
                get { return "get"; }
            }

            public override string RestPath
            {
                get { return String.Format("bundles/{0}/", BundleSlug); }
            }

            public override string HttpMethod
            {
                get { return "get"; }
            }

        }

        public virtual DeleteBundleRequest DeleteBundle(string bundleSlug)
        {
            return new DeleteBundleRequest(service, bundleSlug);
        }

        public class DeleteBundleRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.Bundle>
        {
            public DeleteBundleRequest(IClientService service, string bundleSlug)
                : base(service)
            {
                BundleSlug = bundleSlug;
            }

            public virtual string BundleSlug { get; private set; }

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
                get { return String.Format("bundles/{0}/", BundleSlug); }
            }

            public override string HttpMethod
            {
                get { return "delete"; }
            }
        }

        public virtual CancelBundleRequest CancelBundle(string bundleSlug)
        {
            return new CancelBundleRequest(service, bundleSlug);
        }

        public class CancelBundleRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.Bundle>
        {
            public CancelBundleRequest(IClientService service, string bundleSlug)
                : base(service)
            {
                BundleSlug = bundleSlug;
            }

            public virtual string BundleSlug { get; private set; }

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
                get { return "put"; }
            }

            public override string RestPath
            {
                get { return String.Format("bundles/{0}/cancel/", BundleSlug); }
            }

            public override string HttpMethod
            {
                get { return "put"; }
            }

        }

        public virtual ExpireBundleRequest ExpireBundle(string bundleSlug)
        {
            return new ExpireBundleRequest(service, bundleSlug);
        }

        public class ExpireBundleRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.Bundle>
        {
            public ExpireBundleRequest(IClientService service, string bundleSlug)
                : base(service)
            {
                BundleSlug = bundleSlug;
            }

            public virtual string BundleSlug { get; private set; }

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
                get { return "put"; }
            }

            public override string RestPath
            {
                get { return String.Format("bundles/{0}/expire/", BundleSlug); }
            }

            public override string HttpMethod
            {
                get { return "put"; }
            }

        }

        public virtual GenerateInterimBundleFilesRequest GenerateInterimBundleFiles(string bundleSlug)
        {
            return new GenerateInterimBundleFilesRequest(service, bundleSlug);
        }

        public class GenerateInterimBundleFilesRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.Bundle>
        {
            public GenerateInterimBundleFilesRequest(IClientService service, string bundleSlug)
                : base(service)
            {
                BundleSlug = bundleSlug;
            }

            public virtual string BundleSlug { get; private set; }

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
                get { return "put"; }
            }

            public override string RestPath
            {
                get { return String.Format("bundles/{0}/files/", BundleSlug); }
            }

            public override string HttpMethod
            {
                get { return "put"; }
            }

        }

        public virtual BundleAddTagRequest BundleAddTag(string bundleSlug)
        {
            return new BundleAddTagRequest(service, bundleSlug);
        }

        public virtual BundleAddTagRequest BundleAddTag(string bundleSlug,IList<string> tags = null)
        {
            return new BundleAddTagRequest(service, bundleSlug, tags);
        }

        public class BundleAddTagRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.Bundle>
        {
            public BundleAddTagRequest(IClientService service, string bundleSlug)
                : base(service)
            {
                BundleSlug = bundleSlug;
            }

            public BundleAddTagRequest(IClientService service, string bundleSlug, IList<string> tags = null)
                : base(service)
            {
                BundleSlug = bundleSlug;
                Tags = tags;
            }

            public virtual string BundleSlug { get; private set; }
            public virtual IList<string> Tags { get; set; }
            public override IEnumerable<KeyValuePair<string, string>> BuildRequestBody()
            {
                var list = new List<KeyValuePair<string, string>>();
                if (Tags != null && Tags.Count > 0)
                {
                    StringBuilder builder = new StringBuilder("[");
                    foreach (var tag in Tags)
                    {
                        if (Tags.IndexOf(tag) != 0)
                            builder.Append(",");
                        builder.Append(tag);
                    }
                    builder.Append("]");
                    list.Add(new KeyValuePair<string, string>("tags", builder.ToString()));
                }
                else
                    list.Add(new KeyValuePair<string, string>("tags", "null"));

                return list;
            }

            public override string BuildUriRequest()
            {
                return RestPath;
            }

            public override string MethodName
            {
                get { return "put"; }
            }

            public override string RestPath
            {
                get { return String.Format("bundles/{0}/add_tags/", BundleSlug); }
            }

            public override string HttpMethod
            {
                get { return "put"; }
            }

        }

        public virtual BundleRemoveTagRequest BundleRemoveTag(string bundleSlug)
        {
            return new BundleRemoveTagRequest(service, bundleSlug);
        }

        public virtual BundleRemoveTagRequest BundleRemoveTag(string bundleSlug, IList<string> tags = null)
        {
            return new BundleRemoveTagRequest(service, bundleSlug, tags);
        }

        public class BundleRemoveTagRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.Bundle>
        {
            public BundleRemoveTagRequest(IClientService service, string bundleSlug)
                : base(service)
            {
                BundleSlug = bundleSlug;
            }

            public BundleRemoveTagRequest(IClientService service, string bundleSlug, IList<string> tags = null)
                : base(service)
            {
                BundleSlug = bundleSlug;
                Tags = tags;
            }

            public virtual string BundleSlug { get; private set; }
            public virtual IList<string> Tags { get; set; }
            public override IEnumerable<KeyValuePair<string, string>> BuildRequestBody()
            {

                var list = new List<KeyValuePair<string, string>>();
                if (Tags != null && Tags.Count > 0)
                {
                    StringBuilder builder = new StringBuilder("[");
                    foreach (var tag in Tags)
                    {
                        if (Tags.IndexOf(tag) != 0)
                            builder.Append(",");
                        builder.Append(tag);
                    }
                    builder.Append("]");
                    list.Add(new KeyValuePair<string, string>("tags", builder.ToString()));
                }
                else
                    list.Add(new KeyValuePair<string, string>("tags", "null"));

                return list;
            }

            public override string BuildUriRequest()
            {
                return RestPath;
            }

            public override string MethodName
            {
                get { return "put"; }
            }

            public override string RestPath
            {
                get { return String.Format("bundles/{0}/remove_tags/", BundleSlug); }
            }

            public override string HttpMethod
            {
                get { return "put"; }
            }

        }

        public virtual BundleCreateRequest CreateBundle(List<RequestModel.Packet> packets, List<RequestModel.IDocument> documents, string label, string emailSubject, string emailMessage, bool isTest = true)
        {
            var bundleHelper = new BundleHelper
            {
                Label = label,
                EmailSubject = emailSubject,
                EmailMessage = emailMessage,
                IsTest = isTest
            };

            if (packets.Count == 0)
                throw new BlueinkApiException(service.Name, "must have a packet")
                {
                    Error = new Error()
                    {
                        Code = ErrorCode.Invalid,
                        Message = "must have a packet",
                        Errors = null
                    }
                };

            foreach (var signer in packets)
            {
                bundleHelper.AddSigner(signer.Name,
                    signer.Email,
                    signer.Phone,
                    signer.DeliverVia,
                    signer.PersonId,
                    signer.AuthSms,
                    signer.AuthSelfie,
                    signer.AuthId,
                    signer.Order,
                    signer.Key);
            }

            if (documents.Count == 0)
                throw new BlueinkApiException(service.Name, "must have a document")
                {
                    Error = new Error()
                    {
                        Code = ErrorCode.Invalid,
                        Message = "must have a document",
                        Errors = null
                    }
                };

            foreach (var document in documents)
            {
                if (document is RequestModel.TemplateRef templateRef)
                {
                    var assignments = new Dictionary<string, string>();
                    foreach (var assignment in templateRef.Assignments)
                    {
                        assignments[assignment.Role] = assignment.Signer;
                    }

                    var initialValues = new Dictionary<string, string>();
                    foreach (var field in templateRef.FieldValues)
                    {
                        initialValues[field.Key] = field.InitialValue;
                    }

                    bundleHelper.AddDocumentTemplate(templateRef.TemplateId, assignments, initialValues);
                }
                else if (document is RequestModel.DocumentRef documentRef)
                {
                    string docKey = String.Empty;
                    if (!String.IsNullOrWhiteSpace(documentRef.FileUrl))
                        docKey = bundleHelper.AddDocumentByUrl(documentRef.Key, documentRef.FileUrl);
                    else
                        docKey = bundleHelper.AddDocumentByB64(documentRef.Key, documentRef.Filename, documentRef.FileBinary64);

                    if (documentRef.Fields != null)
                    {
                        foreach (var field in documentRef.Fields)
                        {
                            bundleHelper.AddField(
                                docKey,
                                field.X,
                                field.Y,
                                field.W,
                                field.H,
                                field.Page,
                                field.Kind,
                                field?.Editors?.Cast<string>().ToList(),
                                field.Label,
                                field.Key,
                                field.VPattern,
                                field.VMin,
                                field.VMax);
                        }                        
                    }

                    if (documentRef.AutoPlacements != null)
                    {
                        foreach (var field in documentRef.AutoPlacements)
                        {
                            bundleHelper.AddAutoplacement(
                                docKey,
                                field.Kind,
                                field.Search,
                                field.W,
                                field.H,
                                field.OffsetX,
                                field.OffsetY,
                                field.Editors?.Cast<string>().ToList());
                        } 
                    }
                }
            }

            return CreateBundleFromHelper(bundleHelper);
        }

        public virtual BundleCreateRequest CreateBundleFromHelper(BundleHelper bundleHelper)
        {
            if (bundleHelper == null) return null;

            return new BundleCreateRequest(service,bundleHelper);
        }

        public class BundleCreateRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.Bundle>
        {
            public BundleCreateRequest(IClientService service,
                 BundleHelper bundleHelper)
                 : base(service)
            {
                BundleHelper = bundleHelper;
            }
            public virtual BundleHelper BundleHelper { get; set; }
            public override void BuildRequestContent(MultipartFormDataContent content)
            {
                base.BuildRequestContent(content);
            }
            public override string BuildJsonRequestBody()
            {
                return Service.SerializeObject(BundleHelper.CompileBundle());                
            }

            public override IEnumerable<KeyValuePair<string, string>> BuildRequestBody()
            {
                return base.BuildRequestBody();
            }

            public override string BuildUriRequest()
            {
                return RestPath;
            }

            public override string PayloadContentType
            {
                get { return "json"; }
            }

            public override string MethodName
            {
                get { return "create"; }
            }

            public override string RestPath
            {
                get { return "bundles/"; }
            }

            public override string HttpMethod
            {
                get { return "post"; }
            }

            public override global::Blueink.Client.Net.v2.ResponseModel.Bundle Execute()
            {
                // here do custom validation before we execute the request
                if (this.BundleHelper == null)
                    throw new BlueinkApiException(Service.Name, "BundleHelper cannot be null")
                    {
                        Error = new Error()
                        {
                            Code = ErrorCode.Invalid,
                            Message = "BundleHelper cannot be null",
                            Errors = null
                        }
                    };

                // check the BundleHelper for packets and documents
                if (this.BundleHelper.Packets.Count == 0)
                    throw new BlueinkApiException(Service.Name, "BundleHelper must have at least one packet")
                    {
                        Error = new Error()
                        {
                            Code = ErrorCode.Invalid,
                            Message = "BundleHelper must have at least one packet",
                            Errors = null
                        }
                    };

                if (this.BundleHelper.Documents.Count == 0)
                    throw new BlueinkApiException(Service.Name, "BundleHelper must have at least one document")
                    {
                        Error = new Error()
                        {
                            Code = ErrorCode.Invalid,
                            Message = "BundleHelper must have at least one document",
                            Errors = null
                        }
                    };

                // Check dodument is ok its either template or document
                foreach (var doc in this.BundleHelper.Documents.Values)
                {
                    if (doc is RequestModel.DocumentRef docRef)
                    {
                        // Document must have at least one field or auto-placement
                        if ((docRef.Fields == null || docRef.Fields.Count == 0)
                            && (docRef.AutoPlacements == null || docRef.AutoPlacements.Count == 0))
                            throw new ArgumentNullException("document must have at least one field or auto-placement");
                    }
                    else if (doc is RequestModel.TemplateRef temRef)
                    { 

                    }
                }

                return base.Execute();
            }
        }

        public virtual BundleCreateUploadFilesRequest CreateBundleUploadPdfFiles(List<RequestModel.Packet> packets, List<RequestModel.IDocument> docs,List<string> files,string label, string emailSubject, string emailMessage, bool isTest = true)
        {
            var bundleHelper = new BundleHelper
            {
                Label = label,
                EmailSubject = emailSubject,
                EmailMessage = emailMessage,
                IsTest = isTest
            };

            if (packets.Count == 0)
                throw new ArgumentException("must have a packet");

            foreach (var signer in packets)
            {
                bundleHelper.AddSigner(signer.Name,
                    signer.Email, 
                    signer.Phone, 
                    signer.DeliverVia,
                    signer.PersonId,
                    signer.AuthSms,
                    signer.AuthSelfie,
                    signer.AuthId,
                    signer.Order,
                    signer.Key);
            }

            if (files.Count == 0)
                throw new ArgumentException("must have a file to upload");

            foreach (var file in files)
            {
                bundleHelper.AddDocumentAndFileToUpload(null,file);
            }

            return new BundleCreateUploadFilesRequest(service,bundleHelper);
        }
        public virtual BundleCreateUploadFilesRequest CreateBundleUploadFilesFromHelper(BundleHelper bundleHelper)
        {
            if (bundleHelper == null)
                throw new ArgumentException("bundleHelper cannot be null");

            return new BundleCreateUploadFilesRequest(service, bundleHelper);
        }
        public class BundleCreateUploadFilesRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.Bundle>
        {
            public BundleCreateUploadFilesRequest(IClientService service,
                 BundleHelper bundleHelper)
                 : base(service)
            {
                BundleHelper = bundleHelper;
            }
            public virtual BundleHelper BundleHelper { get; set; }
            
            public override void BuildRequestContent(MultipartFormDataContent content)
            {
                if (content == null)
                    return;

                var json = Service.SerializeObject(BundleHelper.CompileBundle());
                content.Add(new StringContent(String.Format("bundle_request: {0}",json), Encoding.UTF8, "application/json"));
                
                foreach (var file in this.BundleHelper.Files)
                {
                    string docKey = file.Key;
                    var document = this.BundleHelper.Documents[docKey] as RequestModel.DocumentRef;
                    if (document != null)
                    {
                        var filestream = File.OpenRead(file.Value.Item1);
                        var stream = new StreamContent(filestream);
                        stream.Headers.ContentType = MediaTypeHeaderValue.Parse(file.Value.Item2);
                        stream.Headers.ContentLength = filestream.Length;
                        content.Add(stream, String.Format("file[{0}]", document.FileIndex), Path.GetFileName(file.Value.Item1));                        
                    }
                }                
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

            public override string PayloadContentType
            {
                get { return "formdata"; }
            }

            public override string MethodName
            {
                get { return "create"; }
            }

            public override string RestPath
            {
                get { return "bundles/"; }
            }

            public override string HttpMethod
            {
                get { return "post"; }
            }

            public new global::Blueink.Client.Net.v2.ResponseModel.Bundle Execute()
            {
                // here do custom validation before we execute the request
                if (this.BundleHelper == null)
                    throw new BlueinkApiException(Service.Name,"BundleHelper cannot be null")
                    {
                        Error = new Error()
                        {
                            Code = ErrorCode.Invalid,
                            Message = "BundleHelper cannot be null",
                            Errors = null
                        }
                    };

                // check the BundleHelper for packets and documents
                if (this.BundleHelper.Packets.Count == 0)
                    throw new BlueinkApiException(Service.Name,"BundleHelper must have at least one packet")
                    {
                        Error = new Error()
                        {
                            Code = ErrorCode.Invalid,
                            Message = "BundleHelper must have at least one packet",
                            Errors = null
                        }
                    };

                if (this.BundleHelper.Documents.Count == 0)
                    throw new BlueinkApiException(Service.Name, "BundleHelper must have at least one document")
                    {
                        Error = new Error()
                        {
                            Code = ErrorCode.Invalid,
                            Message = "BundleHelper must have at least one document",
                            Errors = null
                        }
                    };

                // Loop through documents and files make sure they cross reference


                return base.Execute();
            }
        }

        public virtual BundleCreateUsingTemplateRequest CreateBundleFromEnvelopeTemplate(string templateId, string label, string emailSubject, string emailMessage,Dictionary<string,string> fieldValues = null, bool isTest = true)
        {
            var vals = new List<RequestModel.EnvelopeTemplateFieldValue>();
            foreach (var kvp in fieldValues)
            {
                var fieldVal = RequestModel.EnvelopeTemplateFieldValue.Create(kvp.Key, kvp.Value);
                vals.Add(fieldVal);
            }

            var bundleHelper = new BundleHelper
            {
                Label = label,
                EmailSubject = emailSubject,
                EmailMessage = emailMessage,
                IsTest = isTest,
                EnvelopeTemplate = RequestModel.EnvelopeTemplate.Create(templateId, vals.Count > 0 ? vals : null)
            };

            bundleHelper.IsTest = isTest;

            return CreateBundleFromEnvelopeTemplate(bundleHelper);
        }

        public virtual BundleCreateUsingTemplateRequest CreateBundleFromEnvelopeTemplate(BundleHelper bundleHelper)
        {
            if (bundleHelper == null)
                throw new ArgumentNullException("bundleHelper cannot be null");

            return new BundleCreateUsingTemplateRequest(service, bundleHelper);
        }

        public class BundleCreateUsingTemplateRequest : BlueinkClientBaseService<Blueink.Client.Net.v2.ResponseModel.Bundle>
        {
            public BundleCreateUsingTemplateRequest(IClientService service,
                 BundleHelper bundleHelper)
                 : base(service)
            {
                BundleHelper = bundleHelper;
            }
            public virtual BundleHelper BundleHelper { get; set; }
            public override void BuildRequestContent(MultipartFormDataContent content)
            {
                base.BuildRequestContent(content);
            }
            public override string BuildJsonRequestBody()
            {
                return Service.SerializeObject(BundleHelper.CreateBundleForTemplate());
            }

            public override IEnumerable<KeyValuePair<string, string>> BuildRequestBody()
            {
                return base.BuildRequestBody();
            }

            public override string BuildUriRequest()
            {
                return RestPath;
            }

            public override string PayloadContentType
            {
                get { return "json"; }
            }

            public override string MethodName
            {
                get { return "create"; }
            }

            public override string RestPath
            {
                get { return "bundles/"; }
            }

            public override string HttpMethod
            {
                get { return "post"; }
            }
        }
    }
}
