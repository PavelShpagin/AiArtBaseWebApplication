using System;
using System.Collections.Generic;
using AiArtBaseWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace AiArtBaseWebApplication;

public partial class DbartbaseContext : DbContext
{
    public DbartbaseContext()
    {
    }

    public DbartbaseContext(DbContextOptions<DbartbaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Art> Arts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-ME0D6IK\\SQLEXPRESS;Database=DBArtbase; Trusted_Connection=True; Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Art>(entity =>
        {
            entity.Property(e => e.Image).HasColumnType("image");

            entity.HasMany(d => d.Categories).WithMany(p => p.Arts)
                .UsingEntity<Dictionary<string, object>>(
                    "ArtCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Art_Category_Category"),
                    l => l.HasOne<Art>().WithMany()
                        .HasForeignKey("ArtId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Art_Category_Arts"),
                    j =>
                    {
                        j.HasKey("ArtId", "CategoryId").HasName("PK_Art_Category");
                        j.ToTable("ArtCategories");
                        j.IndexerProperty<long>("CategoryId").ValueGeneratedOnAdd();
                    });
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.AvatarImage).HasColumnType("image");
            entity.Property(e => e.Email).HasMaxLength(320);
            entity.Property(e => e.FirstName).HasMaxLength(320);
            entity.Property(e => e.SecondName).HasMaxLength(320);
            entity.Property(e => e.Username).HasMaxLength(320);

            entity.HasMany(d => d.Arts).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Like",
                    r => r.HasOne<Art>().WithMany()
                        .HasForeignKey("ArtId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Likes_Arts"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Likes_Users"),
                    j =>
                    {
                        j.HasKey("UserId", "ArtId");
                        j.ToTable("Likes");
                    });

            entity.HasMany(d => d.ArtsNavigation).WithMany(p => p.UsersNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "Own",
                    r => r.HasOne<Art>().WithMany()
                        .HasForeignKey("ArtId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Table_1_Table_1"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Owns_Users"),
                    j =>
                    {
                        j.HasKey("UserId", "ArtId").HasName("PK_Table_1");
                        j.ToTable("Owns");
                    });

            entity.HasMany(d => d.Followers).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Follow",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Follow_Users"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Follows_Users"),
                    j =>
                    {
                        j.HasKey("FollowerId", "UserId").HasName("PK_Follow_1");
                        j.ToTable("Follows");
                    });

            entity.HasMany(d => d.Users).WithMany(p => p.Followers)
                .UsingEntity<Dictionary<string, object>>(
                    "Follow",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Follows_Users"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Follow_Users"),
                    j =>
                    {
                        j.HasKey("FollowerId", "UserId").HasName("PK_Follow_1");
                        j.ToTable("Follows");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
