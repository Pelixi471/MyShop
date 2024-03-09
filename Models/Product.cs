using System.ComponentModel.DataAnnotations;

namespace HttpModels
{
    public class Product
    {
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

    }
}
