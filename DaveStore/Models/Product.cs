using System.ComponentModel.DataAnnotations;

namespace DaveStore.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; } 
        public string Description { get; set; }
        public string ImageLink { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal Discount { get; set; }
        public int UnitsInStock { get; set; }

        public Category Category { get; set; } 

        [Required]
        public int CategoryId { get; set; }

        public List<ProductSpec> ProductSpecs { get; set; }
    }
}
