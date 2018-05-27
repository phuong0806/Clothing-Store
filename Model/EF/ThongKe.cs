namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongKe")]
    public partial class ThongKe
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int? LuotXem { get; set; }

        public int? LuotDatHang { get; set; }

        public int? MaSanPham { get; set; }
    }
}
