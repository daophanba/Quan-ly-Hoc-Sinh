using Microsoft.EntityFrameworkCore;
using QuanLyHocSinh.QuanLyLopHocDB.Entity;
using QuanLyLopHoc.QuanLyLopHocDB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyLopHoc.QuanLyLopHocDB
{
    public class QuanLyLopHocContext : DbContext
    {
        public QuanLyLopHocContext(DbContextOptions<QuanLyLopHocContext> options) : base(options)
        {

        }
        public QuanLyLopHocContext()
        {
        }
        public DbSet<LopHoc> LopHocs { get; set; }
        public DbSet<HocSinh> HocSinhs { get; set; }
        public DbSet<DangNhap> DangNhaps { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LopHoc>().ToTable("LopHoc");
            modelBuilder.Entity<HocSinh>().ToTable("HocSinh");
            modelBuilder.Entity<DangNhap>().ToTable("DangNhap");

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=QuanLyLopHoc;Trusted_Connection=true;");
        }


    }
}
