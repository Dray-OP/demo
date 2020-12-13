using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace demo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // danh mục sản phẩm
            routes.MapRoute(
                name: "Product Category",
                url: "san-pham/{MetaTitle}-{cateId}",
                defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                namespaces: new[] { "demo.Controllers" }
                );
            // chi tiết sản phẩm
            routes.MapRoute(
                name: "Product Detail",
                url: "chi-tiet/{MetaTitle}-{cateId}",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "demo.Controllers" }
                );
            routes.MapRoute(
               name: "About",
               url: "gioi-thieu",
               defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "demo.Controllers" }
               );
            routes.MapRoute(
               name: "Cart",
               url: "gio-hang",
               defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "demo.Controllers" }
               );
            routes.MapRoute(
               name: "Add card",
               url: "them-gio-hang",
               defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
               namespaces: new[] { "demo.Controllers" }
               );

            // defaut luôn để cuối cùng
            routes.MapRoute(
                name: "Default", // mặc định thì vào đây
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] {"demo.Controllers"}
                );

            

        }
    }
}
