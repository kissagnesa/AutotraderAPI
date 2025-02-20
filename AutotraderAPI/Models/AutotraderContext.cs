using System;
using System.Collections.Generic;
using AutotraderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AutotraderApi.Models;

public partial class AutotraderContext : DbContext
{
    public AutotraderContext()
    {
    }

    public AutotraderContext(DbContextOptions<AutotraderContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

  /* Innen át kell tenni az appsettings.jsonbe a végén lévő zárójeles cuccot, minden más pedig törlendő
   * protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=autotrader;user=root;password=;sslmode=none;");*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cars");

            entity.Property(e => e.Brand)
                .HasMaxLength(30)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Color)
                .HasMaxLength(10)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("'current_timestamp()'")
                .HasColumnType("datetime");
            entity.Property(e => e.Myear)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("date")
                .HasColumnName("MYear");
            entity.Property(e => e.Type)
                .HasMaxLength(30)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UpdatedTime)
                .HasDefaultValueSql("'current_timestamp()'")
                .HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}