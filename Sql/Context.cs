using Entities;
using Microsoft.EntityFrameworkCore;

namespace Sql
{
    public class Context : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Post { get; set; }

        public Context()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=persondb;Integrated Security=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasOne<User>(e => e.Author)
                .WithMany()
                .HasForeignKey(h => h.UserId);

            modelBuilder.Entity<Blog>()
                .HasMany<Post>(e => e.Posts)
                .WithOne()
                .HasForeignKey(e => e.BlogId);
        }






    }
}
