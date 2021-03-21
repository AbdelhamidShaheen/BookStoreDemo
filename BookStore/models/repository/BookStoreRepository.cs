using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.models.repository
{
    public class BookStoreRepository : IBookStoreRepository<Book>
    {
        List<Book> books;
        public BookStoreRepository()
        {
            books = new List<Book>() {
            new Book{Id=1,Title="c# Tutriol",Description="no description"},
            new Book{Id=2,Title="java Tutriol",Description="no description"},
            new Book{Id=3,Title="php Tutriol",Description="no description"},
            };

        }
        public void add(Book t)
        {
            books.Add(t);
        }

        public void delete(int id)
        {
           var book= find(id);

            books.Remove(book);
        }

        public Book find(int id)
        {
            var book=books.SingleOrDefault(b=>b.Id==id);
            return book;
        }

        public IList<Book> list()
        {
            return books;
        }

        public void update(int id,Book t)
        {
            var book = find(id);
            var index = this.books.IndexOf(book);
            books[index] = t;

           
        }
    }
}
