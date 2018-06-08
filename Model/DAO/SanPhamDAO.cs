using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Model.DAO
{
    public class SanPhamDAO
    {
        StoreDbContext db = null;

        public SanPhamDAO()
        {
            db = new StoreDbContext();
        }

        public bool kiemTraUrl(string url, int id)
        {
            if (id == 0)
            {
                return checkExistUrl(url);
            }
            else
            {
                var urlOld = getUrlByID(id);
                if (urlOld == url)
                {
                    return false;
                }
                else
                {
                    return checkExistUrl(url);
                }
            }
        }

        public bool checkExistUrl(string url)
        {
            var model = db.SanPhams.Where(x => x.Url == url).SingleOrDefault();
            if (model != null)
            {
                return true;
            }
            return false;
        }

        public string getUrlByID(int id)
        {
            var model = db.SanPhams.Find(id);
            return model.Url;
        }

        public IEnumerable<SanPhamViewModel> layDanhSachTatCaSanPham()
        {
            var model = (from sp in db.SanPhams
                         join l in db.Loais on sp.MaLoai equals l.ID
                         join th in db.ThuongHieux on sp.MaThuongHieu equals th.ID
                         select new SanPhamViewModel
                         {
                             ID = sp.ID,
                             TenSanPham = sp.TenSanPham,
                             Url = sp.Url,
                             MoTa = sp.MoTa,
                             Gia = sp.Gia,
                             HinhAnh = sp.HinhAnh,
                             MaLoai = sp.MaLoai,
                             MaThuongHieu = sp.MaThuongHieu,
                             TenThuongHieu = th.TenThuongHieu,
                             TenLoai = l.TenLoai,
                             MauCollection = sp.Mau.Select(x => new MauViewModel { ID = x.ID, Name = x.Name, Code = x.Code }),
                             KichCoCollection = sp.KichCo.Select(x => new KichCoViewModel { ID = x.ID, Name = x.Name }),
                         }).ToList();

            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
            foreach (var item in model)
            {
                item.GiaString = double.Parse(item.Gia.ToString()).ToString("#,###", cul.NumberFormat);
            }

            return model;
        }

        public void UpdateSaveImages(int id, string images)
        {
<<<<<<< HEAD
            var model = db.SanPhams.Find(id);
=======
>>>>>>> 914d665f494e07325499ab522e171664215e332e
            var sp = db.SanPhams.Find(id);
            sp.AnhKhac = images;
            db.SaveChanges();
        }

        public SanPhamViewModel laySanPhamTheoURL(string url)
        {
            var model = (from sp in db.SanPhams
                         join dm in db.Loais on sp.MaLoai equals dm.ID
                         where sp.Url == url
                         select new SanPhamViewModel
                         {
                             TenSanPham = sp.TenSanPham,
                             HinhAnh = sp.HinhAnh,
                             Url = sp.Url,
                             Gia = sp.Gia,
                             MaThuongHieu = sp.MaThuongHieu,
                             SoLuong = sp.SoLuong,
                             ID = sp.ID,
                             MaLoai = sp.MaLoai,
                             MoTa = sp.MoTa,
                             AnhKhac = sp.AnhKhac,
                             DanhMucID = dm.DanhMucID,
                             MauCollection = sp.Mau.Select(x => new MauViewModel { ID = x.ID, Name = x.Name, Code = x.Code }),
                             KichCoCollection = sp.KichCo.Select(x => new KichCoViewModel { ID = x.ID, Name = x.Name }),
                         }).SingleOrDefault();
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
            model.GiaString = double.Parse(model.Gia.ToString()).ToString("#,###", cul.NumberFormat);
            return model;
        }

        public SanPhamViewModel laySanPhamTheoID(int id)
        {
            var model = (from sp in db.SanPhams
                         join dm in db.Loais on sp.MaLoai equals dm.ID
                         where sp.ID == id
                         select new SanPhamViewModel {
                             TenSanPham = sp.TenSanPham,
                             HinhAnh = sp.HinhAnh,
                             Url = sp.Url,
                             Gia = sp.Gia,
                             MaThuongHieu = sp.MaThuongHieu,
                             SoLuong = sp.SoLuong,
                             ID = sp.ID,
                             MaLoai = sp.MaLoai,
                             MoTa = sp.MoTa,
                             AnhKhac = sp.AnhKhac,
                             DanhMucID = dm.DanhMucID,
                         }).SingleOrDefault();
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
            model.GiaString = double.Parse(model.Gia.ToString()).ToString("#,###", cul.NumberFormat);
            return model;
        }

        public bool insert(SanPhamViewModel sp)
        {
            // Nếu thêm thành công thì trả về true ngược lại thì false
            try
            {
<<<<<<< HEAD
=======
                db.SanPham.Add(sp);
                db.SaveChanges();
>>>>>>> 914d665f494e07325499ab522e171664215e332e
                if (checkExistUrl(sp.Url))
                {
                    return false;
                }

                SanPham SanPhamNew = new SanPham();

                //Thêm dữ liệu cho sản phẩm mới
                SanPhamNew.TenSanPham = sp.TenSanPham;
                SanPhamNew.Url = sp.Url;
                SanPhamNew.MoTa = sp.MoTa;
                SanPhamNew.Gia = sp.Gia;
                SanPhamNew.HinhAnh = sp.HinhAnh;
                SanPhamNew.MaLoai = sp.MaLoai; 
                SanPhamNew.MaThuongHieu = sp.MaThuongHieu;
                SanPhamNew.SoLuong = sp.SoLuong;
                SanPhamNew.TrangThai = true;

                //Thêm màu đã chọn cho sản phẩm
                foreach (var idMau in sp.Mau)
                {
                    insertMau(SanPhamNew, idMau);
                }

                //Thêm kích cỡ đã chọn cho sản phẩm
                foreach (var idKichCo in sp.KichCo)
                {
                    insertKichCo(SanPhamNew, idKichCo);
                }
                db.SanPhams.Add(SanPhamNew); // Thêm sản phẩm
                db.SaveChanges(); // Lưu lại sản phẩm đã thêm
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        public bool update(SanPhamViewModel sp)
        {
            try
            {
<<<<<<< HEAD
=======
                var entity = db.SanPhams.Find(sp.ID);
                entity.TenSanPham = sp.TenSanPham;
                entity.MoTa = sp.MoTa;
                entity.MaLoai = sp.MaLoai;
                entity.MaThuongHieu = sp.MaThuongHieu;
                entity.HinhAnh = sp.HinhAnh;
                entity.Mau = sp.Mau;
                entity.KichCo = sp.KichCo;
                entity.Gia = sp.Gia;
>>>>>>> 914d665f494e07325499ab522e171664215e332e
                if (kiemTraUrl(sp.Url, sp.ID))
                {
                    return false;
                }

                var SanPhamUpdate = db.SanPhams.Find(sp.ID); //Tìm sản phẩm cần cập nhật
                SanPhamUpdate.TenSanPham = sp.TenSanPham;
                SanPhamUpdate.Url = sp.Url;
                SanPhamUpdate.MoTa = sp.MoTa;
                SanPhamUpdate.MaLoai = sp.MaLoai;
                SanPhamUpdate.MaThuongHieu = sp.MaThuongHieu;
                SanPhamUpdate.HinhAnh = sp.HinhAnh;
                SanPhamUpdate.Gia = sp.Gia;

                //Xóa đi màu cũ
                foreach (var item in SanPhamUpdate.Mau.ToList())
                {
                    deleteMau(SanPhamUpdate, item.ID);
                }

                //Cập nhật màu mới
                foreach (var id in sp.Mau.ToList())
                {
                    insertMau(SanPhamUpdate, id);
                }

                //Xóa đi kích cỡ cũ
                foreach (var item in SanPhamUpdate.KichCo.ToList())
                {
                    deleteKichCo(SanPhamUpdate, item.ID);
                }

                //Cập nhật kích cỡ mới
                foreach (var id in sp.KichCo.ToList())
                {
                    insertKichCo(SanPhamUpdate, id);
                }
                db.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return false;
            }
        }

        public bool delete(int id)
        {
            try
            {
<<<<<<< HEAD
                var entity = db.SanPhams.Find(id);
                db.SanPhams.Remove(entity);
=======
                var entity = db.SanPham.Find(id);
                db.SanPham.Remove(entity);
>>>>>>> 914d665f494e07325499ab522e171664215e332e
                var SanPham = db.SanPhams.FirstOrDefault(x => x.ID == id);

                //Xóa hết dữ liệu của sản phẩm trong bảng Mau_SanPham
                foreach (var item in SanPham.Mau.ToList())
                {
                    deleteMau(SanPham, item.ID);
                }

                //Xóa hết dữ liệu của sản phẩm trong bảng Mau_KichCo
                foreach (var item in SanPham.KichCo.ToList())
                {
                    deleteKichCo(SanPham, item.ID);
                }

                db.SanPhams.Remove(SanPham);
                db.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                return false;
            }
        }

        public List<string> loadMoreImages(int id)
        {
            var sp = laySanPhamTheoID(id);
            //Nếu không có ảnh thì return rỗng luôn
            if (sp.AnhKhac == null || sp.AnhKhac == "")
            {
                return null;
            }

            XElement xImages = XElement.Parse(sp.AnhKhac);

            List<string> listImageReturn = new List<string>();

            foreach (XElement element in xImages.Elements())
            {
                listImageReturn.Add(element.Value);
            }

            return listImageReturn;
        }

        //Xóa hết dữ liệu của sản phẩm trong bảng Mau_SanPham
        public void deleteMau(SanPham sp, int MauID)
        {
            var Mau = db.Maus.FirstOrDefault(x => x.ID == MauID);
            sp.Mau.Remove(Mau);
        }

        //Xóa hết dữ liệu của sản phẩm trong bảng Mau_KichCo
        public void deleteKichCo(SanPham sp, int KichCoID)
        {
            var KichCo = db.KichCoes.FirstOrDefault(x => x.ID == KichCoID);
            sp.KichCo.Remove(KichCo);
        }

        public void insertMau(SanPham sp, int MauID)
        {
            var m = db.Maus.Find(MauID);
            sp.Mau.Add(m);
        }
        public void insertKichCo(SanPham sp, int KichCoID)
        {
            var kc = db.KichCoes.Find(KichCoID);
            sp.KichCo.Add(kc);
        }

    }
}
