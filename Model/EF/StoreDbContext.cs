namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class StoreDbContext : DbContext
    {
        public StoreDbContext()
            : base("name=StoreDbContext")
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<DonHang> DonHang { get; set; }
        public virtual DbSet<LoaiSanPham> LoaiSanPham { get; set; }
        public virtual DbSet<NguoiDung> NguoiDung { get; set; }
        public virtual DbSet<SanPham> SanPham { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoan { get; set; }
        public virtual DbSet<ThongKe> ThongKe { get; set; }
        public virtual DbSet<ThuongHieu> ThuongHieu { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoaiSanPham>()
                .HasMany(e => e.SanPham)
                .WithOptional(e => e.LoaiSanPham)
                .HasForeignKey(e => e.MaLoai);

            modelBuilder.Entity<ThuongHieu>()
                .HasMany(e => e.SanPham)
                .WithOptional(e => e.ThuongHieu)
                .HasForeignKey(e => e.MaThuongHieu);
        }
    }
}
