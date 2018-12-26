using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebshopApp.Models;

namespace WebshopApp.Data
{
    public class WebshopAppContext : IdentityDbContext
    {
        public WebshopAppContext(DbContextOptions<WebshopAppContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<WebShopUser> WebShopUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
