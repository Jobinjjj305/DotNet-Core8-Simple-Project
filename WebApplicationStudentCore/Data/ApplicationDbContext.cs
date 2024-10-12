using Microsoft.EntityFrameworkCore;
using WebApplicationStudentCore.Models.Entities;

namespace WebApplicationStudentCore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options) 
        {

        }

        public DbSet<Student> Students { get; set; }
    }
}
