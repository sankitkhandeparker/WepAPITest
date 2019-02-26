using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using WebApiContrib.Formatting.Jsonp;
using System.Web.Http.Cors;

namespace WebApiTest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.Add(new CustomJSONMediaFormatter());

            //var jsonpFormatter = new JsonpMediaTypeFormatter(new CustomJSONMediaFormatter());
            //config.Formatters.Insert(0, jsonpFormatter);
            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            
            //config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
        public class CustomJSONMediaFormatter : JsonMediaTypeFormatter
        {
            public CustomJSONMediaFormatter() {
                this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
                this.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                this.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }
            public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
            {
                base.SetDefaultContentHeaders(type, headers, mediaType);
                headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
        }
    }
}
