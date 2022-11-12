using Microsoft.EntityFrameworkCore;

namespace UserManagemetAPI.Model
{
    public class EmployeeContext:DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options):base(options)
        {

        }

        public DbSet<TBLEmployee>   TBLEmployees { get; set; }

        public DbSet<TBLDesignation> TBLDesignations { get; set; }

    }
}
