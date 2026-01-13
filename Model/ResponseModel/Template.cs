using System;
using System.Collections.Generic;
using Blueink.Client.Net.v2.Serializer;

namespace Blueink.Client.Net.v2.ResponseModel
{
    public class DocumentTemplateRole
    {
        [Newtonsoft.Json.JsonPropertyAttribute("key")]
        public virtual string Key { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("label")]
        public virtual string Label { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("order")]
        public virtual int Order { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("auth_sms")]
        public virtual bool AuthenticateSMS { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("auth_selfie")]
        public virtual bool AuthenticateSelfie { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("auth_id")]
        public virtual bool AuthenticateId { get; set; }        
    }
    public class DocumentTemplateField
    {
        [Newtonsoft.Json.JsonPropertyAttribute("kind")]
        public virtual string Kind { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("label")]
        public virtual string Label { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("required")]
        public virtual bool Required { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("page")]
        public virtual int Page { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("x"),Newtonsoft.Json.JsonRequired]
        public virtual int X { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("y"), Newtonsoft.Json.JsonRequired]
        public virtual int Y { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("w"), Newtonsoft.Json.JsonRequired]
        public virtual int W { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("h"), Newtonsoft.Json.JsonRequired]
        public virtual int H { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("v_pattern")]
        public virtual int V_Pattern { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("v_min")]
        public virtual int V_Min { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("v_max")]
        public virtual int V_Max { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("v_attachment_types")]
        public virtual IList<string> V_Attachment_Types {get;set;}
        [Newtonsoft.Json.JsonPropertyAttribute("default_value")]
        [Newtonsoft.Json.JsonConverter(typeof(StringOrBooleanOrNumberConverter))]
        public virtual object DefaultValue { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("editor_roles")]
        public virtual IList<string> EditorRoles { get; set; }
    }

    public class DocumentTemplate
    {
        [Newtonsoft.Json.JsonPropertyAttribute("is_shared")]
        public virtual bool IsShared { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("name")]
        public virtual string Name { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("file_url")]
        public virtual string FileUrl { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("roles")]
        public virtual IList<DocumentTemplateRole> Roles { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("fields")]
        public virtual IList<DocumentTemplateField> Fields { get; set; }
    }

    public class EnvelopeDocumentTemplate
    {
        [Newtonsoft.Json.JsonPropertyAttribute("id")]
        public virtual string Id { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("created")]
        public virtual DateTime Created { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("is_shared")]
        public virtual bool IsShared { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("is_portal")]
        public virtual bool IsPortal { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("name")]
        public virtual string Name { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("description")]
        public virtual string Description { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("file_url")]
        public virtual string FileUrl { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("allow_direct_signing")]
        public virtual bool AllowDirectSigning { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("smart_link_url")]
        public virtual string SmartLinkUrl { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("signers")]
        public virtual IList<DocumentTemplateRole> Signers { get; set; }
        [Newtonsoft.Json.JsonPropertyAttribute("fields")]
        public virtual IList<DocumentTemplateField> Fields { get; set; }
    }


}
