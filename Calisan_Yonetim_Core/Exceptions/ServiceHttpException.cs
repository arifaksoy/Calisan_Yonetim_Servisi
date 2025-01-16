using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Calisan_Yonetim_Core.Exceptions
{
    public class ServiceHttpException: ApplicationException
    {
        public HttpStatusCode StatusCode { get; private set; }

        public ServiceHttpException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public ServiceHttpException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public ServiceHttpException(HttpStatusCode statusCode, string message, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
