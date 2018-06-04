using Model.EF;
using Model.ViewModel;
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

        public IEnumerable<KichCoViewModel> getDanhSach()
        {
            var result = (from kichco in db.KichCoes
                          select new KichCoViewModel
                          {
                              ID = kichco.ID,
                              Name = kichco.Name,
                          }).ToList();

            return result;
        }

        public IEnumerable<KichCoViewModel> getDanhSachTheoSanPham(int SanPhamID)
        {
            var result = (from kichco in db.KichCoes
                          where kichco.SanPhams.Any(x => x.ID == SanPhamID)
                          select new KichCoViewModel
                          {
                              ID = kichco.ID,
                              Name = kichco.Name,
                          }).ToList();

            return result;
        }
    }
}
