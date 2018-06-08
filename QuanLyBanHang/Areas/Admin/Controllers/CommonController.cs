using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyBanHang.Areas.Admin.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult saveImage(HttpPostedFileBase file)
        {
            return Json(new
            {
                data = ImageHelper.saveImage(file)
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult deleteImage(string filename)
        {
            return Json(ImageHelper.DeleteImageByfilename(filename), JsonRequestBehavior.AllowGet);
        }
    }
}