namespace Bookstore.Models.ViewModels
{
	public class OrderViewData
	{
		public Order Order { get; set; }
		public List<OrderDetails> OrderDetails { get; set; }
		public OrderDetails OrderDetail { get; set; }
	}
}
