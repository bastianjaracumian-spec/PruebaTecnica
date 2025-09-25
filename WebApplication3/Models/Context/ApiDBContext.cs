using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ApiAtencionesMédicas.Models.Context
{
    public class ApiDBContext : DbContext
    {
        public DbSet<Patient> patients { get; set; } = null;
        public ApiDBContext(DbContextOptions<ApiDBContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        

            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder);
                optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("ConecctionStrings:AtencionesMedicas"));
            }
           
        }
    }
}
