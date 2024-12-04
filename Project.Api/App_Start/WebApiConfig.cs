using Project.Api.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Project.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.CorsHandler();

            config.MessageHandlers.Add(new AuthorizationFilter());

            config.Filters.Add(new ValidateModelFilter());
            config.Filters.Add(new ExceptionFilter());

            config.Formatters
                .JsonFormatter
                .SerializerSettings
                .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void CorsHandler(this HttpConfiguration config)
        {
            string origins = ConfigurationManager.AppSettings.Get("AllowOrigins");
            string headers = ConfigurationManager.AppSettings.Get("AllowHeaders");
            string methods = ConfigurationManager.AppSettings.Get("AllowMethods");

            if (string.IsNullOrEmpty(origins))
            {
                origins = "*";
            }

            if (string.IsNullOrEmpty(headers))
            {
                headers = "*";
            }

            if (string.IsNullOrEmpty(methods))
            {
                methods = "*";
            }

            config.EnableCors(new EnableCorsAttribute(origins, headers, methods));
        }
    }
}
