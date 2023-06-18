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
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<UserEvent> UserEvents { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        
        public DbSet<UserActivity> UserActivities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseNpgsql(
                    "Host=localhost;Database=es2;Username=es2;Password=es2;SearchPath=public;Port=5480");
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

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.EventId).HasName("events_pkey");

                entity.ToTable("events");

                entity.Property(e => e.EventId)
                    .HasDefaultValueSql("uuid_generate_v4()")
                    .HasColumnName("eventid");
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("NOW()");
                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .HasColumnName("location");
                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");
                entity.Property(e => e.MaxCapacity)
                    .HasColumnName("maxcapacity")
                    .IsRequired();
                entity.Property(e => e.UserId).HasColumnName("userid");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.TicketId).HasName("tickets_pkey");

                entity.ToTable("tickets");

                entity.Property(e => e.TicketId)
                    .HasDefaultValueSql("uuid_generate_v4()")
                    .HasColumnName("ticketid");
                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10,2)")
                    .HasColumnName("price");
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");
                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .IsRequired();
                entity.Property(e => e.EventId).HasColumnName("eventid");
            });

            modelBuilder.Entity<UserEvent>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.UserId }).HasName("userevents_pkey");

                entity.ToTable("userevents");

                entity.Property(e => e.EventId).HasColumnName("eventid");
                entity.Property(e => e.UserId).HasColumnName("userid");
                entity.Property(e => e.Feedback)
                    .HasMaxLength(255)
                    .HasColumnName("feedback")
                    .IsRequired();
            });

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.HasKey(e => e.ActivityId).HasName("activities_pkey");

                entity.ToTable("activities");
                entity.Property(e => e.ActivityId)
                    .HasDefaultValueSql("uuid_generate_v4()")
                    .HasColumnName("activityid"); // alterado para "activityid"
                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
                entity.Property(e => e.Datetime)
                    .HasColumnName("datetime")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("NOW()");
                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");
                entity.Property(e => e.EventId).HasColumnName("eventid");
            });
            
            modelBuilder.Entity<UserActivity>(entity =>
            {
                entity.HasKey(e => new { e.EventId, e.ActivityId }).HasName("useractivities_pkey");

                entity.ToTable("useractivities");
                
                entity.Property(e => e.EventId).HasColumnName("eventid");
                entity.Property(e => e.ActivityId).HasColumnName("activityid");

            });

            
            
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
