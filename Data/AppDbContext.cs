using Microsoft.EntityFrameworkCore;

namespace HawkManagement.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<LoginLog> LoginLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasKey(e => e.EmployeeID);
            modelBuilder.Entity<Employer>().HasKey(e => e.EmployerID);
            modelBuilder.Entity<Admin>().HasKey(e => e.AdminID);
            modelBuilder.Entity<LoginLog>().HasKey(l => l.LogID);

            // Optional: Configure additional relationships and constraints
        }
    }

    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
    }

    public class Employer
    {
        public int EmployerID { get; set; }
        public string Name { get; set; }
    }

    public class Admin
    {
        public int AdminID { get; set; }
        public string Name { get; set; }
    }

    public class LoginLog
    {
        public int LogID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime LogoutDate { get; set; }
        public Employee Employee { get; set; }
    }
}
