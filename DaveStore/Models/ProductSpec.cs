using System.ComponentModel.DataAnnotations;

namespace DaveStore.Models
{
    public class ProductSpec
    {
        public int ProductSpecId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }

        public Product Product { get; set; }

        [Required]
        public int ProductId { get; set; } 
    }
}
