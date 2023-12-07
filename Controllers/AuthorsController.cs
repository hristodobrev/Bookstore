using Bookstore.Data;
using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
	public class AuthorsController : Controller
	{
		private readonly BookstoreDbContext _context;

		public AuthorsController(BookstoreDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View(_context.Authors.ToList());
		}

		public IActionResult Details(int? id)
		{
			var author = _context.Authors.FirstOrDefault(m => m.Id == id);

			return View(author);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Id,FirstName,LastName,BirthDate,Country")] Author author)
		{
			if (ModelState.IsValid)
			{
				_context.Add(author);
				_context.SaveChanges();

				return RedirectToAction(nameof(Index));
			}

			return View(author);
		}

		public IActionResult Edit(int? id)
		{
			var author = _context.Authors.Find(id);

			return View(author);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind("Id,FirstName,LastName,BirthDate,Country")] Author author)
		{
			if (ModelState.IsValid)
			{
				_context.Update(author);
				_context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}

			return View(author);
		}

		public IActionResult Delete(int? id)
		{
			var author = _context.Authors.FirstOrDefault(m => m.Id == id);

			return View(author);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var author = _context.Authors.Find(id);
			if (author != null)
			{
				_context.Authors.Remove(author);
			}

			_context.SaveChanges();

			return RedirectToAction(nameof(Index));
		}
	}
}
