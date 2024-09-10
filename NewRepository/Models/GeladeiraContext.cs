using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace NewRepository.Models;

public partial class GeladeiraContext : DbContext
{

    public GeladeiraContext()
    {
        
    }

    public GeladeiraContext(DbContextOptions<GeladeiraContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Iten> Itens { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Iten>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Itens__3214EC077646237B");

            entity.Property(e => e.Container)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Posicao)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
