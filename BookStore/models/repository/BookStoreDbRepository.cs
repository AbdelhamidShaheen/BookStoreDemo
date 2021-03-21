using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.models.repository
{
    public class BookStoreDbRepository : IBookStoreRepository<Book>
    {
        BookStoreDBcontext dp;
        public BookStoreDbRepository(BookStoreDBcontext dp)
        {
            this.dp = dp;

        }
        public void add(Book t)
        {
            
            dp.book.Add(t);
            save();

        }

        public void delete(int id)
        {
           var book= find(id);

            dp.book.Remove(book);
            save();

        }

        public Book find(int id)
        {
            var book=dp.book.Include(a => a.Author).SingleOrDefault(b=>b.Id==id);
            return book;
        }

        public IList<Book> list()
        {
            return dp.book.Include(a=>a.Author).ToList();
        }

        public void update(int id,Book t)
        {
            dp.book.Update(t);
            save();


        }
        private void save()
        {
            dp.SaveChanges();
        }
    }
}
