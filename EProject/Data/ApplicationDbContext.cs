using EProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet <User> users { get; set; }
        public  DbSet <NGO> NGOs { get; set; }

        public  DbSet <Donation> Donation { get; set; }

        public  DbSet <Programme> Programme { get; set; }

        public  DbSet <Query> Query { get; set; }
       
       
       


      
        
    }
}
