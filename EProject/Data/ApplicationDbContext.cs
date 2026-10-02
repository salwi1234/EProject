using EProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> users { get; set; }

        public DbSet<NGO> NGOs { get; set; }

        public DbSet<Donation> Donation { get; set; }

        public DbSet<Programme> Programme { get; set; }

        public DbSet<Query> Query { get; set; }

        // New Models

        public DbSet<ProgrammeParticipant> ProgrammeParticipants { get; set; }

        public DbSet<Partner> Partners { get; set; }

        public DbSet<Gallery> Galleries { get; set; }

        public DbSet<AboutPage> AboutPages { get; set; }

        public DbSet<DonationCause> DonationCauses { get; set; }
    }
}