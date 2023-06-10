using KidegaApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace KidegaApp.Infrastructure.Data
{
    public class KidegaDbContext : DbContext
    {
        public DbSet<Author> BookAuthors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<User> Users { get; set; }

        public KidegaDbContext(DbContextOptions<KidegaDbContext> options): base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCategory>().HasKey(bc => new { bc.BookId, bc.CategoryId });
            
            modelBuilder.Entity<BookCategory>().HasOne(bc => bc.Book)
                                               .WithMany(b => b.BookCategories)
                                               .HasForeignKey(bc => bc.BookId);
            modelBuilder.Entity<BookCategory>().HasOne(bc => bc.Category)
                                               .WithMany(c => c.BookCategories)
                                               .HasForeignKey(bc => bc.CategoryId);
            modelBuilder.Entity<Book>().HasOne(b => b.Author)
                                       .WithMany(a => a.Books)
                                       .HasForeignKey(b => b.AuthorId);
            modelBuilder.Entity<User>().HasIndex(u => u.Email)
                                       .IsUnique();
        }
    }
}