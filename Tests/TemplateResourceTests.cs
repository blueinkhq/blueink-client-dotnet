using Blueink.Client.Net.v2.RequestModel;
using Blueink.Client.Net.v2.ResponseModel;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Blueink.Client.Net.v2.Tests
{
    /// <summary>
    /// Unit tests for TemplateResource 2.16 endpoints: preparation session,
    /// metadata PATCH, and metadata-filtered listing.
    /// </summary>
    [TestFixture]
    public class TemplateResourceTests
    {
        private const string ValidApiKey = "a1b2c3d4e5f6a1b2c3d4e5f6a1b2c3d4e5f6a1b2c3d4e5f6a1b2c3d4e5f6a1b2c3d4e5f6a1b2c3d4";

        #region CreateTemplatePreparationSession

        [Test]
        public void CreateTemplatePreparationSession_WithNullRequest_ThrowsArgumentNullException()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                Assert.Throws<ArgumentNullException>(() =>
                    service.TemplateResource.CreateTemplatePreparationSession(null));
            }
        }

        [Test]
        public void CreateTemplatePreparationSession_ReturnsRequest_WithCorrectRestPathAndMethod()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var prep = new TemplatePreparationSessionRequest();

                var request = service.TemplateResource.CreateTemplatePreparationSession(prep);

                Assert.AreSame(prep, request.Request);
                Assert.AreEqual("templates/preparation_session/", request.RestPath);
                Assert.AreEqual("post", request.HttpMethod);
            }
        }

        [Test]
        public void CreateTemplatePreparationSession_SerializesMetadataAndTemplateId()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var prep = new TemplatePreparationSessionRequest
                {
                    TemplateId = "tmpl-1",
                    Metadata = new Dictionary<string, object> { { "region", "us" } }
                };
                var request = service.TemplateResource.CreateTemplatePreparationSession(prep);

                var json = request.BuildJsonRequestBody();

                Assert.That(json, Does.Contain("\"template_id\":\"tmpl-1\""));
                Assert.That(json, Does.Contain("\"metadata\":{\"region\":\"us\"}"));
            }
        }

        [Test]
        public void CreateTemplatePreparationSession_SerializesAllowedDataFlowTags()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var prep = new TemplatePreparationSessionRequest
                {
                    AllowedDataFlowTags = new List<string> { "customer_name", "acme:*" }
                };
                var request = service.TemplateResource.CreateTemplatePreparationSession(prep);

                var json = request.BuildJsonRequestBody();

                Assert.That(json, Does.Contain("\"allowed_data_flow_tags\""));
                Assert.That(json, Does.Contain("\"customer_name\""));
                Assert.That(json, Does.Contain("\"acme:*\""));
            }
        }

        #endregion

        #region DeleteTemplate

        [Test]
        public void DeleteTemplate_ReturnsRequest_WithCorrectRestPathAndMethod()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var request = service.TemplateResource.DeleteTemplate("tmpl-1");

                Assert.AreEqual("templates/tmpl-1/", request.RestPath);
                Assert.AreEqual("delete", request.HttpMethod);
            }
        }

        #endregion

        #region UpdateTemplateMetadata (PATCH)

        [Test]
        public void UpdateTemplateMetadata_ReturnsRequest_WithCorrectRestPathAndMethod()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var metadata = new Dictionary<string, object> { { "k", "v" } };

                var request = service.TemplateResource.UpdateTemplateMetadata("tmpl-1", metadata);

                Assert.AreEqual("templates/tmpl-1/", request.RestPath);
                Assert.AreEqual("patch", request.HttpMethod);
                Assert.That(request.BuildJsonRequestBody(), Does.Contain("\"metadata\":{\"k\":\"v\"}"));
            }
        }

        [Test]
        public void UpdateTemplateMetadata_ThrowsValidation_WhenMetadataNull()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                Assert.Throws<BlueinkValidationException>(
                    () => service.TemplateResource.UpdateTemplateMetadata("tmpl-1", null));
            }
        }

        #endregion

        #region DocumentTemplate is_valid (APIv2 2.23.0)

        [Test]
        public void DocumentTemplate_DeserializesIsValid()
        {
            var present = JsonConvert.DeserializeObject<DocumentTemplate>(
                "{\"id\":\"tmpl-1\",\"is_valid\":true}");
            Assert.IsTrue(present.IsValid.HasValue);
            Assert.IsTrue(present.IsValid.Value);

            var invalid = JsonConvert.DeserializeObject<DocumentTemplate>(
                "{\"id\":\"tmpl-1\",\"is_valid\":false}");
            Assert.IsTrue(invalid.IsValid.HasValue);
            Assert.IsFalse(invalid.IsValid.Value);

            var omitted = JsonConvert.DeserializeObject<DocumentTemplate>(
                "{\"id\":\"tmpl-1\"}");
            Assert.IsFalse(omitted.IsValid.HasValue);
        }

        #endregion

        #region ListTemplateByMetadata (query filter)

        [Test]
        public void ListTemplateByMetadata_BuildsMetadataQueryString()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var metadata = new Dictionary<string, string> { { "region", "us" } };

                var request = service.TemplateResource.ListTemplateByMetadata(metadata);

                var uri = request.BuildUriRequest();

                Assert.That(uri, Does.StartWith("templates/"));
                Assert.That(uri, Does.Contain("metadata%5Bregion%5D=us"));
            }
        }

        #endregion
    }
}
