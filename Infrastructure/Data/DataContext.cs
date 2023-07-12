using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    /*
     The DB context created by extending the DbContext class and providing DbContextOptions to the base constructor of the extended class. 
     Provides an object that contains properties for interacting with the database via DbSets. Can be injected into classes to provide access to the database
     */
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<RecipePost> RecipePosts { get; set; }
        public DbSet<RecipeCategory> RecipeCategories { get; set; }
        public DbSet<RecipeStep> RecipeSteps { get; set; }
    }
}