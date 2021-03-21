
using Microsoft.EntityFrameworkCore;


namespace BookStore.models
{
    public class BookStoreDBcontext:DbContext
    {

        public BookStoreDBcontext(DbContextOptions<BookStoreDBcontext> dbContextOptions):base(dbContextOptions)
        {

        }
        public DbSet<Book> book { get; set; }
        public DbSet<Author> author { get; set; }

    }
}
