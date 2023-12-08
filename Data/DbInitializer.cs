using Bookstore.Models;

namespace Bookstore.Data
{
	public static class DbInitializer
	{
		public static void Initialize(BookstoreDbContext context)
		{
			context.Database.EnsureCreated();

			// Skip if db is already created
			if (context.Authors.Any())
			{
				return;
			}

			List<Author> authors = new List<Author>
			{
				new Author { BirthDate = new DateTime(1980, 5, 15), Country = "USA", FirstName = "John", LastName = "Doe" },
				new Author { BirthDate = new DateTime(1975, 8, 22), Country = "Canada", FirstName = "Jane", LastName = "Smith" },
				new Author { BirthDate = new DateTime(1990, 3, 10), Country = "UK", FirstName = "Robert", LastName = "Johnson" },
				new Author { BirthDate = new DateTime(1985, 12, 5), Country = "Australia", FirstName = "Alice", LastName = "Williams" },
				new Author { BirthDate = new DateTime(1982, 7, 18), Country = "USA", FirstName = "Emily", LastName = "Davis" },
				new Author { BirthDate = new DateTime(1978, 9, 25), Country = "Canada", FirstName = "Daniel", LastName = "Brown" },
				new Author { BirthDate = new DateTime(1989, 1, 30), Country = "Germany", FirstName = "Sophia", LastName = "Miller" },
				new Author { BirthDate = new DateTime(1983, 11, 12), Country = "Australia", FirstName = "Jackson", LastName = "Taylor" },
				new Author { BirthDate = new DateTime(1970, 6, 8), Country = "France", FirstName = "Olivia", LastName = "Johnson" }
			};
			context.Authors.AddRange(authors);
			context.SaveChanges();

			List<Book> books = new List<Book>
			{
				new Book { Title = "The Great Adventure", Genre = "Adventure", Price = 19.99m, ReleaseDate = new DateTime(2022, 2, 15), AuthorId = 1 },
				new Book { Title = "Mystery at Midnight", Genre = "Mystery", Price = 24.99m, ReleaseDate = new DateTime(2021, 10, 8), AuthorId = 2 },
				new Book { Title = "Coding Chronicles", Genre = "Technology", Price = 29.99m, ReleaseDate = new DateTime(2023, 1, 20), AuthorId = 5 },
				new Book { Title = "A Journey Home", Genre = "Fiction", Price = 17.99m, ReleaseDate = new DateTime(2022, 5, 30), AuthorId = 1 },
				new Book { Title = "The Art of Cooking", Genre = "Cookbook", Price = 39.99m, ReleaseDate = new DateTime(2020, 12, 12), AuthorId = 4 },
				new Book { Title = "Beyond the Stars", Genre = "Science Fiction", Price = 22.99m, ReleaseDate = new DateTime(2021, 8, 3), AuthorId = 3 },
				new Book { Title = "The History Buff", Genre = "History", Price = 28.99m, ReleaseDate = new DateTime(2022, 11, 18), AuthorId = 1 },
			};
			context.Books.AddRange(books);
			context.SaveChanges();

			List<Customer> customers = new List<Customer>
			{
				new Customer { FirstName = "Michael", LastName = "Smith", Email = "michael.smith@email.com", Phone = "+1 (123) 456-7890" },
				new Customer { FirstName = "Jennifer", LastName = "Johnson", Email = "jennifer.johnson@email.com", Phone = "+1 (234) 567-8901" },
				new Customer { FirstName = "David", LastName = "Williams", Email = "david.williams@email.com", Phone = "+1 (345) 678-9012" },
				new Customer { FirstName = "Emma", LastName = "Jones", Email = "emma.jones@email.com", Phone = "+1 (456) 789-0123" },
				new Customer { FirstName = "Christopher", LastName = "Miller", Email = "christopher.miller@email.com", Phone = "+1 (567) 890-1234" },
				new Customer { FirstName = "Olivia", LastName = "Brown", Email = "olivia.brown@email.com", Phone = "+1 (678) 901-2345" },
				new Customer { FirstName = "Daniel", LastName = "Taylor", Email = "daniel.taylor@email.com", Phone = "+1 (789) 012-3456" }
			};
			context.Customers.AddRange(customers);
			context.SaveChanges();
		}
	}
}
