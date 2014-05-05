using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AlienAttack.Web.Controllers;

namespace AlienAttack.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                "Move",
                "api/moves/{position}/{id}",
                    new { controller = "moves" }
                );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
