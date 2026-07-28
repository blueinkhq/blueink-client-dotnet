using Blueink.Client.Net.v2.RequestModel;
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
