using Bookstore.Data;
using Bookstore.Extensions;
using Bookstore.Models;
using Bookstore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookstore.Controllers
{
	public class OrderDetailsController : Controller
	{
		private readonly BookstoreDbContext _context;

		public OrderDetailsController(BookstoreDbContext context)
		{
			_context = context;
		}

		public IActionResult Create()
		{
			ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(BookIndexData data)
		{
			List<OrderDetails> orderDetails = HttpContext.Session.GetObject<List<OrderDetails>>("ordersDetails");
			if (orderDetails == null)
			{
				orderDetails = new List<OrderDetails>();
			}

			var orderDetail = orderDetails.Find(od => od.BookId == data.OrderDetails.BookId);
			if (orderDetail != null)
			{
				orderDetail.Quantity += data.OrderDetails.Quantity;
			}
			else
			{
				orderDetails.Add(data.OrderDetails);
			}

			HttpContext.Session.SetObject("ordersDetails", orderDetails);

			return RedirectToAction("Index", "Books");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(OrderViewData data)
		{
			List<OrderDetails> orderDetails = HttpContext.Session.GetObject<List<OrderDetails>>("ordersDetails");
			if (orderDetails == null)
			{
				orderDetails = new List<OrderDetails>();
			}

			orderDetails.RemoveAt(orderDetails.FindIndex(o => o.BookId == data.OrderDetail.BookId));

			HttpContext.Session.SetObject("ordersDetails", orderDetails);

			if (orderDetails.Count > 0)
			{
				return RedirectToAction("Create", "Orders");
			}

			return RedirectToAction("Index", "Books");
		}
	}
}
