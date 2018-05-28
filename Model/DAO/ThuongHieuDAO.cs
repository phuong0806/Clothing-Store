using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ThuongHieuDAO
    {
        StoreDbContext db = null;

        public ThuongHieuDAO()
        {
            db = new StoreDbContext();
        }

        public List<ThuongHieu> getTatCaThuongHieu()
        {
            var model = db.ThuongHieux.ToList();
            return model;
        }

        public ThuongHieu layThuongHieuTheoID(int id)
        {
            var entity = db.ThuongHieux.Find(id);
            return entity;
        }

        public bool delete(int id)
        {
            try
            {
                var entity = db.ThuongHieux.Find(id);
                db.ThuongHieux.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool insert(ThuongHieu loai)
        {
            try
            {
                db.ThuongHieux.Add(loai);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool update(ThuongHieu loai)
        {
            try
            {
                var loaisanpham = db.ThuongHieux.Find(loai.ID);
                loaisanpham.TenThuongHieu = loai.TenThuongHieu;
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
