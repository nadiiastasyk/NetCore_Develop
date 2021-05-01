using Fiction_DZ6.Infrastructure;
using Fiction_DZ6.Models;
using Fiction_DZ6.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestSharp;
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
            services.AddControllersWithViews(configure =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                configure.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddDbContext<FictionDbContext>();
            services.AddScoped<ICharactersRepository, SQLCharactersRepository>();
            services.AddSingleton<IExternalImageServiceClient, ExternalImageServiceClient>();
            services.AddSingleton<IProcessingChannel, ImageProcessingChannel>();
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<FictionDbContext>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 1;
            });

            services.Configure<IFictionConfiguration>(Configuration.GetSection("Fiction"));
            services.AddScoped<SmsMessageSender>();
            services.AddScoped<EmailMessageSender>();
            services.AddSingleton<IRestClient, RestClient>();
            services.AddSingleton<IFictionConfiguration, FictionConfiguration>();

            services.AddScoped<IMessageSender>(messageSender =>
            {
                FictionConfiguration settings = messageSender.GetRequiredService<FictionConfiguration>();

                if (settings.Email.SenderEmailAddress != string.Empty)
                {
                    return messageSender.GetRequiredService<EmailMessageSender>();
                }

                else
                {
                    return messageSender.GetRequiredService<SmsMessageSender>();
                }
            });

            services.AddMemoryCache();
            services.AddHostedService<LoadImageService>();
            services.AddHostedService<UploadImageService>();
            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.SameSite = SameSiteMode.Lax;
            //    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            //    options.SlidingExpiration = true;
            //});
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

            app.Use(async (context, next) =>
            {
                Console.WriteLine($"Middleware before");
                await next();
                Console.WriteLine($"Middleware after");
            });

            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine($"Terminal Middleware");
            //    await context.Response.WriteAsync($"Terminal Middleware");
            //});

            app.Map("/map1", HandleMapTest1);            app.Map("/map2", HandleMapTest2);


            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseMiddleware<WriteToConsoleMiddleWare>();
            app.UseWriteToConsole("This is input");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.Use(request => context =>
            //{
            //    Console.WriteLine($"After endpoint: {context.GetEndpoint()?.DisplayName ?? "null"}");
            //    return request(context);
            //});

        }

        private static void HandleMapTest1(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test 1");
            });
        }

        private static void HandleMapTest2(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test 2");
            });
        }
    }
}
