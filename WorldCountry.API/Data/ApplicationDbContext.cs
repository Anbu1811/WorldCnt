using Microsoft.EntityFrameworkCore;
using WorldCountry.API.Model;

namespace WorldCountry.API.Data
{
    public class ApplicationDbContext : DbContext 
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }


        public DbSet<Country> AllCountries { get; set; }

        public DbSet<States> AllStates { get; set; }
    }
}
