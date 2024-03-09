namespace MyShop.Domain.Entites
{
    public class Product: IEntity
    {
        private string _name;
        public decimal _price;
        public string? _imageUrl;
        public int Id { get; init; }
        public string Name
        {
            get => _name;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("name can't be empty", nameof(value));
                if (value.Length is < 3 or > 100)
                    throw new ArgumentException("the name should be between 3 and 100 characters", nameof(value));

                _name = value;
            }
        }
        public decimal Price 
        {
            get => _price;
            set
            {
                if (value is > 0)
                    throw new ArgumentException("the price must be greater than 0", nameof(value));
                _price = value;
            }
            
        }
        public string ImageUrl 
        {
            get => _imageUrl;
            set
            {
                _imageUrl = value;
            }
        }

    }
}
