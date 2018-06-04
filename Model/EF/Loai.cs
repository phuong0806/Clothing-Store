namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Loai")]
    public partial class Loai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Loai()
        {
            SanPham = new HashSet<SanPham>();
        }

<<<<<<< HEAD:Model/EF/LoaiSanPham.cs
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
=======
>>>>>>> e21535ef34dc1c16d6989a9a77fa6a21967d8bf5:Model/EF/Loai.cs
        public int ID { get; set; }

        [StringLength(50)]
        public string TenLoai { get; set; }

        public int? DanhMucID { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPham { get; set; }
    }
}
