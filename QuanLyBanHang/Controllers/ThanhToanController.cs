using Model.DAO;
using Model.EF;
using Model.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyBanHang.Controllers
{
    public class ThanhToanController : Controller
    {
        // GET: ThanhToan
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult loadDonHang(string listCartString)
        {
            //Chuyển chuỗi json sản phẩm thành danh sách sản phẩm
            var listSanPham = JsonConvert.DeserializeObject<List<SanPhamGioHang>>(listCartString);

            //Lấy danh sách sản phẩm chuyển lại thành chuỗi json để gửi về view hiển thị
            var json = JsonConvert.SerializeObject(new SanPhamDAO().laySanPhamThanhToan(listSanPham), new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            return Json(new
            {
                data = json
            });
        }

        [HttpPost]
        public JsonResult DatHang(string listProduct, string orderDetail)
        {
            //Chuyển chuỗi json thành object
            var listSanPham = JsonConvert.DeserializeObject<List<SanPhamGioHang>>(listProduct);

            //Chuyển chuỗi json thành object
            var donHang = JsonConvert.DeserializeObject<DonHang>(orderDetail);

            var status = new DonHangDAO().insert(listSanPham, donHang);

            return Json(new
            {
                status = status
            });
        }
    }
}