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
   
        public Task<Basket> GetCart()
        {
            return Task.FromResult(State.Value);
        }
        
        
        public Task<List<Product>> GetProducts()
        {
            return Task.FromResult(State.Value.Products);
        }

        public async Task AddProduct(Product product)
        {
        

            if (State.Value.Products == null)
            {
                State = new BasketState();
            }
            
            State.Value.Products.Add(product);
            await WriteStateAsync();
        }
        
        
    }

}
