using Blueink.Client.Net.v2.Common;
using Blueink.Client.Net.v2.Model;
using Blueink.Client.Net.v2.RequestModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueink.Client.Net.v2.Helper
{
    public class BundleHelper
    {
        public string Label { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        public string SMSMessage { get; set; }
        public string RequesterName { get; set; }
        public string RequesterEmail { get; set; }
        public string CustomKey { get; set; }
        public string Team { get; set; }
        public string SigningBrand { get; set; }
        public BundleStatus? Status { get; set; }
        public bool IsTest { get; set; }
        public bool InOrder { get; set; }
        public EnvelopeTemplate EnvelopeTemplate { get; set; }
        public List<string> CCEmails { get; } = new List<string>();
        public Dictionary<string, IDocument> Documents { get; } = new Dictionary<string, IDocument>();
        public Dictionary<string, Packet> Packets { get; } = new Dictionary<string, Packet>();
        // Files: tuple Item1 => Filepath , Item2 => ContentType e.g. application/pdf or application/octet-stream 
        public Dictionary<string,Tuple<string,string>> Files { get; } = new Dictionary<string, Tuple<string, string>>();

        public string AddDocumentAndFileToUpload(string key,string filePath, string contentType = "application/pdf")
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException(String.Format("Cannot find File '{0}'", filePath));

            if (String.IsNullOrWhiteSpace(contentType))
                throw new ArgumentNullException("contentType must have a valid type e.g. application/pdf");

            string filename = Path.GetFileName(filePath);
            string docKey = AddDocumentByB64(key,filename, null);

            this.Files[docKey] = new Tuple<string, string>(filePath, contentType);

            return docKey;
        }
        public string AddDocumentByUrl(string key,string url)
        {
            var document = DocumentRef.Create(key);
            document.FileUrl = url;
            this.Documents[document.Key] = document;
            return document.Key;
        }

        public string AddDocumentByPath(string key,string filePath)
        {
            string filename = Path.GetFileName(filePath);

            string b64str = FileHelper.ConvertFileToBase64Encode(filePath);
            
            return AddDocumentByB64(key,filename, b64str);
        }

        public string AddDocumentByFile(string key,FileStream file)
        {
            string filename = file.Name;

            file.Seek(0, SeekOrigin.Begin);
            byte[] fileBytes = new byte[file.Length];
            file.Read(fileBytes, 0, fileBytes.Length);
            file.Flush();

            string b64str = Convert.ToBase64String(fileBytes);

            return AddDocumentByB64(key,filename, b64str);
        }

        public string AddDocumentByB64(string key,string filename, string b64str)
        {
            int fileIndex = this.Files.Count;

            var document = DocumentRef.Create(key);
            document.Filename = filename;
            document.FileBinary64 = b64str;

            // if the b64str is null then we need to set the fileindex
            if (String.IsNullOrEmpty(b64str))
                document.FileIndex = fileIndex;
            else
                document.FileIndex = null;

            Console.WriteLine("doc -- {document.Key}");
            this.Documents[document.Key] = document;
            return document.Key;
        }

        public string AddDocumentByByteArray(string key,string filename, byte[] byteArray)
        {
            using (var ms = new MemoryStream(byteArray))
            {
                byte[] buffer = ms.ToArray();
                string b64str = Convert.ToBase64String(buffer);
                return AddDocumentByB64(key,filename, b64str);
            }
        }

        public string AddDocumentTemplate(
            string templateId,
            Dictionary<string, string> assignments,
            Dictionary<string, string> initialFieldValues)
        {            
            if (this.Documents.ContainsKey(templateId))
            {
                throw new InvalidOperationException($"Document/Template with id {templateId} already added.");
            }

            var assigns = new List<TemplateRefAssignment>();
            foreach (var kvp in assignments)
            {
                var refAssignment = TemplateRefAssignment.Create(kvp.Key, kvp.Value);
                assigns.Add(refAssignment);
            }

            var vals = new List<TemplateRefFieldValue>();
            foreach (var kvp in initialFieldValues)
            {
                var fieldVal = TemplateRefFieldValue.Create(kvp.Key, kvp.Value);
                vals.Add(fieldVal);
            }

            var template = TemplateRef.Create(null, templateId, assigns, vals);          

            this.Documents[template.Key] = template;
            return template.Key;
        }
    
        public void AddCCEmail(string email)
        {
            this.CCEmails.Add(email);
        }

        public void AddAutoplacement(
            string documentKey,
            FieldKind kind,
            string search,
            int w,
            int h,
            int? offsetX,
            int? offsetY,
            List<string> editors
            )
        { 
            if (!this.Documents.ContainsKey(documentKey))
            {
                throw new InvalidOperationException($"No document found with key {documentKey}!");
            }

            var field = AutoPlacement.Create(kind,search,w,h,offsetX,offsetY, editors);
            ((DocumentRef)this.Documents[documentKey]).AddAutoPlacement(field);
        }
        public string AddField(
            string documentKey,
            int x,
            int y,
            int w,
            int h,
            int p,
            FieldKind kind,
            List<string> editors = null,
            string label = null,
            string key = null,
            int? vPattern = null,
            int? vMin = null,
            int? vMax = null
            )
        {
            if (!this.Documents.ContainsKey(documentKey))
            {
                throw new InvalidOperationException($"No document found with key {documentKey}!");
            }

            var field = Field.Create(x, y, w, h, p, kind, key);
            field.Label = label;
            field.VPattern = vPattern;
            field.VMin = vMin;
            field.VMax = vMax;
                
            if (editors != null)
            {
                foreach (var packetKey in editors)
                {
                    field.AddEditor(packetKey);
                }
            }

            ((DocumentRef)this.Documents[documentKey]).AddField(field);
            return field.Key;
        }

        public string AddSigner(
            string name,
            string email,
            string phone,
            DeliveryVia deliveryVia,
            string personId = null,
            bool? authSms = false,
            bool? authSelfie = false,
            bool? authId = false,
            int? order = null,
            string key = null)
        {
            if (String.IsNullOrWhiteSpace(phone) && String.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("Packet must have either an email or phone number");

            var packet = Packet.Create(key, name);
            packet.PersonId = personId;
            packet.Email = email;
            packet.Phone = phone;
            packet.AuthSms = authSms;
            packet.AuthSelfie = authSelfie;
            packet.AuthId = authId;
            packet.DeliverVia = deliveryVia;
            packet.Order = order;
        
            this.Packets[packet.Key] = packet;
            return packet.Key;
        }

        public void AssignRole(string documentKey , string signerKey , string role )
        {
            if (!this.Documents.ContainsKey(documentKey))
                throw new InvalidOperationException($"no document found with key {documentKey}!");

            if (!(this.Documents[documentKey] is TemplateRef))
                throw new InvalidOperationException($"Document found with key {documentKey} is not a Template!");

            if (!this.Packets.ContainsKey(signerKey))
                throw new InvalidOperationException($"Signer {signerKey} does not have a corresponding packet");

            var assignment = TemplateRefAssignment.Create(role,signerKey);
            ((TemplateRef)this.Documents[documentKey]).AddAssignment(assignment);
        }

        public void SetValue(string documentKey, string key, string value)
        {
            if (!this.Documents.ContainsKey(documentKey))
                throw new InvalidOperationException($"no document found with key {documentKey}!");

            if (!(this.Documents[documentKey] is TemplateRef))
                throw new InvalidOperationException($"Document found with key {documentKey} is not a Template!");

            var field_val = TemplateRefFieldValue.Create(key, value);
            ((TemplateRef)this.Documents[documentKey]).AddFieldValue(field_val);
        }

        public void SetEnvelopeTemplate(string templateId, Dictionary<string, string> fieldValues = null)
        {
            var vals = new List<EnvelopeTemplateFieldValue>();
            foreach (var kvp in fieldValues)
            {
                var fieldVal = EnvelopeTemplateFieldValue.Create(kvp.Key, kvp.Value);
                vals.Add(fieldVal);
            }

            var envelopeTemplate = EnvelopeTemplate.Create(templateId,vals.Count > 0 ? vals : null );
            this.EnvelopeTemplate = envelopeTemplate;
        }

        public void AddEnvelopeTemplateFieldValue(string key, string initialValue)
        {
            if (this.EnvelopeTemplate == null)
                throw new ArgumentNullException("No envelope template set. Call SetEnvelopeTemplate() first.");

            var field_val = EnvelopeTemplateFieldValue.Create(key,initialValue);
            this.EnvelopeTemplate.AddFieldValue(field_val);
        }

        public Bundle CompileBundle()
        {
            List<Packet> packets = this.Packets.Values.ToList();
            List<IDocument> documents = this.Documents.Values.ToList();

            var bundle_out = Bundle.Create(packets, documents);
         
            bundle_out.Label = this.Label;
            bundle_out.InOrder = this.InOrder;
            bundle_out.EmailSubject = this.EmailSubject;
            bundle_out.EmailMessage = this.EmailMessage;
            bundle_out.SMSMessage = this.SMSMessage;
            bundle_out.RequesterName = this.RequesterName;
            bundle_out.RequesterEmail = this.RequesterEmail;
            bundle_out.IsTest = this.IsTest;
            bundle_out.CCEmails = new List<string>(this.CCEmails);
            bundle_out.CustomKey = this.CustomKey;
            bundle_out.Team = this.Team;
            bundle_out.SigningBrand = this.SigningBrand;
            bundle_out.Status = this.Status;

            return bundle_out;
        }

        public string ToJson()
        {
            var bundle_out = this.CompileBundle();
            string sJson = "";
            return sJson;
        }

        public Bundle CreateBundleForTemplate()
        {
            if (this.EnvelopeTemplate == null)
                throw new ArgumentNullException("No envelope template set.");

            List<Packet> packets = this.Packets.Values.ToList();

            var bundle_out = Bundle.Create(packets,null);

            bundle_out.Label = this.Label;
            bundle_out.InOrder = this.InOrder;
            bundle_out.EmailSubject = this.EmailSubject;
            bundle_out.EmailMessage = this.EmailMessage;
            bundle_out.SMSMessage = this.SMSMessage;
            bundle_out.RequesterName = this.RequesterName;
            bundle_out.RequesterEmail = this.RequesterEmail;
            bundle_out.IsTest = this.IsTest;
            bundle_out.CCEmails = new List<string>(this.CCEmails);
            bundle_out.CustomKey = this.CustomKey;
            bundle_out.Team = this.Team;
            bundle_out.SigningBrand = this.SigningBrand;
            bundle_out.Status = this.Status;

            bundle_out.EnvelopeTemplate = new EnvelopeTemplate
            {
                TemplateId = this.EnvelopeTemplate.TemplateId,
                FieldValues = new List<EnvelopeTemplateFieldValue>(this.EnvelopeTemplate.FieldValues)
            };

            return bundle_out;
        }

        public Bundle CreateBundle()
        {
            return this.CompileBundle();
        }

        public List<Bundle> CreateBundles()
        {
            return new List<Bundle>() { CreateBundle() };
        }
    }
}
