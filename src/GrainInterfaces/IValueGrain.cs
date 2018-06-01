using Orleans;
using System.Threading.Tasks;

namespace GrainInterfaces
{
    public interface IValueGrain : IGrainWithIntegerKey
    {
        Task<string> GetValue();
        
        Task SetValue(string value);
    }
}
