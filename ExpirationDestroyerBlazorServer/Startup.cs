using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ExpirationDestroyerBlazorServer.DataAccess.DBContexts;
using Microsoft.EntityFrameworkCore;
using ExpirationDestroyerBlazorServer.BusinessLogic.Mappers;
using ExpirationDestroyerBlazorServer.BusinessLogic.ProductsService;
using ExpirationDestroyerBlazorServer.DataAccess;
using ExpirationDestroyerBlazorServer.DataAccess.ProductsRepository;
using AutoMapper;
using System.Net.Http;

namespace ExpirationDestroyerBlazorServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddScoped<IProductsRepository, EFSQLiteProductsRepository>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddDbContext<EFSQLiteDBContext>(options => options.UseSqlite(Configuration.GetConnectionString("SQLite")));
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
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapControllers();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
