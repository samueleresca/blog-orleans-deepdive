using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrainInterfaces;
using GrainInterfaces.States;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace Cart.API.Controllers
{
    [Route("api/cart")]
    public class CartController : Controller
    {
        private readonly IClusterClient _client;
        
        public CartController(IClusterClient client)
        {
            _client = client;
        }
      
        [HttpGet("{id}")]
        public async Task<GrainInterfaces.States.Cart> Get(Guid id)
        {
            var grain = _client.GetGrain<ICartGrain>(id);
            return await grain.GetCart();
        }
        
        [HttpGet("{id}/product")]
        public async Task<List<Product>> GetProduct(Guid id)
        {
            var grain = _client.GetGrain<ICartGrain>(id);
           return await grain.GetProducts();
        }

        [HttpPost("{id}/product")]
        public async Task AddProduct(Guid id, [FromBody]Product product)
        {
            var grain = _client.GetGrain<ICartGrain>(id);
            await grain.AddProduct(product);
        }
     
    }
}
