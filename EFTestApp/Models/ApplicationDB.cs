using Microsoft.EntityFrameworkCore;

namespace EFTestApp.Models
{
    public class ApplicationDB : DbContext
    {
        public ApplicationDB(DbContextOptions<ApplicationDB> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
                    new Person { Id = 1, Name = "Tom", Age = 37 },
                    new Person { Id = 2, Name = "Bob", Age = 41 },
                    new Person { Id = 3, Name = "Sam", Age = 24 }
            );
        }



    }
}
