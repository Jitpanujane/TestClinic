using Newtonsoft.Json;
using Project.Core.Exceptions;
using Project.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace Project.Api.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            HttpResponseMessage response = new HttpResponseMessage()
            {
                Content = Content(context.Exception)
            };

            switch (context.Exception)
            {
                case _BaseException _:
                    response.StatusCode = HttpStatusCode.BadRequest;
                    break;
                default:
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response = response;
        }

        private StringContent Content(Exception exception)
        {
            string message = "An error occurred, please try again later.";

            if (ConfigurationManager.AppSettings["Environment"] != "Production")
            {
                message = exception.GetInnerException().Message;
            }

            string content = JsonConvert.SerializeObject(new
            {
                Message = message
            });

            return new StringContent(content, Encoding.UTF8, "application/json");
        }
    }
}