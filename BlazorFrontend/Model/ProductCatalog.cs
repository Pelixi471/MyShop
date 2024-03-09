using Models;
using System.Threading;

namespace BlazorWASM.Model
{
    public class ProductCatalog : IProductCatalog
    {
        private readonly List<Product> _products = new()
        {
            new Product(1, "book 1", 500m, new Category(1, "books"), "image/1.jpg"),
            new Product(2, "book 2", 1000m, new Category(1, "books"), "image/2.jpg"),
            new Product(3, "book 3", 100m, new Category(1, "books"), "image/3.jpg")
            };

        private readonly List<Category> _categories = new()
        {
            new Category(1, "Книги"),
            new Category(2, "Тетради")
            };

        public IReadOnlyList<Product> GetProducts()
        {
            return _products.AsReadOnly();
        }

        public IReadOnlyList<Category> GetCategories()
        {
            return _categories.AsReadOnly();
        }

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        public async Task<Product> GetProductAsync(int productId)
        {
            await _semaphore.WaitAsync();
            try
            {
                Product product = _products.FirstOrDefault(p => p.Id == productId);

                if (product == null)
                {
                    throw new Exception("Продукт не найден");
                }

                return product;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task AddProduct(string name, decimal price, Category category, string imageUrl)
        {
            await _semaphore.WaitAsync();
            try
            {
                var lastId = _products.Max(p => p.Id);
                var product = new Product(lastId + 1, name, price, category, imageUrl);
                _products.Add(product);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }

}

