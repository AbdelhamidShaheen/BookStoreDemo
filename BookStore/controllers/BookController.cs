using BookStore.models;
using BookStore.models.repository;
using BookStore.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepository<Book> BookRepository;
        private readonly IBookStoreRepository<Author> AuthorRepository;
        private readonly IHostingEnvironment hosting;

        public BookController(IBookStoreRepository<Book> BookRepository, IBookStoreRepository<Author> AuthorRepository, IHostingEnvironment hosting)
        {
            this.BookRepository = BookRepository;
            this.AuthorRepository = AuthorRepository;
            this.hosting = hosting;

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
               var FileName = String.Empty;
                if(authorBook.file!= null)
                {
                    var UploadPath = Path.Combine(hosting.WebRootPath,"media");
                     FileName = authorBook.file.FileName;
                    var ImagePath = Path.Combine(UploadPath, FileName);
                    FileStream stream = new FileStream(ImagePath, FileMode.Create);
                    authorBook.file.CopyTo(stream);
                    stream.Close();

                }
                Book book = new Book {
                    Title = authorBook.Title,
                    Description = authorBook.Description,
                    Author = this.AuthorRepository.find(authorBook.AuthorId),
                    UrlImage = FileName

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
                AuthorId = book.Author.Id,
                ImageUrl = book.UrlImage

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
                var FileName = String.Empty;
                if (authorBook.file != null)
                {
                    var UploadPath = Path.Combine(hosting.WebRootPath, "media");
                    FileName = authorBook.file.FileName;
                    var ImagePath = Path.Combine(UploadPath, FileName);
                    
                    var OldImageP = authorBook.ImageUrl;
                    var FullOldImageP = Path.Combine(UploadPath, OldImageP);
                    System.IO.File.Delete(FullOldImageP);
                    FileStream stream = new FileStream(ImagePath, FileMode.Create);
                    authorBook.file.CopyTo(stream);
                    stream.Close();
                }

                var model = new Book {
                    Id = authorBook.BookId,
                    Title = authorBook.Title,
                    Description = authorBook.Description,
                    Author = this.AuthorRepository.find(authorBook.AuthorId),
                    UrlImage = FileName
                };
                this.BookRepository.update(authorBook.BookId, model);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                ViewData["error"] = e.Message;
                return View("Error");
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var UploadPath = Path.Combine(hosting.WebRootPath, "media");
            var ImagePath = this.BookRepository.find(id).UrlImage;
            var FullOldImageP = Path.Combine(UploadPath, ImagePath);
            System.IO.File.Delete(FullOldImageP);
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
