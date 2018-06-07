using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Silo
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var siloBuilder = new SiloHostBuilder()
                .AddMemoryGrainStorage("dev")
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "blog-orleans-deepdive";
                })
                .Configure<EndpointOptions>(options =>
                    options.AdvertisedIPAddress = IPAddress.Loopback)
                .ConfigureLogging(logging => logging.AddConsole());


            using (var host = siloBuilder.Build())
            {
                await host.StartAsync();
                Console.ReadLine();
            }
        }
    }
}
