using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ScSoMeAPI.Models;

namespace ScSoMeAPI;

public partial class BachelordbContext : DbContext
{
    public BachelordbContext()
    {
    }

    public BachelordbContext(DbContextOptions<BachelordbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<Connection> Connections { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("connString"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => new { e.Action, e.Date, e.Username }).HasName("PK__Activity__8B179CCD090305B3");

            entity.ToTable("Activity");

            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .HasColumnName("action");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.AdditionalInfo)
                .HasMaxLength(250)
                .HasColumnName("additionalInfo");
            entity.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Connection>(entity =>
        {
            entity.HasKey(e => new { e.UsernameCon1, e.UsernameCon2 }).HasName("PK__Connecti__EA82CC7956863E80");

            entity.Property(e => e.UsernameCon1)
                .HasMaxLength(50)
                .HasColumnName("usernameCon1");
            entity.Property(e => e.UsernameCon2)
                .HasMaxLength(50)
                .HasColumnName("usernameCon2");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("createdDate");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__Users__536C85E543016B33");

            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.HashedPassword)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
