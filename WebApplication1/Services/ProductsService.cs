using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IProductsService
    {
        Task<bool> AddProduct(IProduct product);
        Task<bool> DeleteProduct(int id);
        Task<IEnumerable<IProduct>> GetProducts();
    }

    public class ProductsService : IProductsService
    {
        private readonly ILogger<ProductsService> _logger;
        private readonly IDataStore<IProduct> _dataStore;

        public ProductsService(IDataStore<IProduct> dataStore, ILogger<ProductsService> logger)
        {
            _dataStore = dataStore;
            _logger = logger;
        }

        public async Task<IEnumerable<IProduct>> GetProducts()
        {
            _logger.LogTrace("Getting products");
            return await _dataStore.GetItemsAsync();
        }

        public async Task<bool> DeleteProduct(int id)
        {
            _logger.LogTrace($"Deleting product: {id}");
            return await _dataStore.DeleteItemAsync(id);
        }

        public async Task<bool> AddProduct(IProduct product)
        {
            _logger.LogTrace($"Adding product: {product.DisplayName}", product);
            return await _dataStore.AddItemAsync(product);
        }
    }
}
