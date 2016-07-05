using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace SendWithUs
{
    public class SendWithUsException : System.Exception
    {
        public HttpStatusCode StatusCode;

        public SendWithUsException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
