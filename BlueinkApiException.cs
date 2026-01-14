using Blueink.Client.Net.v2.Model;
using System;
using System.Linq;
using System.Net;

namespace Blueink.Client.Net.v2
{
    /// <summary>
    /// Exception thrown when there is a configuration error with the Blueink SDK.
    /// This includes missing or invalid API keys, invalid base URLs, etc.
    /// </summary>
    public class BlueinkConfigurationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlueinkConfigurationException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public BlueinkConfigurationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlueinkConfigurationException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="inner">The inner exception.</param>
        public BlueinkConfigurationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    /// <summary>
    /// Exception thrown when a Blueink API request fails.
    /// Contains detailed information about the error including HTTP status code and error details.
    /// </summary>
    public class BlueinkApiException : Exception
    {
        private readonly string serviceName;

        /// <summary>
        /// Gets the name of the service that threw the exception.
        /// </summary>
        public string ServiceName { get { return serviceName; } }

        /// <summary>
        /// Gets or sets the HTTP status code of the failed request.
        /// </summary>
        public HttpStatusCode? HttpStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the error details from the API response.
        /// </summary>
        public Error Error { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlueinkApiException"/> class.
        /// </summary>
        /// <param name="serviceName">The name of the service.</param>
        /// <param name="message">The error message.</param>
        /// <param name="inner">The inner exception.</param>
        /// <exception cref="ArgumentNullException">Thrown when serviceName is null or empty.</exception>
        public BlueinkApiException(string serviceName, string message, Exception inner)
            : base(message, inner)
        {
            if (String.IsNullOrWhiteSpace(serviceName))
                throw new ArgumentNullException(nameof(serviceName), "Service name cannot be null or empty.");

            this.serviceName = serviceName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlueinkApiException"/> class.
        /// </summary>
        /// <param name="serviceName">The name of the service.</param>
        /// <param name="message">The error message.</param>
        public BlueinkApiException(string serviceName, string message)
            : this(serviceName, message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlueinkApiException"/> class.
        /// </summary>
        /// <param name="serviceName">The name of the service.</param>
        /// <param name="message">The error message.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        public BlueinkApiException(string serviceName, string message, HttpStatusCode statusCode)
            : this(serviceName, message, null)
        {
            this.HttpStatusCode = statusCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlueinkApiException"/> class.
        /// </summary>
        /// <param name="serviceName">The name of the service.</param>
        /// <param name="message">The error message.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <param name="error">The error details from the API.</param>
        public BlueinkApiException(string serviceName, string message, HttpStatusCode statusCode, Error error)
            : this(serviceName, message, statusCode)
        {
            this.Error = error;
        }

        /// <summary>
        /// Returns a string representation of the exception with full error details.
        /// </summary>
        /// <returns>A detailed string representation of the exception.</returns>
        public override string ToString()
        {
            var statusCodeInfo = HttpStatusCode.HasValue
                ? $" (HTTP {(int)HttpStatusCode.Value} {HttpStatusCode.Value})"
                : "";

            if (Error == null)
            {
                return $"The service {serviceName} has thrown an exception{statusCodeInfo}: {base.ToString()}";
            }

            var formatErrorFields = Error.Errors?.Select(q =>
                $"  - Field: {q.Field}, Message: {q.Message}").ToArray();

            if (formatErrorFields != null && formatErrorFields.Length > 0)
            {
                return $"The service {serviceName} has thrown an exception{statusCodeInfo}:\r\n" +
                       $"Error Code: {Error.Code}\r\n" +
                       $"Error Message: {Error.Message}\r\n" +
                       $"Error Detail: {Error.Detail}\r\n" +
                       $"Field Errors:\r\n{String.Join("\r\n", formatErrorFields)}\r\n\r\n" +
                       $"Stack Trace: {base.ToString()}";
            }

            return $"The service {serviceName} has thrown an exception{statusCodeInfo}:\r\n" +
                   $"Error Code: {Error.Code}\r\n" +
                   $"Error Message: {Error.Message}\r\n" +
                   $"Error Detail: {Error.Detail}\r\n\r\n" +
                   $"Stack Trace: {base.ToString()}";
        }
    }

    /// <summary>
    /// Exception thrown when a required parameter is missing or invalid.
    /// </summary>
    public class BlueinkValidationException : BlueinkApiException
    {
        /// <summary>
        /// Gets the name of the parameter that failed validation.
        /// </summary>
        public string ParameterName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlueinkValidationException"/> class.
        /// </summary>
        /// <param name="serviceName">The name of the service.</param>
        /// <param name="parameterName">The name of the parameter that failed validation.</param>
        /// <param name="message">The validation error message.</param>
        public BlueinkValidationException(string serviceName, string parameterName, string message)
            : base(serviceName, $"Validation error for parameter '{parameterName}': {message}")
        {
            ParameterName = parameterName;
        }
    }

    /// <summary>
    /// Exception thrown when a requested resource is not found (HTTP 404).
    /// </summary>
    public class BlueinkNotFoundException : BlueinkApiException
    {
        /// <summary>
        /// Gets the type of resource that was not found.
        /// </summary>
        public string ResourceType { get; }

        /// <summary>
        /// Gets the ID of the resource that was not found.
        /// </summary>
        public string ResourceId { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlueinkNotFoundException"/> class.
        /// </summary>
        /// <param name="serviceName">The name of the service.</param>
        /// <param name="resourceType">The type of resource (e.g., "Bundle", "Packet").</param>
        /// <param name="resourceId">The ID of the resource that was not found.</param>
        public BlueinkNotFoundException(string serviceName, string resourceType, string resourceId)
            : base(serviceName, $"{resourceType} with ID '{resourceId}' was not found.", System.Net.HttpStatusCode.NotFound)
        {
            ResourceType = resourceType;
            ResourceId = resourceId;
        }
    }

    /// <summary>
    /// Exception thrown when the API rate limit is exceeded (HTTP 429).
    /// </summary>
    public class BlueinkRateLimitException : BlueinkApiException
    {
        /// <summary>
        /// Gets the number of seconds to wait before retrying.
        /// </summary>
        public int? RetryAfterSeconds { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlueinkRateLimitException"/> class.
        /// </summary>
        /// <param name="serviceName">The name of the service.</param>
        /// <param name="retryAfterSeconds">The number of seconds to wait before retrying.</param>
        public BlueinkRateLimitException(string serviceName, int? retryAfterSeconds = null)
            : base(serviceName,
                retryAfterSeconds.HasValue
                    ? $"Rate limit exceeded. Please wait {retryAfterSeconds} seconds before retrying."
                    : "Rate limit exceeded. Please wait before retrying.",
                (System.Net.HttpStatusCode)429)
        {
            RetryAfterSeconds = retryAfterSeconds;
        }
    }

    /// <summary>
    /// Exception thrown when authentication fails (HTTP 401 or 403).
    /// </summary>
    public class BlueinkAuthenticationException : BlueinkApiException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlueinkAuthenticationException"/> class.
        /// </summary>
        /// <param name="serviceName">The name of the service.</param>
        /// <param name="message">The authentication error message.</param>
        public BlueinkAuthenticationException(string serviceName, string message = "Authentication failed. Please check your API key.")
            : base(serviceName, message, System.Net.HttpStatusCode.Unauthorized)
        {
        }
    }
}
