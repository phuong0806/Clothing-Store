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

    }
}
