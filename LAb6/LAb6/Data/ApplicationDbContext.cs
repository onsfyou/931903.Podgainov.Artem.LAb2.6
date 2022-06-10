using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LAb6.Models;

namespace LAb6.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<LAb6.Models.Category>? Categories { get; set; }
        public DbSet<LAb6.Models.Post>? Posts { get; set; }
        public DbSet<LAb6.Models.Reply>? Replies { get; set; }
        public DbSet<LAb6.Models.Topic>? Topics { get; set; }
        public DbSet<LAb6.Models.UseFile>? UseFiles { get; set; }
    }
}