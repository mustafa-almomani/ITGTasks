using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SaveErrorFile.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ITG-MMMOMANI\\MSSQLSERVER01;Database=Task1;User Id=test;Password=Ab@123456;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ErrorLog__3214EC072E5A0D5B");

            entity.Property(e => e.LoggedAt)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Source).HasMaxLength(255);
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__language__3213E83F3985C54A");

            entity.ToTable("language");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Language1)
                .HasMaxLength(255)
                .HasColumnName("language");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USERS__3213E83FB74F01F9");

            entity.ToTable("USERS");

            entity.HasIndex(e => e.NationalId, "UQ__USERS__2C578784B88D8970").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ_username").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientName)
                .HasMaxLength(50)
                .HasColumnName("clientNAME");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Img)
                .HasMaxLength(255)
                .HasColumnName("IMG");
            entity.Property(e => e.LangId).HasColumnName("lang_ID");
            entity.Property(e => e.Language).HasMaxLength(255);
            entity.Property(e => e.NationalId).HasColumnName("National_ID");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("userNAME");
            entity.Property(e => e.Usertype).HasColumnName("usertype");

            entity.HasOne(d => d.Lang).WithMany(p => p.Users)
                .HasForeignKey(d => d.LangId)
                .HasConstraintName("FK_users_language");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
