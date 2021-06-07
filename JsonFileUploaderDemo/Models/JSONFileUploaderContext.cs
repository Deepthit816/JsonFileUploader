using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace JsonFileUploaderDemo.Models
{
    public partial class JSONFileUploaderContext : DbContext
    {
        public JSONFileUploaderContext()
        {
        }

        public JSONFileUploaderContext(DbContextOptions<JSONFileUploaderContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FileDetail> FileDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQL2019;Database=JSONFileUploader;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<FileDetail>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("FILE_DETAILS");

                entity.Property(e => e.Guid).HasColumnName("GUID");

                entity.Property(e => e.FileBlob)
                    .IsRequired()
                    .HasColumnName("FILE_BLOB");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FILE_NAME");

                entity.Property(e => e.FileSize).HasColumnName("FILE_SIZE");

                entity.Property(e => e.FileType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FILE_TYPE")
                    .HasDefaultValueSql("('JSON')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
