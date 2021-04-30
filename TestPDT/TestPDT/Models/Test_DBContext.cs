using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TestPDT.Models
{
    public partial class Test_DBContext : DbContext
    {
        public Test_DBContext()
        {
        }

        public Test_DBContext(DbContextOptions<Test_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contacto> Contactos { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=DESKTOP-5GHF8UJ\\MSSQLSERVER2012;Initial Catalog=Test_DB;User ID=sa;Password=gabriel;MultipleActiveResultSets=true");
//            }
//        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

        //    modelBuilder.Entity<Contacto>(entity =>
        //    {
        //        entity.Property(e => e.Curp)
        //            .IsRequired()
        //            .HasMaxLength(25)
        //            .IsUnicode(false);

        //        entity.Property(e => e.Direccion)
        //            .IsRequired()
        //            .HasMaxLength(200)
        //            .IsUnicode(false);

        //        entity.Property(e => e.FechaRegistro).HasColumnType("smalldatetime");

        //        entity.Property(e => e.Nombre)
        //            .IsRequired()
        //            .HasMaxLength(150)
        //            .IsUnicode(false);

        //        entity.Property(e => e.Telefono)
        //            .IsRequired()
        //            .HasMaxLength(15)
        //            .IsUnicode(false);
        //    });

        //    OnModelCreatingPartial(modelBuilder);
        //}

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
