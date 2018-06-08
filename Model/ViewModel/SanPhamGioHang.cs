using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class SanPhamGioHang
    {
        public int ID { get; set; }
        public string HinhAnh { get; set; }
        public string TenSanPham { get; set; }
        public int Color { get; set; }
        public int Size { get; set; }
        public int? Amount { get; set; }
        public string MauString { get; set; }
        public string SizeString { get; set; }
        public string TongTienString { get; set; }
        public int? SoLuong { get; set; }
        public int? Gia { get; set; }
        public int? TongTien { get; set; }
        public IEnumerable<MauViewModel> Mau { get; set; }
        public IEnumerable<KichCoViewModel> KichCo { get; set; }
    }
}
