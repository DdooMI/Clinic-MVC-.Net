using Clinic1.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic1.Data
{
    public class ClinicDbContext:DbContext
    {
        public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options) { }
        public DbSet<SignUp> signUps { get; set; }
        public DbSet<LogIn> logins { get; set; }
        public DbSet<Reservation> reservations { get; set; }
    }
}
