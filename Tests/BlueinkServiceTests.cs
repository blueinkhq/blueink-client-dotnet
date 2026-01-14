using NUnit.Framework;
using System;

namespace Blueink.Client.Net.v2.Tests
{
    /// <summary>
    /// Unit tests for BlueinkService initialization and configuration.
    /// </summary>
    [TestFixture]
    public class BlueinkServiceTests
    {
        private const string ValidApiKey = "a1b2c3d4e5f6a1b2c3d4e5f6a1b2c3d4e5f6a1b2c3d4e5f6a1b2c3d4e5f6a1b2c3d4e5f6a1b2c3d4";

        [SetUp]
        public void SetUp()
        {
            // Clear environment variables before each test
            Environment.SetEnvironmentVariable("BLUEINK_API_KEY", null);
            Environment.SetEnvironmentVariable("BLUEINK_API_URL", null);
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up environment variables after each test
            Environment.SetEnvironmentVariable("BLUEINK_API_KEY", null);
            Environment.SetEnvironmentVariable("BLUEINK_API_URL", null);
        }

        [Test]
        public void Constructor_WithValidApiKey_CreatesService()
        {
            // Act
            using (var service = new BlueinkService(ValidApiKey))
            {
                // Assert
                Assert.IsNotNull(service);
                Assert.IsNotNull(service.BundleResource);
                Assert.IsNotNull(service.PacketResource);
                Assert.IsNotNull(service.PersonResource);
                Assert.IsNotNull(service.TemplateResource);
                Assert.IsNotNull(service.WebhookResource);
                Assert.IsNotNull(service.RateLimitResource);
            }
        }

        [Test]
        public void Constructor_WithNullApiKey_AndNoEnvironmentVariable_ThrowsConfigurationException()
        {
            // Act & Assert
            Assert.Throws<BlueinkConfigurationException>(() => new BlueinkService(null));
        }

        [Test]
        public void Constructor_WithEmptyApiKey_AndNoEnvironmentVariable_ThrowsConfigurationException()
        {
            // Act & Assert
            Assert.Throws<BlueinkConfigurationException>(() => new BlueinkService(""));
        }

        [Test]
        public void Constructor_WithWhitespaceApiKey_AndNoEnvironmentVariable_ThrowsConfigurationException()
        {
            // Act & Assert
            Assert.Throws<BlueinkConfigurationException>(() => new BlueinkService("   "));
        }

        [Test]
        public void Constructor_WithTooShortApiKey_ThrowsConfigurationException()
        {
            // Act & Assert
            var ex = Assert.Throws<BlueinkConfigurationException>(() => new BlueinkService("shortkey"));
            Assert.That(ex.Message, Does.Contain("appears to be invalid"));
        }

        [Test]
        public void Constructor_WithInvalidCharactersInApiKey_ThrowsConfigurationException()
        {
            // Act & Assert
            var ex = Assert.Throws<BlueinkConfigurationException>(() => 
                new BlueinkService("a1b2c3d4e5f6a1b2c3d4e5f6a1b2c3d4e5f6!@#$"));
            Assert.That(ex.Message, Does.Contain("invalid characters"));
        }

        [Test]
        public void Constructor_WithEnvironmentVariableApiKey_CreatesService()
        {
            // Arrange
            Environment.SetEnvironmentVariable("BLUEINK_API_KEY", ValidApiKey);

            // Act
            using (var service = new BlueinkService())
            {
                // Assert
                Assert.IsNotNull(service);
                Assert.IsNotNull(service.BundleResource);
            }
        }

        [Test]
        public void Constructor_WithExplicitApiKey_OverridesEnvironmentVariable()
        {
            // Arrange
            Environment.SetEnvironmentVariable("BLUEINK_API_KEY", "environmentkey12345678901234567890123456789012345678901234567890123456789012345678");
            
            // Act - Use explicit key
            using (var service = new BlueinkService(ValidApiKey))
            {
                // Assert - Service should be created (using explicit key, not env var)
                Assert.IsNotNull(service);
            }
        }

        [Test]
        public void BaseUri_ReturnsDefaultProductionUrl()
        {
            // Act
            using (var service = new BlueinkService(ValidApiKey))
            {
                // Assert
                Assert.AreEqual("https://api.blueink.com/api/v2/", service.BaseUri);
            }
        }

        [Test]
        public void Constructor_WithCustomBaseUrl_UsesCustomUrl()
        {
            // Act
            using (var service = new BlueinkService(ValidApiKey, "https://custom.api.com/api/v2/"))
            {
                // Assert
                Assert.AreEqual("https://custom.api.com/api/v2/", service.BaseUri);
            }
        }

        [Test]
        public void Constructor_WithCustomBaseUrlWithoutTrailingSlash_AddsTrailingSlash()
        {
            // Act
            using (var service = new BlueinkService(ValidApiKey, "https://custom.api.com/api/v2"))
            {
                // Assert
                Assert.AreEqual("https://custom.api.com/api/v2/", service.BaseUri);
            }
        }

        [Test]
        public void Name_ReturnsBlueink()
        {
            // Act
            using (var service = new BlueinkService(ValidApiKey))
            {
                // Assert
                Assert.AreEqual("blueink", service.Name);
            }
        }
    }
}

