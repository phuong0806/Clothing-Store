using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyBanHang.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            setViewBagHome();
            return View();
        }

        public void setViewBagHome()
        {
            DanhMucDAO dm = new DanhMucDAO();
            ViewBag.DanhMucSanPham = dm.getDanhMuc();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}