using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrainInterfaces;
using GrainInterfaces.States;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace API.Controllers
{
    [Route("api/basket")]
    public class BasketController : Controller
    {
        private readonly IClusterClient _client;
        
        public BasketController(IClusterClient client)
        {
            _client = client;
        }
      
        [HttpGet("{id}")]
        public async Task<Basket> Get(Guid id)
        {
            var grain = _client.GetGrain<IBasketGrain>(id);
            return await grain.GetCart();
        }
        
        [HttpGet("{id}/product")]
        public async Task<List<Product>> GetProduct(Guid id)
        {
            var grain = _client.GetGrain<IBasketGrain>(id);
           return await grain.GetProducts();
        }

        [HttpPost("{id}/product")]
        public async Task AddProduct(Guid id, [FromBody]Product product)
        {
            var grain = _client.GetGrain<IBasketGrain>(id);
            await grain.AddProduct(product);
        }
     
    }
}
