using Blueink.Client.Net.v2.RequestModel;
using Blueink.Client.Net.v2.Serializer;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Blueink.Client.Net.v2.Tests
{
    /// <summary>
    /// Unit tests for BundleResource document routing and the bundle preparation session endpoint.
    /// </summary>
    [TestFixture]
    public class BundleResourceTests
    {
        private const string ValidApiKey = "a1b2c3d4e5f6a1b2c3d4e5f6a1b2c3d4e5f6a1b2c3d4e5f6a1b2c3d4e5f6a1b2c3d4e5f6a1b2c3d4";

        private static List<Packet> OneSigner()
        {
            var packet = Packet.Create("signer1", "Test Signer");
            packet.Email = "test@example.com";
            return new List<Packet> { packet };
        }

        #region file_b64 / file_html routing

        [Test]
        public void CreateBundle_WithBothFileB64AndFileHtml_ThrowsBlueinkApiException()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var doc = DocumentRef.Create("doc1");
                doc.Filename = "agreement.html";
                doc.FileBinary64 = "YWJj";
                doc.FileHtml = "<p>Hello</p>";
                var documents = new List<IDocument> { doc };

                var ex = Assert.Throws<BlueinkApiException>(() =>
                    service.BundleResource.CreateBundle(OneSigner(), documents, "label", "subject", "message"));
                Assert.That(ex.Message, Does.Contain("file_b64 or file_html"));
            }
        }

        [Test]
        public void CreateBundle_WithFileHtmlOnly_RoutesToHtmlDocument()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var doc = DocumentRef.Create("doc1");
                doc.Filename = "agreement.html";
                doc.FileHtml = "<p>Hello</p>";
                var documents = new List<IDocument> { doc };

                var request = service.BundleResource.CreateBundle(OneSigner(), documents, "label", "subject", "message");

                var compiled = (DocumentRef)request.BundleHelper.Documents["doc1"];
                Assert.AreEqual("<p>Hello</p>", compiled.FileHtml);
                Assert.IsNull(compiled.FileBinary64);
            }
        }

        #endregion

        #region CreateBundlePreparationSession

        [Test]
        public void CreateBundlePreparationSession_WithNullRequest_ThrowsArgumentNullException()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                Assert.Throws<ArgumentNullException>(() =>
                    service.BundleResource.CreateBundlePreparationSession(null));
            }
        }

        [Test]
        public void CreateBundlePreparationSession_ReturnsRequest_WithCorrectRestPathAndMethod()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var prep = new PreparationSessionRequest();

                var request = service.BundleResource.CreateBundlePreparationSession(prep);

                Assert.AreSame(prep, request.Request);
                Assert.AreEqual("bundles/preparation_session/", request.RestPath);
                Assert.AreEqual("post", request.HttpMethod);
            }
        }

        [Test]
        public void CreateBundlePreparationSession_BuildJsonRequestBody_SerializesDefaults()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var prep = new PreparationSessionRequest();
                var request = service.BundleResource.CreateBundlePreparationSession(prep);

                var json = request.BuildJsonRequestBody();

                Assert.That(json, Does.Contain("\"upload_pdf\":true"));
                Assert.That(json, Does.Contain("\"allow_search_signers\":false"));
            }
        }

        #endregion

        #region PreparationSessionRequest serialization

        [Test]
        public void PreparationSessionRequest_OmitsOptionalFieldsWhenNull()
        {
            // Arrange
            var prep = new PreparationSessionRequest();

            // Act
            var json = NewtonsoftJsonSerializer.Instance.Serialize(prep);

            // Assert
            Assert.That(json, Does.Not.Contain("template_ids"));
            Assert.That(json, Does.Not.Contain("folder_ids"));
            Assert.That(json, Does.Not.Contain("redirect_url"));
            Assert.That(json, Does.Not.Contain("draft_bundle"));
        }

        [Test]
        public void PreparationSessionRequest_IncludesOptionalFieldsWhenSet()
        {
            // Arrange
            var prep = new PreparationSessionRequest
            {
                TemplateIds = new List<string> { "tmpl-1" },
                RedirectUrl = "https://example.com/callback"
            };

            // Act
            var json = NewtonsoftJsonSerializer.Instance.Serialize(prep);

            // Assert
            Assert.That(json, Does.Contain("\"template_ids\":[\"tmpl-1\"]"));
            Assert.That(json, Does.Contain("\"redirect_url\":\"https://example.com/callback\""));
        }

        #endregion

        #region Draft send / validate / update

        [Test]
        public void ValidateBundle_ReturnsRequest_WithCorrectRestPathAndMethod()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var request = service.BundleResource.ValidateBundle("slug-1");

                Assert.AreEqual("bundles/slug-1/validate/", request.RestPath);
                Assert.AreEqual("put", request.HttpMethod);
            }
        }

        [Test]
        public void SendBundle_ReturnsRequest_WithCorrectRestPathAndMethod()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var request = service.BundleResource.SendBundle("slug-1");

                Assert.AreEqual("bundles/slug-1/send/", request.RestPath);
                Assert.AreEqual("post", request.HttpMethod);
            }
        }

        [Test]
        public void UpdateBundle_WithNullUpdate_ThrowsArgumentNullException()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                Assert.Throws<ArgumentNullException>(() =>
                    service.BundleResource.UpdateBundle("slug-1", null));
            }
        }

        [Test]
        public void UpdateBundle_SerializesSigningBrand_AndUsesPatch()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var update = new BundleUpdate { SigningBrand = "brand-uuid" };
                var request = service.BundleResource.UpdateBundle("slug-1", update);

                Assert.AreEqual("bundles/slug-1/", request.RestPath);
                Assert.AreEqual("patch", request.HttpMethod);
                Assert.That(request.BuildJsonRequestBody(), Does.Contain("\"signing_brand\":\"brand-uuid\""));
            }
        }

        #endregion

        #region tag_values / auth_secrets

        [Test]
        public void Bundle_SerializesTagValues()
        {
            var bundle = new Bundle
            {
                TagValues = new Dictionary<string, object> { { "amount", "100.00" } }
            };

            var json = NewtonsoftJsonSerializer.Instance.Serialize(bundle);

            Assert.That(json, Does.Contain("\"tag_values\":{\"amount\":\"100.00\"}"));
        }

        [Test]
        public void Bundle_OmitsTagValuesWhenNull()
        {
            var bundle = new Bundle();

            var json = NewtonsoftJsonSerializer.Instance.Serialize(bundle);

            Assert.That(json, Does.Not.Contain("tag_values"));
        }

        [Test]
        public void Packet_SerializesAuthSecretsWhenSet()
        {
            var packet = Packet.Create("signer1", "Test Signer");
            packet.AuthSecrets = new Dictionary<string, object> { { "dob", "1990-01-01" } };

            var json = NewtonsoftJsonSerializer.Instance.Serialize(packet);

            Assert.That(json, Does.Contain("auth_secrets"));
        }

        #endregion

        #region Verify

        [Test]
        public void Verify_WithNullHash_ThrowsArgumentNullException()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                Assert.Throws<ArgumentNullException>(() =>
                    service.VerifyResource.Verify(null));
            }
        }

        [Test]
        public void Verify_ReturnsRequest_WithCorrectRestPathMethodAndBody()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var request = service.VerifyResource.Verify("abc123");

                Assert.AreEqual("verify/", request.RestPath);
                Assert.AreEqual("post", request.HttpMethod);
                Assert.That(request.BuildJsonRequestBody(), Does.Contain("\"hash\":\"abc123\""));
            }
        }

        #endregion
    }
}
