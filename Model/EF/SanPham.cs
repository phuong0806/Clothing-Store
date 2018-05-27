namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        public int ID { get; set; }

        [StringLength(250)]
        public string TenSanPham { get; set; }

        public string MoTa { get; set; }

        public int Gia { get; set; }

        public int? MaLoai { get; set; }

        public int? MaThuongHieu { get; set; }

        [StringLength(250)]
        public string HinhAnh { get; set; }

        [StringLength(250)]
        public string Mau { get; set; }

        [StringLength(50)]
        public string KichCo { get; set; }

        public virtual LoaiSanPham LoaiSanPham { get; set; }

        public virtual ThuongHieu ThuongHieu { get; set; }
    }
}
