using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
