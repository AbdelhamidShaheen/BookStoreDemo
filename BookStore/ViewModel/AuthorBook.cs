using System;
using System.Collections.Generic;
using BookStore.models;

using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModel
{
    public class AuthorBook
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int AuthorId { get; set; }

        public List<Author> authors { get; set; }
    }
}
