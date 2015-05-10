using System.Web.Http;
using Newtonsoft.Json;

namespace QueryMonger
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services
            config.EnableCors();

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{action}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

		    //Web API formatters
            config.Formatters.Remove(config.Formatters.XmlFormatter);

			config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
			 = ReferenceLoopHandling.Serialize;
			config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling
			= PreserveReferencesHandling.Objects;
		}
	}
}
