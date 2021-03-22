
using BookStore.models;
using BookStore.models.repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Configuration;

namespace BookStore
{
    public class Startup
    {
        private readonly IConfiguration iconfigration;
        public Startup(IConfiguration iconfigration)
        {
            this.iconfigration = iconfigration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<IBookStoreRepository<Author>, AuthorDbRepository>();
            services.AddScoped<IBookStoreRepository<Book>,BookStoreDbRepository>();
            services.AddDbContext<BookStoreDBcontext>(
                option =>
                {
                    option.UseSqlServer(iconfigration.GetConnectionString("sqlcon"));
                }
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {

                    context.Response.Redirect("Home/Index");
                });

                endpoints.MapControllerRoute(name: "default", pattern: "{controller}/{action}");
                

                



            });
        }
    }
}
