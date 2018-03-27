using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Http.Tracing;
using NineCodingChallenge.Web.Utils;

namespace NineCodingChallenge.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //string physicalWebRootPath = HttpContext.Current.Server.MapPath("~/App_Data");
            //// Create a file for output named TestFile.txt.
            //Stream logFileStream = File.Open(physicalWebRootPath + "/LogFile.txt", FileMode.Append);

            //// Web API configuration and services
            //SystemDiagnosticsTraceWriter traceWriter = config.EnableSystemDiagnosticsTracing();
            //traceWriter.TraceSource.Listeners.Add(new TextWriterTraceListener(logFileStream));

            config.EnableSystemDiagnosticsTracing();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var jsonFormatter = new JsonMediaTypeFormatter();
            config.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonFormatter));
            config.MessageHandlers.Add(new MessageLoggingHandler());
        }
    }
}
