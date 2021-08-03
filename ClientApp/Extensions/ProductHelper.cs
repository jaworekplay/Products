using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Extensions
{
    public static class ProductHelper
    {
        public static ClientApp.API.IProduct FromProductToIProduct(this API.Product product)
        {
            return new API.IProduct
            {
                DisplayName = product.DisplayName,
                Id = product.Id,
                Price = product.Price,
                Quantity = product.Quantity
            };
        }
    }
}
