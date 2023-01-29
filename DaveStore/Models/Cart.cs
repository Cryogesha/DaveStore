using System.ComponentModel.DataAnnotations;

namespace DaveStore.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        [Required]
        public string UserId { get; set; }
        public List<Product> Products { get; set; }
    }
}
