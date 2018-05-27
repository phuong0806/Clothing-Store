namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguoiDung")]
    public partial class NguoiDung
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(50)]
        public string HotenND { get; set; }

        [StringLength(50)]
        public string DiaChiND { get; set; }

        [StringLength(50)]
        public string SDTND { get; set; }

        [StringLength(50)]
        public string GioiTinh { get; set; }
    }
}
