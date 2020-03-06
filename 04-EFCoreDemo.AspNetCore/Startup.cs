using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EFCoreDemo
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Itt konfiguráljuk be a webalkalmazás szolgáltatásait, amik mindenhol elérhetők lesznek.
        public void ConfigureServices(IServiceCollection services)
        {
            // A konfiguráció alapértelmezetten pl. az appsettings.json fájlból jön.
            var connectionString = Configuration.GetConnectionString("DogFarmDB");
            services.AddDbContext<DogFarmDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        // Itt konfiguráljuk be a kiszolgálási csővezetéket
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // Erre a végpontra saját kezelőfüggvényt regisztrálunk
                endpoints.MapGet("/", async context =>
                {
                    // Nem tesszük using-ba a DbContext-et, mert erről a kapcsolat bontásáról 
                    // a keretrendszer fog gondoskodni
                    // Így kérhetjük el a beregisztrált DbContext példányunkat:
                    var dbContext = context.RequestServices.GetRequiredService<DogFarmDbContext>();
                    var dog = await dbContext.Dogs.FirstOrDefaultAsync();
                    await context.Response.WriteAsync(dog == null ? ":(" : $"{dog.Name} says: bark-bark!");
                });
            });
        }
    }
}