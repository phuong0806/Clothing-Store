using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<SanPhamViewModel> layDanhSachTatCaSanPham()
        {
            var model = (from sp in db.SanPhams
                         join l in db.Loais on sp.MaLoai equals l.ID
                         join th in db.ThuongHieux on sp.MaThuongHieu equals th.ID
                         select new SanPhamViewModel
                         {
                             ID = sp.ID,
                             TenSanPham = sp.TenSanPham,
                             MoTa = sp.MoTa,
                             Gia = sp.Gia,
                             HinhAnh = sp.HinhAnh,
                             MaLoai = sp.MaLoai,
                             MaThuongHieu = sp.MaThuongHieu,
                             TenThuongHieu = th.TenThuongHieu,
                             TenLoai = l.TenLoai,
                             MauCollection = sp.Maus,
                             KichCoCollection = sp.KichCoes,
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
            var sp = db.SanPhams.Find(id);
            sp.AnhKhac = images;
            db.SaveChanges();
        }

        public SanPham laySanPhamTheoID(int id)
        {
            var model = db.SanPhams.Find(id);
            return model;
        }

        public bool insert(SanPhamViewModel sp)
        {
            // Nếu thêm thành công thì trả về true ngược lại thì false
            try
            {
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
                foreach (var item in SanPhamUpdate.Maus.ToList())
                {
                    deleteMau(SanPhamUpdate, item.ID);
                }

                //Cập nhật màu mới
                foreach (var id in sp.Mau.ToList())
                {
                    insertMau(SanPhamUpdate, id);
                }

                //Xóa đi kích cỡ cũ
                foreach (var item in SanPhamUpdate.KichCoes.ToList())
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
                var SanPham = db.SanPhams.FirstOrDefault(x => x.ID == id);

                //Xóa hết dữ liệu của sản phẩm trong bảng Mau_SanPham
                foreach (var item in SanPham.Maus.ToList())
                {
                    deleteMau(SanPham, item.ID);
                }

                //Xóa hết dữ liệu của sản phẩm trong bảng Mau_KichCo
                foreach (var item in SanPham.KichCoes.ToList())
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

        //Xóa hết dữ liệu của sản phẩm trong bảng Mau_SanPham
        public void deleteMau(SanPham sp, int MauID)
        {
            var Mau = db.Maus.FirstOrDefault(x => x.ID == MauID);
            sp.Maus.Remove(Mau);
        }

        //Xóa hết dữ liệu của sản phẩm trong bảng Mau_KichCo
        public void deleteKichCo(SanPham sp, int KichCoID)
        {
            var KichCo = db.KichCoes.FirstOrDefault(x => x.ID == KichCoID);
            sp.KichCoes.Remove(KichCo);
        }

        public void insertMau(SanPham sp, int MauID)
        {
            var m = db.Maus.Find(MauID);
            sp.Maus.Add(m);
        }
        public void insertKichCo(SanPham sp, int KichCoID)
        {
            var kc = db.KichCoes.Find(KichCoID);
            sp.KichCoes.Add(kc);
        }

    }
}
