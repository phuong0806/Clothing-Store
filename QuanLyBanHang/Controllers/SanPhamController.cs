using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyBanHang.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        public ActionResult Index()
        {
            setViewBagForSanPham();
            return View();
        }

        public void setViewBagForSanPham()
        {
            SanPhamDAO sp = new SanPhamDAO();
            ViewBag.DanhSachSanPham = sp.layDanhSachTatCaSanPham();
        }

        public ActionResult ChiTietSanPham(string url)
        {
            SanPhamDAO sp = new SanPhamDAO();

            var model = sp.laySanPhamTheoURL(url);

            ViewBag.DanhSachHinhAnh = sp.loadMoreImages(model.ID);

            return View(model);
        }
    }
}