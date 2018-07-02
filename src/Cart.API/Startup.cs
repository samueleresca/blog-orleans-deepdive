using System;
using System.Threading.Tasks;
using GrainInterfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;

namespace Cart.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton(CreateClusterClient);
        }

        private IClusterClient CreateClusterClient(IServiceProvider serviceProvider)
        {
            var client = new ClientBuilder()
                .UseLocalhostClustering( serviceId: "blog-orleans-deepdive")
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(ICartGrain).Assembly).WithReferences())
                .ConfigureLogging(_ => _.AddConsole())
                .Build();
           
            StartClientWithRetries( client).Wait();
            return client;
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }

        private static async Task StartClientWithRetries(IClusterClient client)
        {
            for (var i = 0; i < 5; i++)
            {
                try
                {
                    await client.Connect();
                    return;
                }
                catch (Exception)
                {
                    // ignored
                }

                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }
    }
}