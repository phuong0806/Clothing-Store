using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyBanHang.Controllers
{
    public class ChiTietSanPhamController : Controller
    {
        // GET: ChiTietSanPham
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult loadMoreImages(int id)
        {
            return Json(new
            {
                data = new SanPhamDAO().loadMoreImages(id)
            },
           JsonRequestBehavior.AllowGet);
        }
    }
}