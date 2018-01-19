using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UrbanSpork.DataAccess.Data
{
    public class USDbContext : DbContext
    {
        public USDbContext(DbContextOptions<USDbContext> options) : base(options)
        {

        }


        public DbSet<UserRM> Users { get; set; }
       


        //Fluent API
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Book>().Property(b => b.Isbn).HasMaxLength(10);
        //    modelBuilder.Entity<Book>().HasAlternateKey(b => b.Isbn).HasName("UniqueIsbn");
        //    modelBuilder.Entity<Book>().Ignore(b => b.FullTitle);
        //    modelBuilder.Entity<Book>().Property(b => b.CreatedAt).HasDefaultValueSql("getdate()");

        //    // modelBuilder.Entity<Author>().HasKey(a => new { a.FirstName, a.LastName });

        //    modelBuilder.Entity<Client>().HasOne(c => c.Library).WithOne(l => l.Client).HasForeignKey<PersonalLibrary>();
        //    modelBuilder.Entity<PersonalLibraryBook>().HasKey(pl => new { pl.BookId, pl.LibraryId });

        //    //book - personalLibrary many-to-many relationship
        //    modelBuilder.Entity<PersonalLibraryBook>().HasOne(pl => pl.Book).WithMany(b => b.PersonalLibraryBooks).HasForeignKey(pl => pl.BookId);
        //    modelBuilder.Entity<PersonalLibraryBook>().HasOne(pl => pl.PersonalLibrary).WithMany(l => l.PersonalLibraryBooks).HasForeignKey(pl => pl.LibraryId);


        //}
    }
}
