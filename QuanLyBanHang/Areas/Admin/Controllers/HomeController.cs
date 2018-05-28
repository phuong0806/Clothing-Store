using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyBanHang.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        StoreDbContext db = new StoreDbContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = db.Admins.Find(username);
            if (user != null)
            {
                if (user.MatKhauAD == password)
                {
                    Session["TaiKhoanAD"] = user.TaiKhoanAD;
                    Session["HoTen"] = user.HoTen;
                    Session["TrangThai"] = user.TrangThai;
                    Session["isAdmin"] = user.isAdmin;
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Error = "Tài khoản hoặc mật khẩu không xác định";
            return View();
        }
    }
}