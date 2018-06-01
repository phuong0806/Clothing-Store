using Common;
using Model.DAO;
using Model.EF;
using Model.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Linq;

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
            MauDAO m = new MauDAO();
            KichCoDAO kc = new KichCoDAO();
            DanhMucDAO dm = new DanhMucDAO();

            ViewBag.listImage = ImageHelper.loadListImage();//lay danh sach hinh anh

            ViewBag.MaLoai = l.getTatCaLoai();//lay tat cac cac loai

            ViewBag.MaThuongHieu = th.getTatCaThuongHieu();  //lay thuong hieu

            ViewBag.DanhSachSanPham = spDAO.layDanhSachTatCaSanPham(); //lay tat ca sp

            ViewBag.Mau = m.layDanhSachMau();  //lat tat ca mau 

            ViewBag.KichCo = kc.getDanhSachKichCo();  //lat tat ca mau 

            ViewBag.DanhMuc = dm.getDanhMuc(); // lấy tất cả danh mục

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

        [HttpGet]
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
                DanhMucID = new DanhMucDAO().layIdDanhMucTheoLoai(model.MaLoai),
                data = output
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous, ValidateInput(false)]
        public JsonResult Save(string SanPham, HttpPostedFileBase file)
        {
            SanPhamViewModel sp = JsonConvert.DeserializeObject<SanPhamViewModel>(SanPham);

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

        [HttpPost]
        public JsonResult kiemTraUrlTonTai(string url, int id)
        {
            return Json(new
            {
                status = new SanPhamDAO().kiemTraUrl(url, id)
            });
        }

        [HttpPost]
        public JsonResult LayLoaiSanPham(int DanhMucID)
        {
            var list = new LoaiDAO().layDanhSachLoaiTheoID(DanhMucID);
            
            var output = JsonConvert.SerializeObject(list, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            return Json(new
            {
                data = output
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveImages(int id, string images)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            var listImages = serializer.Deserialize<List<string>>(images);

            XElement xElement = new XElement("images");

            foreach (var item in listImages)
            {
                xElement.Add(new XElement("image", item));
            }

            try
            {
                new SanPhamDAO().UpdateSaveImages(id, xElement.ToString());

                return Json(new
                {
                    status = true
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        public JsonResult loadMoreImages(int id)
        {
            var sp = new SanPhamDAO().laySanPhamTheoID(id);

            if (sp.AnhKhac == null || sp.AnhKhac == "")
            {
                return Json(new
                {
                    data = ""
                }, JsonRequestBehavior.AllowGet);
            }

            XElement xImages = XElement.Parse(sp.AnhKhac);

            List<string> listImageReturn = new List<string>();

            foreach (XElement element in xImages.Elements())
            {
                listImageReturn.Add(element.Value);
            }

            return Json(new
            {
                data = listImageReturn
            }, JsonRequestBehavior.AllowGet);
        }
    }
}