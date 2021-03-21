using BookStore.models;
using BookStore.models.repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BookStore.controllers
{
    public class AuthorController : Controller
    {
        private readonly IBookStoreRepository<Author> authorRepository;
        public AuthorController(IBookStoreRepository<Author> authorRepository)
        {
            this.authorRepository = authorRepository;
        }
        // GET: AuthorController
        public ActionResult Index()
        {
            var authors = this.authorRepository.list();
            return View(authors);
        }

     

        // GET: AuthorController/Details/5
        public ActionResult Details(int id)
        {
            var author = this.authorRepository.find(id);
            return View(author);
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            try
            {
                this.authorRepository.add(author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Edit/5
        public ActionResult Edit(int id)
        {
            var author = this.authorRepository.find(id);
            return View(author);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author author)
        {
            try
            {
                this.authorRepository.update(id,author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        public ActionResult Delete(int id)
        {
            this.authorRepository.delete(id);
            return RedirectToAction(nameof(Index));
        }

      
    }
}
