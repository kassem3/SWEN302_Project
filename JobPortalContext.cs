using Microsoft.EntityFrameworkCore;

namespace SimpleJobPortal.Models
{
    public class JobPortalContext : DbContext
    {
        public JobPortalContext(DbContextOptions<JobPortalContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Application> Applications { get; set; }
    }
}
