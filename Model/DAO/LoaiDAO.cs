using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class LoaiDAO
    {
        StoreDbContext db = null;

        public LoaiDAO()
        {
            db = new StoreDbContext();
        }

        public IEnumerable<LoaiViewModel> getDanhSach()
        {
<<<<<<< HEAD
            var model = db.LoaiSanPham.ToList();
=======
            var result = (from loai in db.Loais
                          select new LoaiViewModel
                          {
                              ID = loai.ID,
                              TenLoai = loai.TenLoai,
                              DanhMucID = loai.DanhMucID
                              
                          }).ToList();

            return result;
        }

        public IEnumerable<LoaiViewModel> layDanhSachLoaiTheoID(int DanhMucID)
        {
            var model = (from loai in db.Loais
                         where loai.DanhMucID == DanhMucID
                         select new LoaiViewModel
                         {
                             ID = loai.ID,
                             TenLoai = loai.TenLoai
                         }).ToList();
>>>>>>> e21535ef34dc1c16d6989a9a77fa6a21967d8bf5
            return model;
        }

        public Loai layLoaiTheoID(int id)
        {
            var entity = db.Loais.Find(id);
            return entity;
        }

        public bool delete(int id)
        {
            try
            {
                var entity = db.Loais.Find(id);
                db.Loais.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool insert(Loai loai)
        {
            try
            {
                db.Loais.Add(loai);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool update(Loai loai)
        {
            try
            {
                var loaisanpham = db.Loais.Find(loai.ID);
                loaisanpham.TenLoai = loai.TenLoai;
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
