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
<<<<<<< HEAD
            var list = db.LoaiSanPham.ToList();
=======
            var list = db.DanhMucs.ToList();
>>>>>>> e21535ef34dc1c16d6989a9a77fa6a21967d8bf5
            return list;
        }

        public int? layIdDanhMucTheoLoai(int? id)
        {
<<<<<<< HEAD
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
=======
            var result = db.Loais.Find(id);
            return result.DanhMucID;
        }


>>>>>>> e21535ef34dc1c16d6989a9a77fa6a21967d8bf5
    }
}
