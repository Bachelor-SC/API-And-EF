using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using ScSoMeAPI.Models;
using ScSoMeAPI.Models.UserData;

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
    public virtual DbSet<Block> Blocks { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserInfoDB> UserInfo { get; set; }

    public virtual DbSet<SocialMedia> SocialMedia { get; set; }
    public virtual DbSet<ExternalLink> ExternalLink { get; set; }
    public virtual DbSet<Location> UserLocations { get; set; }


    public virtual DbSet<Comment> Comment { get; set; }

    public virtual DbSet<Post> Post { get; set; }

    public virtual DbSet<Coach> Coaches { get; set; }

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
        modelBuilder.Entity<Coach>(entity =>
        {
            entity.HasKey(e => new { e.username }).HasName("PK__Coaches__F3DBC5738FCC8819");

            entity.ToTable("Coaches");

            entity.Property(e => e.username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.shortDesc)
                .HasMaxLength(100)
                .HasColumnName("shortDesc");
            entity.Property(e => e.content)
                .HasMaxLength(8000)
                .HasColumnName("content");
            entity.Property(e => e.picture)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("picture");
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

        // BLOCKS TABLE
        modelBuilder.Entity<Block>(entity =>
        {
            entity.ToTable("Blocks");
            entity.HasKey(e => new { e.UsernameCon1, e.UsernameCon2 }).HasName("PK__Blocks__EA82CC79A8B974E8");

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

            entity.ToTable("Users");

            entity.HasKey(e => e.Username).HasName("PK__Users__536C85E544CB7113");

            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.HashedPassword)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SubscriptionLevel)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserInfoDB>(entity =>
        {
            entity.ToTable("UserInfo");

            entity.HasKey(e => e.Username).HasName("PK__UserInfo__536C85E5A5D72B09");

            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Description)
                .HasMaxLength(int.MaxValue)
                .IsUnicode(false);
            entity.Property(e => e.ProfilePicture)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CoverPicture)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Phonenumber)
                .IsRequired()
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("UserLocations");

            entity.HasKey(e => e.Username).HasName("PK__UserLoca__536C85E55A3AA776");

            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Username");


            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PostalCode)
                .IsRequired()
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ExternalLink>(entity =>
        {
            entity.ToTable("ExternalLink");

            entity.HasKey(e => e.Username).HasName("PK__External__C8EEF8230E3DAB09");

            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Username");

            entity.Property(e => e.Link)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SocialMedia>(entity =>
        {
            entity.ToTable("SocialMedia");

            entity.HasKey(e => e.Username).HasName("PK__SocialMe__536C85E5A16B43BB");

            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Username");

            entity.Property(e => e.Facebook)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.LinkedIn)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.Instagram)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.commentID).HasName("PK__Comments__C3B4DFAA7A642927");

            entity.ToTable("Comments");

            entity.Property(e => e.username)
                .HasMaxLength(255)
                .HasColumnName("Username");
            entity.Property(e => e.comment)
                .HasMaxLength(8000)
                .HasColumnName("Comment");
            entity.Property(e => e.likes)
                .HasColumnName("Likes");
            entity.Property(e => e.postID)
                .HasColumnName("PostID");
            entity.Property(e => e.createdDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("createdDate");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.postID).HasName("PK__Posts__AA1260385E437CE9");

            entity.ToTable("Posts");

            entity.Property(e => e.username)
                .HasMaxLength(255)
                .HasColumnName("Username");
            entity.Property(e => e.content)
                .HasMaxLength(8000)
                .HasColumnName("Content");
            entity.Property(e => e.likes)
                .HasColumnName("Likes");
            entity.Property(e => e.createdDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("createdDate");
        });

        OnModelCreatingPartial(modelBuilder);


    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
