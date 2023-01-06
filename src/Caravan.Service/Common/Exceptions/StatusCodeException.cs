using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.Service.Common.Exceptions
{
    public class StatusCodeException : Exception
    {
        public HttpStatusCode statusCode { get; set; }

        public StatusCodeException()
        {

        }
        public StatusCodeException(HttpStatusCode statusCode, string message) :
            base(message)
        {
            this.statusCode = statusCode;
        }

    }
}
