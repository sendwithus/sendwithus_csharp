using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus SendwithusException class.
    /// Used to return an exception when an API call fails.  Provides the HTTP status code and error message.
    /// </summary>
    [Serializable]
    public class SendwithusException : System.Exception
    {
        public HttpStatusCode StatusCode;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="statusCode">The HTTP status code of the API call that triggered the exception</param>
        /// <param name="message">The associated error message</param>
        public SendwithusException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
