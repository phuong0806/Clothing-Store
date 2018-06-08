using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    //data accesss object
    public class DanhMucDAO
    {
        StoreDbContext db = null;

        public DanhMucDAO()
        {
            db = new StoreDbContext();
        }

        public List<DanhMuc> getDanhMuc()
        {
            var list = db.DanhMucs.ToList();
            return list;
        }

        public int? layIdDanhMucTheoLoai(int? id)
        {
            var entity = db.Loais.Find(id);
            return entity.ID;
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
