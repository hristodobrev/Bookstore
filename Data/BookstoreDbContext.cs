using Bookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Data
{
	public class BookstoreDbContext : DbContext
	{
		public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options) : base(options) { }

		public DbSet<Author> Authors { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetails> OrderDetails { get; set; }
	}
}
