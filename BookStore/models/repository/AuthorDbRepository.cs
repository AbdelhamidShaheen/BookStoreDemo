using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.models.repository
{

    public class AuthorDbRepository : IBookStoreRepository<Author>
    {
        BookStoreDBcontext dp;
        public AuthorDbRepository(BookStoreDBcontext dp)
        {

            this.dp = dp;
           
        }
        public void add(Author t)
        {
            dp.author.Add(t);
            save();
        }

        public void delete(int id)
        {
            var author = find(id);

            dp.author.Remove(author);
            save();
        }

        public Author find(int id)
        {
           return dp.author.SingleOrDefault(b => b.Id == id);

        }

        public IList<Author> list()
        {
            return dp.author.ToList();
            
        }

        public void update(int id, Author t)
        {
            dp.author.Update(t);
            save();
        }

        private void save()
        {
            dp.SaveChanges();
        }
    }
}
