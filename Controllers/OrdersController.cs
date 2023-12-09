using Bookstore.Data;
using Bookstore.Extensions;
using Bookstore.Models;
using Bookstore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Controllers
{
	public class OrdersController : Controller
	{
		private readonly BookstoreDbContext _context;

		public OrdersController(BookstoreDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var bookstoreDbContext = _context.Orders.Include(o => o.Customer);

			return View(bookstoreDbContext.ToList());
		}

		public IActionResult Details(int? id)
		{
			var order = _context.Orders
				.Include(o => o.Customer)
				.FirstOrDefault(m => m.Id == id);

			return View(order);
		}

		public IActionResult Create()
		{
			List<OrderDetails> orderDetails = HttpContext.Session.GetObject<List<OrderDetails>>("ordersDetails");
			if (orderDetails == null || orderDetails.Count == 0)
			{
				return RedirectToAction("Index", "Books");
			}
			foreach (var orderDetail in orderDetails)
			{
				orderDetail.Book = _context.Books.Where(b => b.Id == orderDetail.BookId).FirstOrDefault();
				orderDetail.TotalPrice = orderDetail.Quantity * orderDetail.Book.Price;
			}

			ViewData["CustomerId"] = new SelectList(_context.Customers.Select(c => new { Id = c.Id, Name = $"{c.FirstName} {c.LastName}" }), "Id", "Name");

			return View(new OrderCreateData { OrderDetails = orderDetails });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Id,OrderDate,CustomerId")] Order order)
		{
			List<OrderDetails> orderDetails = HttpContext.Session.GetObject<List<OrderDetails>>("ordersDetails");
			if (ModelState.IsValid && orderDetails != null && orderDetails.Any())
			{
				_context.Add(order);
				_context.SaveChanges();

				foreach (var orderDetail in orderDetails)
				{
					orderDetail.OrderId = order.Id;
				}

				_context.OrderDetails.AddRange(orderDetails);
				_context.SaveChanges();

				return RedirectToAction(nameof(Index));
			}

			ViewData["CustomerId"] = new SelectList(_context.Customers.Select(c => new { Id = c.Id, Name = $"{c.FirstName} {c.LastName}" }), "Id", "Name");

			return View(order);
		}

		public IActionResult Edit(int? id)
		{
			var order = _context.Orders.Find(id);

			ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", order.CustomerId);

			return View(order);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind("Id,OrderDate,CustomerId")] Order order)
		{
			if (ModelState.IsValid)
			{
				_context.Update(order);
				_context.SaveChanges();

				return RedirectToAction(nameof(Index));
			}

			ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", order.CustomerId);

			return View(order);
		}

		public IActionResult Delete(int? id)
		{
			var order = _context.Orders
				.Include(o => o.Customer)
				.FirstOrDefault(m => m.Id == id);

			return View(order);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var order = _context.Orders.Find(id);
			if (order != null)
			{
				_context.Orders.Remove(order);
			}

			_context.SaveChanges();

			return RedirectToAction(nameof(Index));
		}
	}
}
