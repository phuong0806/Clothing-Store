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
            var result = db.Loais.Find(id);
            return result.DanhMucID;
        }


    }
}
