using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
	public class Book
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Genre { get; set; }
		[DisplayFormat(DataFormatString = "{0:c}")]
		public decimal Price { get; set; }
		[DataType(DataType.Date)]
		public DateTime ReleaseDate { get; set; }

		public int AuthorId { get; set; }
		public Author? Author { get; set; }
	}
}
