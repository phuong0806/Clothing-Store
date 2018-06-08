using Model.EF;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class DonHangDAO
    {
        StoreDbContext db = null;

        public DonHangDAO()
        {
            db = new StoreDbContext();
        }

        public IEnumerable<DonHangViewModel> getDanhSach()
        {
            var list = (from dh in db.DonHangs
                        select new DonHangViewModel()
                        {
                            ID = dh.ID,
                            NgayTao = dh.NgayTao,
                            NgayDuyet = dh.NgayDuyet,
                            NgayHoanThanh = dh.NgayHoanThanh,
                            TinhTrang = dh.TinhTrang,
                            XacNhan = dh.XacNhan,
                            TongTien = dh.TongTien,
                        }).ToList();
            return list;
        } 

        public bool insert(List<SanPhamGioHang> listSanPham, DonHang donHang)
        {
                DonHang DonHangNew = new DonHang();

                var max = 0;

                if (db.DonHangs.Count() > 0)
                {
                    max = db.DonHangs.Max(x => x.ID);
                }
                else
                {
                    DonHangNew.ID = 111111111;
                }

                if (max > 0)
                {
                    DonHangNew.ID = max + 1;
                }

                DonHangNew.TenKH = donHang.TenKH;
                DonHangNew.DiaChiGiaoHang = donHang.DiaChiGiaoHang;
                DonHangNew.SDT = donHang.SDT;
                DonHangNew.Email = donHang.Email;
                DonHangNew.TongTien = donHang.TongTien;
                DonHangNew.XacNhan = false;
                DonHangNew.TinhTrang = 1;
                DonHangNew.NgayTao = DateTime.Now;
                DonHangNew.NgayDuyet =  DateTime.Now;
                DonHangNew.NgayHoanThanh = DateTime.Now;
                db.DonHangs.Add(DonHangNew);
                db.SaveChanges();
                foreach (var item in listSanPham)
                {
                    MauViewModel mau = db.Maus.Where(y => y.ID == item.Color).Select(x => new MauViewModel { ID = x.ID, Name = x.Name, Code = x.Code }).SingleOrDefault();
                    KichCoViewModel kichCo = db.KichCoes.Where(y => y.ID == item.Size).Select(x => new KichCoViewModel { ID = x.ID, Name = x.Name }).SingleOrDefault();
                    var sp = db.SanPhams.Find(item.ID);
                    DonHang_SanPham donHangSanPham = new DonHang_SanPham();
                    donHangSanPham.DonHangID = DonHangNew.ID;
                    donHangSanPham.SanPhamID = sp.ID;
                    donHangSanPham.KichCo = kichCo.Name;
                    donHangSanPham.Mau = mau.Name;
                    donHangSanPham.SoLuong = item.Amount;
                    donHangSanPham.Gia = sp.Gia;
                    donHangSanPham.TongTien = donHangSanPham.Gia * donHangSanPham.SoLuong;
                    donHangSanPham.SanPham = sp;
                    donHangSanPham.DonHang = DonHangNew;
                    db.DonHang_SanPham.Add(donHangSanPham);
                }
                db.SaveChanges();
                return true;
     
        }
    }
}
