using Blueink.Client.Net.v2.Helper;
using Blueink.Client.Net.v2.RequestModel;
using Blueink.Client.Net.v2.Serializer;
using NUnit.Framework;
using System;

namespace Blueink.Client.Net.v2.Tests
{
    /// <summary>
    /// Unit tests for BundleHelper covering HTML-backed documents and the Expires field.
    /// </summary>
    [TestFixture]
    public class BundleHelperTests
    {
        #region AddDocumentByHtml Tests

        [Test]
        public void AddDocumentByHtml_SetsFileHtmlAndFilename()
        {
            // Arrange
            var helper = new BundleHelper();

            // Act
            var key = helper.AddDocumentByHtml("doc1", "agreement.html", "<p>Hello</p>");

            // Assert
            Assert.AreEqual("doc1", key);
            Assert.IsTrue(helper.Documents.ContainsKey("doc1"));
            var doc = (DocumentRef)helper.Documents["doc1"];
            Assert.AreEqual("<p>Hello</p>", doc.FileHtml);
            Assert.AreEqual("agreement.html", doc.Filename);
            Assert.IsNull(doc.FileBinary64);
            Assert.IsNull(doc.FileIndex);
        }

        [Test]
        public void AddDocumentByHtml_WithNullKey_GeneratesKey()
        {
            // Arrange
            var helper = new BundleHelper();

            // Act
            var key = helper.AddDocumentByHtml(null, "agreement.html", "<p>Hi</p>");

            // Assert
            Assert.IsFalse(String.IsNullOrWhiteSpace(key));
            Assert.IsTrue(helper.Documents.ContainsKey(key));
        }

        #endregion

        #region Expires Tests

        [Test]
        public void CompileBundle_PropagatesExpiresToBundle()
        {
            // Arrange
            var expires = new DateTime(2030, 1, 15, 10, 30, 0, DateTimeKind.Utc);
            var helper = new BundleHelper { Expires = expires };

            // Act
            var bundle = helper.CompileBundle();

            // Assert
            Assert.AreEqual(expires, bundle.Expires);
        }

        [Test]
        public void CompileBundle_WithExpires_SerializesIso8601()
        {
            // Arrange
            var helper = new BundleHelper
            {
                Expires = new DateTime(2030, 1, 15, 10, 30, 0, DateTimeKind.Utc)
            };
            var bundle = helper.CompileBundle();

            // Act
            var json = NewtonsoftJsonSerializer.Instance.Serialize(bundle);

            // Assert - default Newtonsoft DateTime serialization is ISO-8601
            Assert.That(json, Does.Contain("\"expires\":\"2030-01-15T10:30:00Z\""));
        }

        [Test]
        public void CompileBundle_WithoutExpires_OmitsExpires()
        {
            // Arrange
            var helper = new BundleHelper();
            var bundle = helper.CompileBundle();

            // Act
            var json = NewtonsoftJsonSerializer.Instance.Serialize(bundle);

            // Assert - NullValueHandling.Ignore omits the property when null
            Assert.That(json, Does.Not.Contain("expires"));
            Assert.IsNull(bundle.Expires);
        }

        #endregion
    }
}
