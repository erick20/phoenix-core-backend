using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Helpers
{
    public class LoggingHelper
    {
        public static bool GetLoggingAccess(int statusCode)
        {
            bool needLogging = false;

            switch (statusCode)
            {
                case 400:
                    // Bad request
                    break;
                case 401:
                    // unAuthorized request
                    break;
                case 402:
                    // login unsuccessful
                    break;
                case 403:
                    // forbidden access
                    break;
                case 404:
                    // page not found
                    break;
                case 409:
                    // conflict
                    break;
                case 422:
                    // unprocessable entity
                    break;
                case 503:
                    // service unavelable
                    needLogging = true;
                    break;
                case 500:
                    // internal server error
                    needLogging = true;
                    break;
                default:
                    needLogging = true;
                    break;
            }

            return needLogging;
        }
    }
}
