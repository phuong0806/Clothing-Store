using Model.DAO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyBanHang.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        // GET: Admin/DonHang
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult loadDonHang()
        {
            var listDonHang = new DonHangDAO().getDanhSach();

            return Json(new
            {
                data = listDonHang
            }, JsonRequestBehavior.AllowGet);
        }
    }
}