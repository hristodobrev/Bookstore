using Bookstore.Data;
using Bookstore.Models;
using Bookstore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookstoreDbContext _context;

        public BooksController(BookstoreDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var bookstoreDbContext = _context.Books.Include(b => b.Author);

            return View(new BookIndexData { Books = bookstoreDbContext.ToList(), OrderDetails = new OrderDetails() });
        }

        public IActionResult Details(int? id)
        {
            var book = _context.Books
                .Include(b => b.Author)
                .FirstOrDefault(m => m.Id == id);

            return View(book);
        }

        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "FullName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Genre,Price,ReleaseDate,AuthorId")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "FullName", book.AuthorId);

            return View(book);
        }

        public IActionResult Edit(int? id)
        {
            var book = _context.Books.Find(id);
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "FullName", book.AuthorId);

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Title,Genre,Price,ReleaseDate,AuthorId")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Update(book);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "FullName", book.AuthorId);

            return View(book);
        }

        public IActionResult Delete(int? id)
        {
            var book = _context.Books
                .Include(b => b.Author)
                .FirstOrDefault(m => m.Id == id);

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
