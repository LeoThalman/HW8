    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using HW8.Models;

namespace HW8.DAL
{
    public partial class ArtistContext : DbContext
    {
        public ArtistContext() : base("name=ArtistDBContext")
        {
        }

        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<ArtWork> ArtWorks { get; set; }
        public virtual DbSet<Classification> Classifications { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>()
                .HasMany(e => e.ArtWorks)
                .WithRequired(e => e.Artist1)
                .HasForeignKey(e => e.Artist);

            modelBuilder.Entity<ArtWork>()
                .HasMany(e => e.Classifications)
                .WithRequired(e => e.ArtWork1)
                .HasForeignKey(e => e.ArtWork);

            modelBuilder.Entity<Genre>()
                .HasMany(e => e.Classifications)
                .WithRequired(e => e.Genre1)
                .HasForeignKey(e => e.Genre);
        }
    }
}
