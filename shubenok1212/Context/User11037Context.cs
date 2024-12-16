using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using shubenok1212.Models;

namespace shubenok1212.Context;

public partial class User11037Context : DbContext
{
    public User11037Context()
    {
    }

    public User11037Context(DbContextOptions<User11037Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<Manuf> Manufs { get; set; }

    public virtual DbSet<Prod> Prods { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=192.168.7.159;Port=5432;Database=user11037;Username=user11037;Password=24731");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => e.IdActiv).HasName("activity_pk");

            entity.ToTable("activity", "books");

            entity.Property(e => e.IdActiv)
                .ValueGeneratedNever()
                .HasColumnName("id_activ");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Manuf>(entity =>
        {
            entity.HasKey(e => e.IdManuf).HasName("manuf_pk");

            entity.ToTable("manuf", "books");

            entity.Property(e => e.IdManuf)
                .ValueGeneratedNever()
                .HasColumnName("id_manuf");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Prod>(entity =>
        {
            entity.HasKey(e => e.IdProd).HasName("prods_pk");

            entity.ToTable("prods", "books");

            entity.Property(e => e.IdProd)
                .ValueGeneratedNever()
                .HasColumnName("id_prod");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.Descr)
                .HasColumnType("character varying")
                .HasColumnName("descr");
            entity.Property(e => e.IdActiv).HasColumnName("id_activ");
            entity.Property(e => e.IdManuf).HasColumnName("id_manuf");
            entity.Property(e => e.Image)
                .HasColumnType("character varying")
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");

            entity.HasOne(d => d.IdActivNavigation).WithMany(p => p.Prods)
                .HasForeignKey(d => d.IdActiv)
                .HasConstraintName("prods_activity_fk");

            entity.HasOne(d => d.IdManufNavigation).WithMany(p => p.Prods)
                .HasForeignKey(d => d.IdManuf)
                .HasConstraintName("prods_manuf_fk");

            entity.HasMany(d => d.IdDops).WithMany(p => p.IdMains)
                .UsingEntity<Dictionary<string, object>>(
                    "DopProd",
                    r => r.HasOne<Prod>().WithMany()
                        .HasForeignKey("IdDop")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("dop_prod_prods_fk_1"),
                    l => l.HasOne<Prod>().WithMany()
                        .HasForeignKey("IdMain")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("dop_prod_prods_fk"),
                    j =>
                    {
                        j.HasKey("IdMain", "IdDop").HasName("dop_prod_pk");
                        j.ToTable("dop_prod", "books");
                        j.IndexerProperty<int>("IdMain").HasColumnName("id_main");
                        j.IndexerProperty<int>("IdDop").HasColumnName("id_dop");
                    });

            entity.HasMany(d => d.IdMains).WithMany(p => p.IdDops)
                .UsingEntity<Dictionary<string, object>>(
                    "DopProd",
                    r => r.HasOne<Prod>().WithMany()
                        .HasForeignKey("IdMain")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("dop_prod_prods_fk"),
                    l => l.HasOne<Prod>().WithMany()
                        .HasForeignKey("IdDop")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("dop_prod_prods_fk_1"),
                    j =>
                    {
                        j.HasKey("IdMain", "IdDop").HasName("dop_prod_pk");
                        j.ToTable("dop_prod", "books");
                        j.IndexerProperty<int>("IdMain").HasColumnName("id_main");
                        j.IndexerProperty<int>("IdDop").HasColumnName("id_dop");
                    });
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.IdSale).HasName("sale_pk");

            entity.ToTable("sale", "books");

            entity.Property(e => e.IdSale)
                .ValueGeneratedNever()
                .HasColumnName("id_sale");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.IdProd).HasColumnName("id_prod");
            entity.Property(e => e.Time).HasColumnName("time");

            entity.HasOne(d => d.IdProdNavigation).WithMany(p => p.Sales)
                .HasForeignKey(d => d.IdProd)
                .HasConstraintName("sale_prods_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
