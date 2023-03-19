using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MangaWorld_admin.Models
{
    public partial class DBcontext : DbContext
    {
        public DBcontext()
            : base("name=DBcontext")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<ChapterType> ChapterTypes { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Manga> Mangas { get; set; }
        public virtual DbSet<ScanTeam> ScanTeams { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Chapter> Chapters { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(e => e.Mangas)
                .WithRequired(e => e.Author)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChapterType>()
                .HasMany(e => e.Chapters)
                .WithRequired(e => e.ChapterType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Language>()
                .HasMany(e => e.Mangas)
                .WithRequired(e => e.Language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Language>()
                .HasMany(e => e.ScanTeams)
                .WithRequired(e => e.Language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manga>()
                .HasMany(e => e.Chapters)
                .WithRequired(e => e.Manga)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manga>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Manga)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manga>()
                .HasMany(e => e.Ratings)
                .WithRequired(e => e.Manga)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manga>()
                .HasMany(e => e.Reports)
                .WithRequired(e => e.Manga)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ScanTeam>()
                .HasMany(e => e.Chapters)
                .WithRequired(e => e.ScanTeam)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Mangas)
                .WithRequired(e => e.Status1)
                .HasForeignKey(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Ratings)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Reports)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
