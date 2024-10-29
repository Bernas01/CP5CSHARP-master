
using MongoDB.Driver;
using ProductAPI.Models;
using ProductAPI.Interfaces;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace ProductAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IOptions<DatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _products = database.GetCollection<Product>("Products");
        }

        // Async method to add a new product
        public async Task CreateProductAsync(Product product)
        {
            await _products.InsertOneAsync(product);
        }

        // Async method to get a product by ID
        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _products.Find<Product>(p => p.Id == id).FirstOrDefaultAsync();
        }

        // Async method to update a product
        public async Task UpdateProductAsync(string id, Product updatedProduct)
        {
            await _products.ReplaceOneAsync(p => p.Id == id, updatedProduct);
        }

        // Async method to delete a product
        public async Task DeleteProductAsync(string id)
        {
            await _products.DeleteOneAsync(p => p.Id == id);
        }
    }
}
