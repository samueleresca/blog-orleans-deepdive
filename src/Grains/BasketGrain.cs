using System;
using System.Collections.Generic;
using GrainInterfaces;
using Orleans;
using System.Threading.Tasks;
using GrainInterfaces.States;
using Orleans.Providers;

namespace Grains
{
    [StorageProvider(ProviderName="OrleansStorage")]
    public class BasketGrain : Grain<BasketState>, IBasketGrain
    {
   
        public async Task<Basket> GetCart()
        {
            await ReadStateAsync();
            
            if (State.Value != null) return State.Value;

            State.Value = new Basket
            {
                Id =  Guid.NewGuid(),
                Products = new List<Product>()
            };

            await WriteStateAsync();
            
            return State.Value;
        }
        
        
        public async Task<List<Product>> GetProducts()
        {
            await ReadStateAsync();
            return State.Value.Products;
        }

        public async Task AddProduct(Product product)
        {
            await ReadStateAsync();

            if (State.Value == null)
            {
                State = new BasketState();
            }
            
            State.Value.Products.Add(product);
            await WriteStateAsync();
        }
        
        
    }

}
