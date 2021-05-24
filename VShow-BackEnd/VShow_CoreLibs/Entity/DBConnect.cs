using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VShow_CoreLibs.Entity.Tables;

namespace VShow_BackEnd.Entity
{
    /// <summary>
    /// สำหรับเชื่อมต่อและ Setting Database
    /// </summary>
    public partial class DBConnect : DbContext
    {
        /// <summary>
        /// สำหรับค้นหาตัวแปรของ appsetting.json
        /// </summary>
        public readonly IConfiguration Configuration;

        public DBConnect(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// ตั้งค่าเพิ่มเติมเมื่อเกิด Event การสร้าง Model
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Tables
            modelBuilder.HasAnnotation("Relational:Collation", "Thai_CI_AS");

            modelBuilder.Entity<AccountProvider>(entity =>
            {
                entity.HasKey(e => e.AccId)
                    .HasName("PK__tmp_ms_x__91CBC378559E4330");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.HashPassword).HasMaxLength(500);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.ProviderType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Username).HasMaxLength(100);
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__tmp_ms_x__1788CC4C473B3AF2");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.FirstName).HasMaxLength(250);

                entity.Property(e => e.Image).HasMaxLength(20);

                entity.Property(e => e.LastName).HasMaxLength(250);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UserRole)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('User')");

                entity.Property(e => e.VerifyCode).HasMaxLength(50);
            });
            #endregion
            #region Views

            #endregion

            OnModelCreatingPartial(modelBuilder);
        }

        /// <summary>
        /// ตั้งค่าการเชื่อมต่อ Connection String ของ Database
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("ConnectDatabase"));
            }
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}