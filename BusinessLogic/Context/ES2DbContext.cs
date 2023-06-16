using System;
using System.Collections.Generic;
using BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Context
{
    public partial class ES2DbContext : DbContext
    {
        public ES2DbContext()
        {
        }

        public ES2DbContext(DbContextOptions<ES2DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=es2;Username=es2;Password=es2;SearchPath=public;Port=5480");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.HasPostgresExtension("postgis");
    modelBuilder.HasPostgresExtension("uuid-ossp");
    modelBuilder.HasPostgresExtension("topology", "postgis_topology");

    modelBuilder.Entity<Author>(entity =>
    {
        entity.HasKey(e => e.Id).HasName("authors_pkey");

        entity.ToTable("authors");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("uuid_generate_v4()")
            .HasColumnName("id");
        entity.Property(e => e.BirthDate).HasColumnName("birth_date");
        entity.Property(e => e.FirstName)
            .HasMaxLength(100)
            .HasColumnName("first_name");
        entity.Property(e => e.LastName)
            .HasMaxLength(100)
            .HasColumnName("last_name");
    });

    modelBuilder.Entity<Book>(entity =>
    {
        entity.HasKey(e => e.Id).HasName("books_pkey");

        entity.ToTable("books");

        entity.Property(e => e.Id)
            .HasDefaultValueSql("uuid_generate_v4()")
            .HasColumnName("id");
        entity.Property(e => e.AuthorId).HasColumnName("author_id");
        entity.Property(e => e.PublicationYear).HasColumnName("publication_year");
        entity.Property(e => e.Status)
            .HasMaxLength(20)
            .HasColumnName("status");
        entity.Property(e => e.Title)
            .HasMaxLength(255)
            .HasColumnName("title");

        entity.HasOne(d => d.Author).WithMany(p => p.Books)
            .HasForeignKey(d => d.AuthorId)
            .HasConstraintName("books_author_id_fkey");
    });

    modelBuilder.Entity<User>(entity =>
    {
        entity.HasKey(e => e.UserId).HasName("users_pkey");

        entity.ToTable("users");

        entity.Property(e => e.UserId)
            .HasDefaultValueSql("uuid_generate_v4()")
            .HasColumnName("userid");
        entity.Property(e => e.Email)
            .HasMaxLength(255)
            .HasColumnName("email");
        entity.Property(e => e.Username)
            .HasMaxLength(100)
            .HasColumnName("username");
        entity.Property(e => e.Password)
            .HasMaxLength(255)
            .HasColumnName("password");
        entity.Property(e => e.Name)
            .HasMaxLength(100)
            .HasColumnName("name");
        entity.Property(e => e.Role)
            .HasMaxLength(50)
            .HasColumnName("role");
        entity.Property(e => e.CreatedAt)
            .HasColumnName("createdat")
            .HasColumnType("timestamp")
            .HasDefaultValueSql("NOW()");
        entity.Property(e => e.Enable)
            .HasColumnName("enable")
            .IsRequired();
    });

    OnModelCreatingPartial(modelBuilder);
}
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
