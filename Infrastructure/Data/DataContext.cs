using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    /*
     The DB context created by extending the DbContext class and providing DbContextOptions to the base constructor of the extended class. 
     Provides an object that contains a property Posts of type DbSet<Post>. This object allows for interaction with the database via 
     dependency injection, and utilization of the Posts property, which corresponds to the db table Posts
     */
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
    }
}