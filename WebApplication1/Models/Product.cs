using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public interface IProduct
    {
        string DisplayName { get; set; }
        int Id { get; set; }
        decimal Price { get; set; }
        int Quantity { get; set; }
    }

    public class Product : IProduct
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
