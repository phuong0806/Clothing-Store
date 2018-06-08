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

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<DanhMuc> DanhMucs { get; set; }
        public virtual DbSet<DonHang> DonHangs { get; set; }
        public virtual DbSet<DonHang_SanPham> DonHang_SanPhams { get; set; }
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
                .HasMany(e => e.SanPham)
                .WithMany(e => e.KichCo)
                .Map(m => m.ToTable("KichCo_SanPham").MapLeftKey("KichCoID").MapRightKey("SanPhamID"));

            modelBuilder.Entity<Loai>()
                .HasMany(e => e.SanPham)
                .WithOptional(e => e.Loai)
                .HasForeignKey(e => e.MaLoai);

            modelBuilder.Entity<Mau>()
                .HasMany(e => e.SanPham)
                .WithMany(e => e.Mau)
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
