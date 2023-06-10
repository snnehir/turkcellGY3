using KidegaApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace KidegaApp.Infrastructure.Data
{
    public class KidegaDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        public KidegaDbContext(DbContextOptions<KidegaDbContext> options): base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Book>().HasOne(b => b.Category)
                                       .WithMany(c => c.Books)
                                       .HasForeignKey(bc => bc.CategoryId);
            modelBuilder.Entity<Book>().HasOne(b => b.Author)
                                       .WithMany(a => a.Books)
                                       .HasForeignKey(b => b.AuthorId);
            modelBuilder.Entity<User>().HasIndex(u => u.Email)
                                       .IsUnique();
        }
    }
}