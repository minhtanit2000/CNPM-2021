using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CNPM
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
 
            routes.MapRoute("Detail", "{type}/{meta}/{id}",
            new { controller = "Detail", action = "getDescription", meta = UrlParameter.Optional },
            new RouteValueDictionary
            {
                { "type", "cua-hang" }
            },
            namespaces: new[] { "CNPM.Controllers" });

            routes.MapRoute("Product", "{type}/{meta}",
            new { controller = "Product", action = "getProductFollowMeta", meta = UrlParameter.Optional },
            new RouteValueDictionary
            {
                { "type", "cua-hang" }
            },
            namespaces: new[] { "CNPM.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "TrangChu", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Lienhe",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "LienHe", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "About",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional }
           );


            routes.MapRoute(    
                name: "Login",
                url: "{controller}/{action}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Register",
               url: "{controller}",
               defaults: new { controller = "Register", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Cart",
               url: "{controller}",
               defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "accounts",
                url: "{controller}",
                defaults: new { controller = "Account", action = "Index", id = UrlParameter.Optional }
            );

        }
    }   
}
