namespace BlazorWASM.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public string ImageUrl { get; set; }

        public Product(int id, string name, decimal price, Category category, string imageUrl)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), $"'{nameof(name)}' can't be empty or null");
            }
            if (price < 0)
            {
                throw new ArgumentNullException(nameof(price));
            }
            Id = id;
            Name = name;
            Price = price;
            Category = category;
            ImageUrl = imageUrl;
        }
    }
}

