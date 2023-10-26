using Microsoft.EntityFrameworkCore;
using short_Url.Models;

namespace short_Url.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Url> Urls { get; set; }
    }
}
