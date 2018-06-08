using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.EF
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GioHang")]
    public partial class GioHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GioHang()
        {
            GioHang_SanPham = new HashSet<GioHang_SanPham>();
        }
        public int ID { get; set; }

        [StringLength(250)]
        public string TenSanPham { get; set; }

        [StringLength(250)]
        public string HinhAnh { get; set; }

        public int? Gia { get; set; }

        public int? SoLuong { get; set; }

        public double ThanhTien
        {
            get { return SoLuong * Gia; }
        }
        //Hàm tạo cho giỏ hàng

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GioHang_SanPham> GioHang_SanPham { get; set; }
    }
    }
