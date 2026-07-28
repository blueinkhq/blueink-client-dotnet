using Blueink.Client.Net.v2.Helper;
using Blueink.Client.Net.v2.Model;
using Blueink.Client.Net.v2.Resource;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Blueink.Client.Net.v2.Tests
{
    /// <summary>
    /// Unit tests for WebhookResource create/update request bodies. Verifies that the
    /// server-assigned <c>id</c> is never sent (read-only in the API) and that the required
    /// <c>name</c> field is included.
    /// </summary>
    [TestFixture]
    public class WebhookResourceTests
    {
        private const string ValidApiKey = "a1b2c3d4e5f6a1b2c3d4e5f6a1b2c3d4e5f6a1b2c3d4e5f6a1b2c3d4e5f6a1b2c3d4e5f6a1b2c3d4";

        private static IList<EventType> Events()
        {
            return new List<EventType> { EventType.BundleComplete };
        }

        private static IList<Blueink.Client.Net.v2.ResponseModel.WebhookExtraHeader> NoHeaders()
        {
            return new List<Blueink.Client.Net.v2.ResponseModel.WebhookExtraHeader>();
        }

        private static IDictionary<string, string> BodyMap(
            IEnumerable<KeyValuePair<string, string>> body)
        {
            return body.ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        #region Create

        [Test]
        public void Create_DoesNotSendId_AndSendsName()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var request = service.WebhookResource.Create(
                    "My Hook", "https://example.com/hook", true, true, Events(), NoHeaders());

                var body = BodyMap(request.BuildRequestBody());

                Assert.IsFalse(body.ContainsKey("id"), "id must not be sent (read-only).");
                Assert.AreEqual("My Hook", body["name"]);
                Assert.AreEqual("https://example.com/hook", body["url"]);
                Assert.AreEqual("urlencodedformdata", request.PayloadContentType, "body must be sent as form data, not empty json.");
            }
        }

        [Test]
        public void Create_ThrowsValidation_WhenNameMissing()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var request = service.WebhookResource.Create(
                    null, "https://example.com/hook", true, true, Events(), NoHeaders());

                Assert.Throws<BlueinkValidationException>(() => request.BuildRequestBody().ToList());
            }
        }

        [Test]
        public void Create_DoesNotThrow_WhenListsNull()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var request = service.WebhookResource.Create(
                    "My Hook", "https://example.com/hook", true, true, null, null);

                var body = BodyMap(request.BuildRequestBody());

                Assert.AreEqual("[]", body["event_types"], "null event types serialize to an empty array.");
                Assert.AreEqual("[]", body["extra_headers"], "null extra headers serialize to an empty array.");
            }
        }

        #endregion

        #region Update (PUT)

        [Test]
        public void Update_DoesNotSendId_AndSendsNameWhenProvided()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var request = service.WebhookResource.Update(
                    "wh-1", "Renamed", "https://example.com/hook", true, true, Events(), NoHeaders());

                var body = BodyMap(request.BuildRequestBody());

                Assert.IsFalse(body.ContainsKey("id"), "id must not be sent (read-only).");
                Assert.AreEqual("Renamed", body["name"]);
                Assert.AreEqual("put", request.HttpMethod);
                Assert.AreEqual("webhooks/wh-1/", request.RestPath);
                Assert.AreEqual("urlencodedformdata", request.PayloadContentType, "body must be sent as form data, not empty json.");
            }
        }

        [Test]
        public void Update_DoesNotThrow_WhenListsNull()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var request = service.WebhookResource.Update(
                    "wh-1", "Renamed", "https://example.com/hook", true, true, null, null);

                var body = BodyMap(request.BuildRequestBody());

                Assert.AreEqual("[]", body["event_types"], "null event types serialize to an empty array.");
                Assert.AreEqual("[]", body["extra_headers"], "null extra headers serialize to an empty array.");
            }
        }

        #endregion

        #region PartialUpdate (PATCH)

        [Test]
        public void PartialUpdate_DoesNotSendId_AndOmitsNameWhenNull()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var request = service.WebhookResource.PartialUpdate(
                    "wh-1", null, "https://example.com/hook", true, true, Events(), NoHeaders());

                var body = BodyMap(request.BuildRequestBody());

                Assert.IsFalse(body.ContainsKey("id"), "id must not be sent (read-only).");
                Assert.IsFalse(body.ContainsKey("name"), "name is omitted when null on PATCH.");
                Assert.AreEqual("patch", request.HttpMethod);
                Assert.AreEqual("urlencodedformdata", request.PayloadContentType, "body must be sent as form data, not empty json.");
            }
        }

        [Test]
        public void PartialUpdate_OmitsUnsetFields()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var request = service.WebhookResource.PartialUpdate("wh-1");
                request.Enabled = false;

                var body = BodyMap(request.BuildRequestBody());

                Assert.IsFalse(body.ContainsKey("name"), "name omitted when unset on PATCH.");
                Assert.IsFalse(body.ContainsKey("url"), "url omitted when unset on PATCH.");
                Assert.IsFalse(body.ContainsKey("json"), "json omitted when unset on PATCH.");
                Assert.IsFalse(body.ContainsKey("event_types"), "event_types omitted when unset on PATCH.");
                Assert.IsFalse(body.ContainsKey("extra_headers"), "extra_headers omitted when unset on PATCH.");
                Assert.AreEqual("False", body["enabled"], "explicitly set field is sent.");
            }
        }

        #endregion

        #region ExtraHeader create/update

        [Test]
        public void CreateHeader_DoesNotSendId()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var request = service.WebhookResource.CreateHeader("wh-1", "X-Custom", "abc", 1);

                var body = BodyMap(request.BuildRequestBody());

                Assert.IsFalse(body.ContainsKey("id"), "id must not be sent (read-only).");
                Assert.AreEqual("wh-1", body["webhook"]);
                Assert.AreEqual("X-Custom", body["name"]);
                Assert.AreEqual("abc", body["value"]);
                Assert.AreEqual("urlencodedformdata", request.PayloadContentType, "body must be sent as form data, not empty json.");
            }
        }

        [Test]
        public void UpdateHeader_DoesNotSendId()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var request = service.WebhookResource.UpdateHeader(
                    "eh-1", "wh-1", "X-Custom", "abc", 1);

                var body = BodyMap(request.BuildRequestBody());
                Assert.IsFalse(body.ContainsKey("id"));
                Assert.AreEqual("put", request.HttpMethod);
                Assert.AreEqual("webhooks/headers/eh-1/", request.RestPath);
                Assert.AreEqual("urlencodedformdata", request.PayloadContentType, "body must be sent as form data, not empty json.");
            }
        }

        [Test]
        public void PartialUpdateHeader_DoesNotSendId()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var request = service.WebhookResource.PartialUpdateHeader(
                    "eh-1", "wh-1", "X-Custom", "abc", 1);

                var body = BodyMap(request.BuildRequestBody());
                Assert.IsFalse(body.ContainsKey("id"));
                Assert.AreEqual("patch", request.HttpMethod);
                Assert.AreEqual("urlencodedformdata", request.PayloadContentType, "body must be sent as form data, not empty json.");
            }
        }

        [Test]
        public void PartialUpdateHeader_OmitsUnsetFields()
        {
            using (var service = new BlueinkService(ValidApiKey))
            {
                var request = service.WebhookResource.PartialUpdateHeader("eh-1");
                request.Value = "abc";

                var body = BodyMap(request.BuildRequestBody());
                Assert.IsFalse(body.ContainsKey("webhook"), "webhook omitted when unset on PATCH.");
                Assert.IsFalse(body.ContainsKey("name"), "name omitted when unset on PATCH.");
                Assert.IsFalse(body.ContainsKey("order"), "order omitted when unset on PATCH.");
                Assert.AreEqual("abc", body["value"], "explicitly set field is sent.");
            }
        }

        #endregion

        #region EventType conversion

        [Test]
        public void EnumTypeHelper_ConvertsNewEventTypes()
        {
            Assert.AreEqual("bundle_signer_reassigned",
                EnumTypeHelper.ConvertEventTypeToString(EventType.BundleSignerReassigned));
            Assert.AreEqual("packet_declined",
                EnumTypeHelper.ConvertEventTypeToString(EventType.PacketDeclined));

            Assert.AreEqual(EventType.BundleSignerReassigned,
                EnumTypeHelper.ConvertStringToEventType("bundle_signer_reassigned"));
            Assert.AreEqual(EventType.PacketDeclined,
                EnumTypeHelper.ConvertStringToEventType("packet_declined"));
        }

        #endregion
    }
}
