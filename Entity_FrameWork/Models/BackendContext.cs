using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entity_FrameWork.Models;

public partial class BackendContext : DbContext
{
    public BackendContext()
    {
    }

    public BackendContext(DbContextOptions<BackendContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Item> Items { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=Backend;Username=postgres;Password=michaeladu");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("item_pkey");

            entity.ToTable("item", "account");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descriptio).HasColumnName("descriptio");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
