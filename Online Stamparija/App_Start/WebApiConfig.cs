﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Online_Stamparija
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name : "DefaultApi",
                routeTemplate : "api/{controller}/{id}",
                defaults : new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
              name : "Posao1",
              routeTemplate : "api/{controller}/{category}/{id}",
              defaults : new { controller = "Posao1", category = "Get", id = RouteParameter.Optional }
            );
        }
    }
}
