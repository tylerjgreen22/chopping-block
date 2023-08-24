using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    /*
     The DB context created by extending the DbContext class and providing DbContextOptions to the base constructor of the extended class. 
     Provides an object that contains properties for interacting with the database via DbSets. Can be injected into classes to provide access to the database
     */
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Like>()
                .HasIndex(l => new { l.PostId, l.UserId })
                .IsUnique();
        }
    }
}