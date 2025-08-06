using Microsoft.EntityFrameworkCore;

namespace WebAPI_Project.Models
{
    public class PetDbContext:DbContext
    {
        public PetDbContext(DbContextOptions<PetDbContext> options) : base(options) { Database.EnsureCreated(); }
        public DbSet<Pet> Pets { get; set; } 
    }
}
