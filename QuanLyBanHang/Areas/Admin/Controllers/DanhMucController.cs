using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyBanHang.Areas.Admin.Controllers
{
    public class DanhMucController : Controller
    {
        // GET: Admin/DanhMuc
        public ActionResult Index()
        {
            ViewBag.danhSachDanhMuc = new DanhMucDAO().getDanhMuc();
            return View();
        }

        [HttpPost]
        public JsonResult Save(LoaiSanPham loaisanpham)
        {
            if (loaisanpham.ID > 0)
            {
                return Json(new
                {
                    //them danh muc
                    status = new DanhMucDAO().insert(loaisanpham)
                });
            }
            else
            {
                return Json(new
                {
                    //cap nhat danh muc
                    status = new DanhMucDAO().update(loaisanpham)
                });
            }

            
        }
    }
}