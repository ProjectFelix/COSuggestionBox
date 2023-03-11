using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace COSuggestionBox.Models;

public partial class SuggestionContext : DbContext
{
    public SuggestionContext()
    {
    }

    public SuggestionContext(DbContextOptions<SuggestionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Suggestion> Suggestions { get; set; }

    public virtual DbSet<SuggestionStatus> SuggestionStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=JAMES_DB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Suggestion>(entity =>
        {
            entity.ToTable("Suggestion");

            entity.Property(e => e.Comment).HasMaxLength(256);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateFinished).HasColumnType("datetime");
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");
        });

        modelBuilder.Entity<SuggestionStatus>(entity =>
        {
            entity.ToTable("SuggestionStatus");

            entity.Property(e => e.Status).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
