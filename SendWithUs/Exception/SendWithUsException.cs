using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Sendwithus
{
    public class SendwithusException : System.Exception
    {
        public HttpStatusCode StatusCode;

        public SendwithusException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
