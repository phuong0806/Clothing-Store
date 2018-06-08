using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyBanHang.Areas.Admin.Controllers
{
    public class LoaiController : Controller
    {
        // GET: Admin/Loai
        public ActionResult Index()
        {
            ViewBag.danhSachLoaiSanPham = new LoaiDAO().getDanhSach();
            return View();
        }

      
        [HttpPost]
        public JsonResult Save(Loai loaisanpham)
        {
            if (loaisanpham.ID > 0)
            {
                return Json(new
                {
                    //them danh muc
                    status = new LoaiDAO().update(loaisanpham)
                });
            }
            else
            {
                return Json(new
                {
                    //cap nhat danh muc
                    status = new LoaiDAO().insert(loaisanpham)
                });
            }
        }

        public JsonResult Delete(int id)
        {
            return Json(new
            {
                status = new LoaiDAO().delete(id)
            });
        }
    }
}