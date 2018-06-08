namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonHang")]
    public partial class DonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHang()
        {
            DonHang_SanPham = new HashSet<DonHang_SanPham>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(50)]
        public string TenKH { get; set; }

        [StringLength(250)]
        public string DiaChiGiaoHang { get; set; }

        [StringLength(50)]
        public string SDT { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public int? TongTien { get; set; }

        public bool? XacNhan { get; set; }

        public int? TinhTrang { get; set; }

        public DateTime? NgayTao { get; set; }

        public DateTime? NgayDuyet { get; set; }

        public DateTime? NgayHoanThanh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHang_SanPham> DonHang_SanPham { get; set; }
    }
}
