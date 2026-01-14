using Blueink.Client.Net.v2.Helper;
using NUnit.Framework;

namespace Blueink.Client.Net.v2.Tests
{
    /// <summary>
    /// Unit tests for ValidationHelper utility methods.
    /// </summary>
    [TestFixture]
    public class ValidationHelperTests
    {
        #region IsValidUUID Tests

        [Test]
        public void IsValidUUID_WithValidUUID_ReturnsTrue()
        {
            // Arrange
            var validUuid = "550e8400-e29b-41d4-a716-446655440000";

            // Act
            var result = ValidationHelper.IsValidUUID(validUuid);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsValidUUID_WithValidUUIDWithBraces_ReturnsTrue()
        {
            // Arrange
            var validUuid = "{550e8400-e29b-41d4-a716-446655440000}";

            // Act
            var result = ValidationHelper.IsValidUUID(validUuid);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsValidUUID_WithUppercaseUUID_ReturnsTrue()
        {
            // Arrange
            var validUuid = "550E8400-E29B-41D4-A716-446655440000";

            // Act
            var result = ValidationHelper.IsValidUUID(validUuid);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsValidUUID_WithNullString_ReturnsFalse()
        {
            // Act
            var result = ValidationHelper.IsValidUUID(null);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidUUID_WithEmptyString_ReturnsFalse()
        {
            // Act
            var result = ValidationHelper.IsValidUUID("");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidUUID_WithInvalidFormat_ReturnsFalse()
        {
            // Arrange
            var invalidUuid = "not-a-valid-uuid";

            // Act
            var result = ValidationHelper.IsValidUUID(invalidUuid);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidUUID_WithPartialUUID_ReturnsFalse()
        {
            // Arrange
            var invalidUuid = "550e8400-e29b-41d4";

            // Act
            var result = ValidationHelper.IsValidUUID(invalidUuid);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region IsValidEmail Tests

        [Test]
        public void IsValidEmail_WithValidEmail_ReturnsTrue()
        {
            // Act
            var result = ValidationHelper.IsValidEmail("test@example.com");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsValidEmail_WithValidEmailWithSubdomain_ReturnsTrue()
        {
            // Act
            var result = ValidationHelper.IsValidEmail("test@mail.example.com");

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsValidEmail_WithNullEmail_ReturnsFalse()
        {
            // Act
            var result = ValidationHelper.IsValidEmail(null);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidEmail_WithEmptyEmail_ReturnsFalse()
        {
            // Act
            var result = ValidationHelper.IsValidEmail("");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsValidEmail_WithInvalidEmail_ReturnsFalse()
        {
            // Act
            var result = ValidationHelper.IsValidEmail("not-an-email");

            // Assert
            Assert.IsFalse(result);
        }

        #endregion

        #region generate_UUIDkey Tests

        [Test]
        public void GenerateUUIDkey_ReturnsValidUUID()
        {
            // Act
            var result = ValidationHelper.generate_UUIDkey();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(ValidationHelper.IsValidUUID(result));
        }

        #endregion
    }
}

