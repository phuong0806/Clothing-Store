using Model.DAO;
using Model.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyBanHang.Areas.Admin.Controllers
{
    public class MauController : Controller
    {
        // GET: Admin/Mau
        public ActionResult Index()
        {
            ViewBag.DanhSachMau = new MauDAO().layDanhSachMau();
            return View();
        }

        public JsonResult layMau(int id)
        {
            return Json(new
            {
                data = new MauDAO().layMauTheoID(id)
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Save(Mau mau)
        {
            if (mau.ID > 0)
            {
                return Json(new
                {
                    //them danh muc
                    status = new MauDAO().update(mau)
                });
            }
            else
            {
                return Json(new
                {
                    //cap nhat danh muc
                    status = new MauDAO().insert(mau)
                });
            }
        }

        public JsonResult Delete(int id)
        {
            return Json(new
            {
                status = new MauDAO().delete(id)
            });
        }
    }
}