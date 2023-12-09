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

			orderDetails.Add(data.OrderDetails);

			HttpContext.Session.SetObject("ordersDetails", orderDetails);

			return RedirectToAction("Index", "Books");
		}
	}
}
