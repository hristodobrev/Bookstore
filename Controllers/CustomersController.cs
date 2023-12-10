using Bookstore.Data;
using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Controllers
{
	public class CustomersController : Controller
	{
		private readonly BookstoreDbContext _context;

		public CustomersController(BookstoreDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View(_context.Customers.ToList());
		}

		public IActionResult Details(int? id)
		{
			var customer = _context.Customers.FirstOrDefault(m => m.Id == id);

			return View(customer);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Id,FirstName,LastName,Email,Phone")] Customer customer)
		{
			if (ModelState.IsValid)
			{
				_context.Add(customer);
				_context.SaveChanges();

				return RedirectToAction(nameof(Index));
			}

			return View(customer);
		}

		public IActionResult Edit(int? id)
		{
			var customer = _context.Customers.Find(id);

			return View(customer);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind("Id,FirstName,LastName,Email,Phone")] Customer customer)
		{
			if (ModelState.IsValid)
			{
				_context.Update(customer);
				_context.SaveChanges();

				return RedirectToAction(nameof(Index));
			}

			return View(customer);
		}

		public IActionResult Delete(int? id)
		{
			var customer = _context.Customers.FirstOrDefault(m => m.Id == id);

			return View(customer);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var customer = _context.Customers.Find(id);

			try
			{
				if (customer != null)
				{
					_context.Customers.Remove(customer);
				}

				_context.SaveChanges();
			}
			catch (DbUpdateException ex)
			{
				TempData["CustomerError"] = "Cannot delete this customer as they have existing orders.";

				return View(customer);
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
