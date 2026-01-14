using Blueink.Client.Net.v2.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;

namespace Blueink.Client.Net.v2.Tests
{
    /// <summary>
    /// Unit tests for Blueink exception classes.
    /// </summary>
    [TestFixture]
    public class BlueinkExceptionTests
    {
        [Test]
        public void BlueinkConfigurationException_WithMessage_SetsMessage()
        {
            // Act
            var exception = new BlueinkConfigurationException("Test message");

            // Assert
            Assert.AreEqual("Test message", exception.Message);
        }

        [Test]
        public void BlueinkConfigurationException_WithInnerException_SetsInnerException()
        {
            // Arrange
            var innerException = new InvalidOperationException("Inner");

            // Act
            var exception = new BlueinkConfigurationException("Test message", innerException);

            // Assert
            Assert.AreEqual("Test message", exception.Message);
            Assert.AreSame(innerException, exception.InnerException);
        }

        [Test]
        public void BlueinkApiException_WithServiceNameAndMessage_SetsProperties()
        {
            // Act
            var exception = new BlueinkApiException("testservice", "Test error message");

            // Assert
            Assert.AreEqual("testservice", exception.ServiceName);
            Assert.AreEqual("Test error message", exception.Message);
        }

        [Test]
        public void BlueinkApiException_WithNullServiceName_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new BlueinkApiException(null, "Test message"));
        }

        [Test]
        public void BlueinkApiException_WithEmptyServiceName_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new BlueinkApiException("", "Test message"));
        }

        [Test]
        public void BlueinkApiException_WithHttpStatusCode_SetsStatusCode()
        {
            // Act
            var exception = new BlueinkApiException("testservice", "Test error", HttpStatusCode.BadRequest);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.HttpStatusCode);
        }

        [Test]
        public void BlueinkApiException_WithError_SetsError()
        {
            // Arrange
            var error = new Error
            {
                Code = ErrorCode.Invalid,
                Message = "Validation failed",
                Detail = "The request was invalid"
            };

            // Act
            var exception = new BlueinkApiException("testservice", "Test error", HttpStatusCode.BadRequest, error);

            // Assert
            Assert.IsNotNull(exception.Error);
            Assert.AreEqual(ErrorCode.Invalid, exception.Error.Code);
            Assert.AreEqual("Validation failed", exception.Error.Message);
        }

        [Test]
        public void BlueinkApiException_ToString_IncludesAllDetails()
        {
            // Arrange
            var error = new Error
            {
                Code = ErrorCode.Invalid,
                Message = "Validation failed",
                Detail = "The request was invalid",
                Errors = new List<ErrorField>
                {
                    new ErrorField { Field = "email", Message = "Invalid email format" }
                }
            };
            var exception = new BlueinkApiException("testservice", "Test error", HttpStatusCode.BadRequest, error);

            // Act
            var result = exception.ToString();

            // Assert
            Assert.That(result, Does.Contain("testservice"));
            Assert.That(result, Does.Contain("400"));
            Assert.That(result, Does.Contain("Validation failed"));
            Assert.That(result, Does.Contain("email"));
        }

        [Test]
        public void BlueinkValidationException_SetsParameterName()
        {
            // Act
            var exception = new BlueinkValidationException("testservice", "bundleId", "Bundle ID is required");

            // Assert
            Assert.AreEqual("bundleId", exception.ParameterName);
            Assert.That(exception.Message, Does.Contain("bundleId"));
            Assert.That(exception.Message, Does.Contain("Bundle ID is required"));
        }

        [Test]
        public void BlueinkNotFoundException_SetsResourceTypeAndId()
        {
            // Act
            var exception = new BlueinkNotFoundException("testservice", "Bundle", "123-456-789");

            // Assert
            Assert.AreEqual("Bundle", exception.ResourceType);
            Assert.AreEqual("123-456-789", exception.ResourceId);
            Assert.AreEqual(HttpStatusCode.NotFound, exception.HttpStatusCode);
        }

        [Test]
        public void BlueinkRateLimitException_WithRetryAfter_SetsRetryAfterSeconds()
        {
            // Act
            var exception = new BlueinkRateLimitException("testservice", 60);

            // Assert
            Assert.AreEqual(60, exception.RetryAfterSeconds);
            Assert.That(exception.Message, Does.Contain("60 seconds"));
        }

        [Test]
        public void BlueinkAuthenticationException_SetsDefaultMessage()
        {
            // Act
            var exception = new BlueinkAuthenticationException("testservice");

            // Assert
            Assert.That(exception.Message, Does.Contain("Authentication failed"));
            Assert.AreEqual(HttpStatusCode.Unauthorized, exception.HttpStatusCode);
        }
    }
}

