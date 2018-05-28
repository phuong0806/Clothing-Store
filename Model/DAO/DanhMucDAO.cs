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
            var list = db.LoaiSanPhams.ToList();
            return list;
        }

        public bool insert(LoaiSanPham loai)
        {
            try
            {
                db.LoaiSanPhams.Add(loai);
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
                var loaisanpham = db.LoaiSanPhams.Find(loai.ID);
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
