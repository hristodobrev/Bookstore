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

			var orderDetails = _context.OrderDetails
				.Include(od => od.Book)
				.Where(od => od.OrderId == order.Id)
				.ToList();

			return View(new OrderViewData { Order = order, OrderDetails = orderDetails });
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

			ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FullName");

			return View(new OrderViewData { OrderDetails = orderDetails });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Id,OrderDate,CustomerId")] Order order)
		{
			List<OrderDetails> orderDetails = HttpContext.Session.GetObject<List<OrderDetails>>("ordersDetails");
			if (orderDetails == null || !orderDetails.Any())
			{
				return RedirectToAction("Index", "Books");
			}

			if (ModelState.IsValid)
			{
				_context.Add(order);
				_context.SaveChanges();

				foreach (var orderDetail in orderDetails)
				{
					orderDetail.OrderId = order.Id;
					orderDetail.Book = _context.Books.Where(b => b.Id == orderDetail.BookId).FirstOrDefault();
					orderDetail.TotalPrice = orderDetail.Quantity * orderDetail.Book.Price;
				}

				order.OrderTotalPrice = orderDetails.Select(od => od.TotalPrice).Sum();
				_context.Orders.Update(order);
				_context.SaveChanges();

				_context.OrderDetails.AddRange(orderDetails);
				_context.SaveChanges();

				HttpContext.Session.ResetObject("ordersDetails");

				return RedirectToAction(nameof(Index));
			}

			ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FullName");

			return View(new OrderViewData { OrderDetails = orderDetails });
		}

		public IActionResult Edit(int? id)
		{
			var order = _context.Orders.Find(id);
			if (order.IsComplete)
			{
				return View(nameof(Index));
			}

			var orderDetails = _context.OrderDetails
				.Include(od => od.Book)
				.Where(od => od.OrderId == order.Id)
				.ToList();

			ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FullName", order.CustomerId);

			return View(new OrderViewData { Order = order, OrderDetails = orderDetails });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind("Id,OrderDate,CustomerId,IsComplete,OrderTotalPrice")] Order order)
		{
			if (ModelState.IsValid)
			{
				_context.Update(order);
				_context.SaveChanges();

				return RedirectToAction(nameof(Index));
			}

			var orderDetails = _context.OrderDetails
				.Include(od => od.Book)
				.Where(od => od.OrderId == order.Id)
				.ToList();

			ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FullName", order.CustomerId);

			return View(new OrderViewData { Order = order, OrderDetails = orderDetails });
		}

		public IActionResult Delete(int? id)
		{
			var order = _context.Orders
				.Include(o => o.Customer)
				.FirstOrDefault(m => m.Id == id);
			if (order.IsComplete)
			{
				return View(nameof(Index));
			}

			var orderDetails = _context.OrderDetails
				.Include(od => od.Book)
				.Where(od => od.OrderId == order.Id)
				.ToList();

			return View(new OrderViewData { Order = order, OrderDetails = orderDetails });
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var order = _context.Orders.Find(id);
			if (order.IsComplete)
			{
				return View(nameof(Index));
			}


			if (order != null)
			{
				_context.OrderDetails.RemoveRange(_context.OrderDetails.Where(od => od.OrderId == id));
				_context.Orders.Remove(order);
			}

			_context.SaveChanges();

			return RedirectToAction(nameof(Index));
		}
	}
}
