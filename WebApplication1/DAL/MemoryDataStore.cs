using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class MemoryDataStore : IDataStore<IProduct>
    {
        public MemoryDataStore()
        {
            Products = new ConcurrentStack<IProduct>(new[] { 
                new Product { Id = 1, DisplayName = "Display Name 1", Price = 10, Quantity = 1 },
                new Product { Id = 2, DisplayName = "Display Name 2", Price = 15, Quantity = 2 },
                new Product { Id = 3, DisplayName = "Display Name 3", Price = 20, Quantity = 3 },
                new Product { Id = 4, DisplayName = "Display Name 4", Price = 25, Quantity = 4 },
                new Product { Id = 5, DisplayName = "Display Name 5", Price = 30, Quantity = 5 },
                new Product { Id = 6, DisplayName = "Display Name 6", Price = 35, Quantity = 6 },
                new Product { Id = 7, DisplayName = "Display Name 7", Price = 40, Quantity = 7 } });
        }

        public ConcurrentStack<IProduct> Products { get; set; }

        public async Task<bool> AddItemAsync(IProduct item)
        {
            return await Task.Factory.StartNew( () => 
            {
                Products.Push(item);
                return true;
            });
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            return await Task.Factory.StartNew( () =>
            {
                var found = Products.SingleOrDefault(p => p.Id == id);
                if (found is object)
                {
                    return Products.TryPop(out found);
                }
                return false;
            });
        }

        public Task<IProduct> GetItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IProduct>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.Factory.StartNew(() => Products);
        }

        public Task<bool> UpdateItemAsync(IProduct item)
        {
            throw new NotImplementedException();
        }
    }
}
