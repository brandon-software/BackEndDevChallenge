using BackEndDevChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEndDevChallenge
{
    public class CalculatorContext : DbContext
    {
        public DbSet<MathProblem> MathProblems { get; set; }

        public CalculatorContext(DbContextOptions<CalculatorContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure the options for the context as desired or remove
        }
    }
}
