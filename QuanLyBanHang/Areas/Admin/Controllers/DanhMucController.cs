using Model.DAO;
using Model.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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

        //public JsonResult layDanhMuc(int id)
        //{

        //    var output = JsonConvert.SerializeObject(new DanhMucDAO().layDanhMucTheoID(id),
        //                    new JsonSerializerSettings
        //                    {
        //                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //                    });

        //    return Json(new
        //    {
        //        data = output
        //    }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult Save(LoaiSanPham loaisanpham)
        //{
        //    if (loaisanpham.ID > 0)
        //    {
        //        return Json(new
        //        {
        //            //them danh muc
        //            status = new DanhMucDAO().update(loaisanpham)
        //        });
        //    }
        //    else
        //    {
        //        return Json(new
        //        {
        //            //cap nhat danh muc
        //            status = new DanhMucDAO().insert(loaisanpham)
        //        });
        //    }
        //}

        //[HttpPost]
        //public JsonResult Delete(int id)
        //{
        //    return Json(new
        //    {
        //        status = new DanhMucDAO().delete(id)
        //    });
        //}
    }
}