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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            DonHang_SanPham = new HashSet<DonHang_SanPham>();
            KichCoes = new HashSet<KichCo>();
            Maus = new HashSet<Mau>();
        }

        public int ID { get; set; }

        [StringLength(250)]
        public string TenSanPham { get; set; }

        [StringLength(250)]
        public string Url { get; set; }

        public string MoTa { get; set; }

        public int? Gia { get; set; }

        [StringLength(250)]
        public string HinhAnh { get; set; }

        [Column(TypeName = "xml")]
        public string AnhKhac { get; set; }

        public int? MaLoai { get; set; }

        public int? MaThuongHieu { get; set; }

        public int? SoLuong { get; set; }

        public bool? TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHang_SanPham> DonHang_SanPham { get; set; }

        public virtual Loai Loai { get; set; }

        public virtual ThuongHieu ThuongHieu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KichCo> KichCoes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mau> Maus { get; set; }
    }
}
