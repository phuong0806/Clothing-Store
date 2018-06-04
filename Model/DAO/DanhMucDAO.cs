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

        public List<LoaiSanPham> getDanhMuc()
        {
            var list = db.LoaiSanPham.ToList();
            return list;
        }

        public LoaiSanPham layDanhMucTheoID(int id)
        {
            var entity = db.LoaiSanPham.Find(id);
            return entity;
        }

        public bool delete(int id)
        {
            try
            {
                var entity = db.LoaiSanPham.Find(id);
                db.LoaiSanPham.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool insert(LoaiSanPham loai)
        {
            try
            {
                db.LoaiSanPham.Add(loai);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool update(LoaiSanPham loai)
        {
            try
            {
                var loaisanpham = db.LoaiSanPham.Find(loai.ID);
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
