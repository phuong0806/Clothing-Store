using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuanLyBanHang
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "Danh Sách Sản Phẩm",
              url: "san-pham",
              defaults: new { controller = "SanPham", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "QuanLyBanhang.Controllers" }
            );

            routes.MapRoute(
              name: "Chi tiết sản phẩm",
              url: "san-pham/{url}",
              defaults: new { controller = "SanPham", action = "ChiTietSanPham", id = UrlParameter.Optional },
              namespaces: new[] { "QuanLyBanhang.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "QuanLyBanhang.Controllers" }
            );


        }
    }
}
