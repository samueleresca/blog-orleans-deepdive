using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Hosting;
using System;
using System.Threading.Tasks;

namespace Silo
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var siloBuilder = new SiloHostBuilder()
                .UseLocalhostClustering(serviceId:"blog-orleans-deepdive")
                .AddAdoNetGrainStorage("CartStorage", options=>
                    {
                        options.Invariant = "System.Data.SqlClient";
                        options.ConnectionString = "Data Source=localhost,1433;Initial Catalog=ServicePersistence;Integrated Security=False;User ID=sa;Password=P@55w0rd";
                        options.UseJsonFormat = true;
                    })
                .ConfigureLogging(logging => logging.AddConsole())
                .UseDashboard();


            using (var host = siloBuilder.Build())
            {
                await host.StartAsync();
                Console.ReadLine();
            }
        }
    }
}