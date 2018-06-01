using Model.EF;
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

        public List<Loai> getTatCaLoai()
        {
            var model = db.Loais.ToList();
            return model;
        }

        public List<Loai> layDanhSachLoaiTheoID(int DanhMucID)
        {
            var model = db.Loais.Where(x => x.DanhMucID == DanhMucID).ToList();
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
