namespace Bookstore.Models
{
	public class OrderDetails
	{
		public int Id { get; set; }
		public int Quantity { get; set; }
		public decimal TotalPrice { get; set; }

		public int BookId { get; set; }
		public Book? Book { get; set; }

		public int? OrderId { get; set; }
		public Order? Order { get; set; }
	}
}
