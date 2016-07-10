using System;
using System.Security.Permissions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.Serialization;

namespace Sendwithus
{
    /// <summary>
    /// sendwithus SendwithusException class.
    /// Used to return an exception when an API call fails.  Provides the HTTP status code and error message.
    /// </summary>
    [Serializable]
    public class SendwithusException : System.Exception
    {
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Default constructor to use for a new SendwithusException
        /// </summary>
        /// <param name="statusCode">The HTTP status code of the API call that triggered the exception</param>
        /// <param name="message">The associated error message</param>
        public SendwithusException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Extends the empty constructor() from System.Exception.
        /// </summary>
        public SendwithusException() : base() { }

        /// <summary>
        /// Extends the constructor(string message) from System.Exception.
        /// </summary>
        /// <param name="message">The error meessage</param>
        public SendwithusException(string message) : base(message) { }

        /// <summary>
        /// Extends the constructor(string message, Exception inner) from System.Exception.
        /// This public constructor is used by class instantiators.
        /// </summary>
        /// <param name="message">The error meessage</param>
        /// <param name="inner">The inner exception</param>
        public SendwithusException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Extends the constructor(SerializationInfo info, StreamingContext context) from System.Exception.
        /// This protected constructor is used for deserialization.
        /// </summary>
        /// <param name="info">The SerializationInfo to populate with data</param>
        /// <param name="context">The destination (see StreamingContext) for this serialization</param>
        protected SendwithusException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// Extends the GetObjectData method from System.Exception.
        /// GetObjectData performs a custom serialization.
        /// Adds the HTTP Status code to the information to be serialized
        /// </summary>
        /// <param name="info">The SerializationInfo to populate with data</param>
        /// <param name="context">The destination (see StreamingContext) for this serialization</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("StatusCode", StatusCode);
        }
    }
}
