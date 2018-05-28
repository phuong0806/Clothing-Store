using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
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

        public List<SanPhamViewModel> layDanhSachTatCaSanPham()
        {
            var model = (from sp in db.SanPhams
                         join l in db.LoaiSanPhams on sp.MaLoai equals l.ID
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
                             Mau = sp.Mau,
                             KichCo = sp.KichCo
                         }).ToList();

            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
            foreach (var item in model)
            {
                item.GiaString = double.Parse(item.Gia.ToString()).ToString("#,###", cul.NumberFormat);
            }

            return model;
        }

        public SanPham laySanPhamTheoID(int id)
        {
            var model = db.SanPhams.Find(id);
            return model;
        }

        public bool insert(SanPham sp)
        {
            try
            {
                db.SanPhams.Add(sp);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool update(int id)
        {
            throw new NotImplementedException();
        }

        public bool update(SanPham sp)
        {
            try
            {
                var entity = db.SanPhams.Find(sp.ID);
                entity.TenSanPham = sp.TenSanPham;
                entity.MoTa = sp.MoTa;
                entity.MaLoai = sp.MaLoai;
                entity.MaThuongHieu = sp.MaThuongHieu;
                entity.HinhAnh = sp.HinhAnh;
                entity.Mau = sp.Mau;
                entity.KichCo = sp.KichCo;
                entity.Gia = sp.Gia;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool delete(int id)
        {
            try
            {
                var entity = db.SanPhams.Find(id);
                db.SanPhams.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
