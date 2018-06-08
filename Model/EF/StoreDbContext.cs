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

<<<<<<< HEAD
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
=======
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<DanhMuc> DanhMucs { get; set; }
        public virtual DbSet<DonHang> DonHangs { get; set; }
        public virtual DbSet<DonHang_SanPham> DonHang_SanPham { get; set; }
        public virtual DbSet<KichCo> KichCoes { get; set; }
        public virtual DbSet<Loai> Loais { get; set; }
        public virtual DbSet<Mau> Maus { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<ThuongHieu> ThuongHieux { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DonHang>()
                .HasMany(e => e.DonHang_SanPham)
                .WithRequired(e => e.DonHang)
                .HasForeignKey(e => e.SanPhamID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KichCo>()
                .HasMany(e => e.SanPhams)
                .WithMany(e => e.KichCoes)
                .Map(m => m.ToTable("KichCo_SanPham").MapLeftKey("KichCoID").MapRightKey("SanPhamID"));

            modelBuilder.Entity<Loai>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.Loai)
>>>>>>> e21535ef34dc1c16d6989a9a77fa6a21967d8bf5
                .HasForeignKey(e => e.MaLoai);

            modelBuilder.Entity<Mau>()
                .HasMany(e => e.SanPhams)
                .WithMany(e => e.Maus)
                .Map(m => m.ToTable("Mau_SanPham").MapLeftKey("MauID").MapRightKey("SanPhamID"));

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.DonHang_SanPham)
                .WithRequired(e => e.SanPham)
                .HasForeignKey(e => e.DonHangID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ThuongHieu>()
                .HasMany(e => e.SanPham)
                .WithOptional(e => e.ThuongHieu)
                .HasForeignKey(e => e.MaThuongHieu);
        }
    }
}
