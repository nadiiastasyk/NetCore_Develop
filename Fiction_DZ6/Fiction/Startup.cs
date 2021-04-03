using Fiction_DZ6.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Fiction_DZ6
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<FictionDbContext>();
            services.AddScoped<ICharactersRepository, SQLCharactersRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.Use(request => context =>
            {
                Console.WriteLine($"Before Routing: {context.GetEndpoint()?.DisplayName ?? "null"}");
                return request(context);
            });


            app.UseRouting();

            app.Use(request => context =>
            {
                Console.WriteLine($"After routing: {context.GetEndpoint()?.DisplayName ?? "null"}");
                return request(context);
            });


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Use(request => context =>
            {
                Console.WriteLine($"After endpoint: {context.GetEndpoint()?.DisplayName ?? "null"}");
                return request(context);
            });

        }
    }
}
