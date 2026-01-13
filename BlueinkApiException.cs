using Blueink.Client.Net.v2.Model;
using System;
using System.Linq;

namespace Blueink.Client.Net.v2
{
    public class BlueinkApiException : Exception
    {
        private readonly string serviceName;

        public string ServiceName { get { return serviceName; } }

        public BlueinkApiException(string serviceName, string message, Exception inner)
            : base(message,inner)
        {
            if (String.IsNullOrWhiteSpace(serviceName))
                throw new ArgumentNullException("serviceName");

            this.serviceName = serviceName;
        }

        public BlueinkApiException(string serviceName, string message) : this(serviceName,message,null)
        {
        }

        public Error Error { get; set; }
        public override string ToString()
        {
            var formatErrorFiels = Error?.Errors?.Select(q => String.Format("Field: {0} Message: {1}\r\n", q.Field, q.Message)).ToArray();
            if (formatErrorFiels != null)
                return string.Format("The service {1} has thrown an exception: {0}\r\nError Message: {2}\r\nError Detail: {3}\r\n\r\n{4}", base.ToString(), serviceName, Error.Message , Error.Detail , String.Join("\r\n",formatErrorFiels));
            return string.Format("The service {1} has thrown an exception: {0}\r\nError Message: {2} , Error Detail: {3}", base.ToString(), serviceName , Error.Message , Error.Detail );
        }
    }
}
