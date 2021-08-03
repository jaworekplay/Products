using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly Services.IProductsService _productsService;

        public ProductsController(Services.IProductsService productsService)
        {
            _productsService = productsService;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<IEnumerable<Models.IProduct>> Get()
        {
            return await _productsService.GetProducts();
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {            
            return "value";
        }

        // POST api/<ProductsController>
        [HttpPost]
        public void Post([FromBody] Models.Product value)
        {
            _productsService.AddProduct(value);
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            // this is for update
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
