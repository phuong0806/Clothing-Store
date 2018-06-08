using Model.DAO;
using Model.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace QuanLyBanHang.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public JsonResult loadGioHang(string listCartString)
        {
            var listSanPham = JsonConvert.DeserializeObject<List<SanPham>>(listCartString);

            var json = JsonConvert.SerializeObject(new SanPhamDAO().laySanPhamTrongGiohang(listSanPham), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            return Json(new
            {
                data = json
            });
        }

    }
}