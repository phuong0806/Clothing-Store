using Common;
using Model.DAO;
using Model.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace QuanLyBanHang.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        SanPhamDAO spDAO = new SanPhamDAO();
        // GET: Admin/SanPham
        public ActionResult Index()
        {
            setViewBagForSanPham();
            return View();
        }

        public void setViewBagForSanPham()
        {
            LoaiDAO l = new LoaiDAO();
            ThuongHieuDAO th = new ThuongHieuDAO();
            //lay danh sach hinh anh
            ViewBag.listImage = ImageHelper.loadListImage();
            //lay tat cac cac loai
            ViewBag.MaLoai = l.getTatCaLoai();
            //lay thuong hieu
            ViewBag.MaThuongHieu = th.getTatCaThuongHieu();
            //lay tat ca sp
            ViewBag.DanhSachSanPham = spDAO.layDanhSachTatCaSanPham();
        }

        [HttpPost]
        public JsonResult Xoa(int id)
        {
            var status = false;
            if (new SanPhamDAO().delete(id))
            {
                status = true;
            }

            return Json(new
            {
                status = status
            });
        }

        public JsonResult layChiTietSanPham(int id)
        {
            var model = new SanPhamDAO().laySanPhamTheoID(id);

            var output = JsonConvert.SerializeObject(model, 
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });

            return Json(new
            {
                data = output
            }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [AllowAnonymous, ValidateInput(false)]
        public JsonResult Save(string SanPham, HttpPostedFileBase file)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            SanPham sp = serializer.Deserialize<SanPham>(SanPham);

            if (file != null)
            {
                sp.HinhAnh = ImageHelper.saveImage(file);
            }

            var kiemtra = false;

            if (sp.ID == 0)
            {
                kiemtra = spDAO.insert(sp);
            }
            else
            {
               kiemtra = spDAO.update(sp);
            }

            return Json(new
            {
                status = kiemtra
            });
        }
    }
}