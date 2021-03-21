using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.models.repository
{

    public class AuthorRepository : IBookStoreRepository<Author>
    {
        List<Author> authors;
        public AuthorRepository()
        {
            authors = new List<Author>() {
            new Author
            {
                Id=1,
                FullName="shaheen ahmed"
            
            }
            ,
             new Author
            {
                Id=2,
                FullName="shaheen mousa"

            },
              new Author
            {
                Id=3,
                FullName="shaheen shaheen"

            }
            };
        }
        public void add(Author t)
        {
            authors.Add(t);
        }

        public void delete(int id)
        {
            var author = find(id);

            authors.Remove(author);
        }

        public Author find(int id)
        {
           return authors.SingleOrDefault(b => b.Id == id);
        }

        public IList<Author> list()
        {
            return authors;
        }

        public void update(int id, Author t)
        {
            var author = find(id);
            var index=this.authors.IndexOf(author);
            authors[index] = t;
        }
    }
}
