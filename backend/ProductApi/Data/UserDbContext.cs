using Microsoft.EntityFrameworkCore;
using ProductApi.Models;

namespace ProductApi.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                // Mapping ชี้เป้าไปที่ตาราง USER พิมพ์ใหญ่ในระบบของคุณ
                entity.ToTable("USER");

                // ตั้ง Primary Key และแปลง Property C# ให้คุยกับคอลัมน์ตัวพิมพ์เล็กใน SQL Server
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).HasColumnName("id");

                entity.Property(u => u.Username).HasColumnName("username").IsRequired().HasMaxLength(50);
                entity.Property(u => u.Fullname).HasColumnName("fullname").IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).HasColumnName("email").IsRequired().HasMaxLength(50);
                entity.Property(u => u.Role).HasColumnName("role").IsRequired().HasMaxLength(50);
            });
        }
    }
}