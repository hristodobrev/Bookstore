namespace Bookstore.Models.ViewModels
{
	public class OrderCreateData
	{
		public Order Order { get; set; }
		public List<OrderDetails> OrderDetails { get; set; }
	}
}
