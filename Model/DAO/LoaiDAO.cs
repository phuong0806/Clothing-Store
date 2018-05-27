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

        public List<LoaiSanPham> getTatCaLoai()
        {
            var model = db.LoaiSanPham.ToList();
            return model;
        }
    }
}
