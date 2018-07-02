using System.Collections.Generic;
using Orleans;
using System.Threading.Tasks;
using GrainInterfaces.States;

namespace GrainInterfaces
{
    public interface ICartGrain : IGrainWithGuidKey
    {
        Task<Cart> GetCart();
        
        Task<List<Product>> GetProducts();
        
        Task AddProduct(Product product);
    }
}
