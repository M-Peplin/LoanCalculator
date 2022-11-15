using LoanCalcAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanCalcAPI.Data
{
    public class LoanContext : DbContext
    {
        public LoanContext(DbContextOptions options) : base(options) { }        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Loan");
        }

        public DbSet<Loan>  Loan { get; set; }
    }
}
