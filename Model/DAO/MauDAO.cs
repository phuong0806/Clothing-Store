using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class MauDAO
    {
        StoreDbContext db = null;

        public MauDAO()
        {
            db = new StoreDbContext();
        }

        public IEnumerable<MauViewModel> getDanhSach()
        {
            var result = (from mau in db.Maus
                          select new MauViewModel
                          {
                              ID = mau.ID,
                              Name = mau.Name,
                          }).ToList();

            return result;
        }


        public IEnumerable<MauViewModel> getDanhSachTheoSanPham(int SanPhamID)
        {
            var result = (from mau in db.Maus
                       where mau.SanPham.Any(x => x.ID == SanPhamID)
                       select new MauViewModel
                       {
                           ID = mau.ID,
                           Name = mau.Name,
                           Code = mau.Code
                       }).ToList();

            return result;
        }

        public MauViewModel layMauTheoID(int id)
        {
            var entity = (from mau in db.Maus
                          where mau.ID == id 
                          select new MauViewModel {
                              ID = mau.ID,
                              Code = mau.Code,
                              Name = mau.Name
                          }).SingleOrDefault();
            return entity;
        }

        public bool delete(int id)
        {
            try
            {
                var entity = db.Maus.Find(id);
                db.Maus.Remove(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool insert(Mau mau)
        {
            try
            {
                mau.Code = "#" + mau.Code;
                db.Maus.Add(mau);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool update(Mau mau)
        {
            try
            {
                var mauUpdate = db.Maus.Find(mau.ID);
                mauUpdate.Name = mau.Name;
                mauUpdate.Code = "#" + mau.Code;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Mau> layDanhSachMau()
        {
            var list = db.Maus.ToList();
            return list;
        }
    }
}
