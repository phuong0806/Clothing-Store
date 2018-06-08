using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class DonHangViewModel
    {
        public int ID { get; set; }
        public string TenKH { get; set; }
        public string DiaChiGiaoHang { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public int? TongTien { get; set; }
        public bool? XacNhan { get; set; }
        public int? TinhTrang { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayDuyet { get; set; }
        public DateTime? NgayHoanThanh { get; set; }
        public IEnumerable<SanPhamViewModel> DanhSachSanPham { get; set; }

    }
}
