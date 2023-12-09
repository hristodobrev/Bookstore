using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class Order
    {
        public Order()
        {
            OrderDate = DateTime.Now;
        }

        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal OrderTotalPrice { get; set; }
        public bool IsComplete { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
