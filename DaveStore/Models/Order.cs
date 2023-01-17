using System.ComponentModel.DataAnnotations;
using DaveStore.Models;

namespace DaveStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public List<Product> Products { get; set; }
    }
}
