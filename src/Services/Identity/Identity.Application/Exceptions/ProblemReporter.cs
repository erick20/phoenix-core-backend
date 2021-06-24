using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Exceptions
{
    public static class ProblemReporter
    {
        public static void ReportBadRequest(string message = null)
        {
            throw new HttpException(400, message != null ? JsonObject(message) : "Invalid data");
        }

        public static void ReportUnauthorizedAccess(string message = null)
        {
            throw new HttpException(401, message != null ? JsonObject(message) : "Unauthorized: Access is denied due to invalid credentials");
        }

        public static void ReportPaymentRequired(string message = null)
        {
            throw new HttpException(402, message != null ? JsonObject(message) : "Payment Required");
        }

        public static void ReportAuthenticationFail(string message = null)
        {
            throw new HttpException(403, message != null ? JsonObject(message) : "Attempted to perform an unauthorized operation");
        }

        public static void ReportResourseNotfound(string message = null)
        {
            throw new HttpException(404, message != null ? JsonObject(message) : "Resource not found");
        }

        public static void ReportConflict(string message = null)
        {
            throw new HttpException(409, message != null ? JsonObject(message) : "Conflict with data");
        }

        public static void ReportConflict<T>(T model)
        {
            throw new HttpException(409, model != null ? JsonObject(model) : "Conflict with data");
        }

        public static void ReportServiceUnavelable(string message = null)
        {
            throw new HttpException(503, message != null ? JsonObject(message) : "Problem occured: Internal Server Error");
        }

        public static void ReportInternalServerError(string message = null)
        {
            throw new HttpException(500, message != null ? JsonObject(message) : "Problem occured: Server Unavailable");
        }

        public static void ReportInternalServerError(Exception ex)
        {
            throw new HttpException(500, ex != null ? JsonObject(ex.Message) : "Problem occured: Internal Server Error");
        }

        public static void ReportServiceUnavelable(Exception ex)
        {
            throw new HttpException(503, ex != null ? JsonObject(ex.Message) : "Problem occured: Server Unavailable");
        }

        private static string JsonObject(string message)
        {
            ErrorModel error = new ErrorModel
            {
                Key = message
            };

            return JsonConvert.SerializeObject(error);
        }

        private static string JsonObject<T>(T model)
        {
            return JsonConvert.SerializeObject(model);
        }
    }
}
