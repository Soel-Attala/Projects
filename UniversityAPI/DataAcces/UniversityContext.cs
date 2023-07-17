using Microsoft.EntityFrameworkCore;
using UniversityAPI.Models.DataModels;

namespace UniversityAPI.DataAcces
{
    public class UniversityContext : DbContext
    {
        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options)
        {

        }
        //TODO: add DBSets (tables of our database)

        public DbSet<User>? Users { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Student> Students { get; set; }

    }
}
