using BookStore.models;
using BookStore.models.repository;
using BookStore.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepository<Book> BookRepository;
        private readonly IBookStoreRepository<Author> AuthorRepository;

        public BookController(IBookStoreRepository<Book> BookRepository, IBookStoreRepository<Author> AuthorRepository)
        {
            this.BookRepository = BookRepository;
            this.AuthorRepository = AuthorRepository;

        }
        // GET: BookController
        public ActionResult Index()
        {
            var Books=this.BookRepository.list();
            return View(Books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var Book = this.BookRepository.find(id);
            return View(Book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new AuthorBook {
                authors = this.AuthorRepository.list().ToList() 
            };
            

            
            
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuthorBook authorBook)
        {
            
            try

            {
                Book book = new Book {
                    Title = authorBook.Title,
                    Description = authorBook.Description,
                    Author = this.AuthorRepository.find(authorBook.AuthorId)
                
                
                };

                this.BookRepository.add(book);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = this.BookRepository.find(id);
            var model = new AuthorBook
            {
                BookId = id,
                Title = book.Title,
                Description = book.Description,
                authors = this.AuthorRepository.list().ToList(),
                AuthorId = 0
                

            };




            return View(model);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AuthorBook authorBook)
        {
            try
            {
                var model = new Book {
                    Id=authorBook.BookId,
                    Title = authorBook.Title,
                    Description = authorBook.Description,
                    Author = this.AuthorRepository.find(authorBook.AuthorId)
                };
                this.BookRepository.update(authorBook.BookId, model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            this.BookRepository.delete(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
