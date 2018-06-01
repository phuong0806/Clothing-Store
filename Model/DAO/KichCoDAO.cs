using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class KichCoDAO
    {
        StoreDbContext db = null;

        public KichCoDAO()
        {
            db = new StoreDbContext();
        }

        public List<KichCo> getDanhSachKichCo()
        {
            var list = db.KichCoes.ToList();
            return list;
        }
    }
}
