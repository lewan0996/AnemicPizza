using System;
using System.Reflection;
using AnemicPizza.API.Filters;
using AnemicPizza.Core;
using AnemicPizza.Core.Models.Basket;
using AnemicPizza.Core.Models.Ordering;
using AnemicPizza.Core.Models.Products;
using AnemicPizza.Core.Services;
using AnemicPizza.Core.Services.Interfaces;
using AnemicPizza.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AnemicPizza.API
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
            services.AddControllers();

            services.AddMvc(options => options.Filters.Add(typeof(GlobalExceptionFilter)));

            services
                .AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("SqlServer"))
                );

            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddScoped<IRepository<CustomerBasket>, BasketRepository>();
            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddScoped<IRepository<Pizza>, PizzaRepository>();
            services.AddScoped<IRepository<Supplier>, SupplierRepository>();

            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<IPizzaService, PizzaService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AnemicPizza API",
                    Version = "v1",
                    Description = "The AnemicPizza API"
                });
                options.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}\{Assembly.GetExecutingAssembly().GetName().Name}.xml");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Anemic Pizza API");
                });

            var swaggerRewriteOptions = new RewriteOptions();
            swaggerRewriteOptions.AddRedirect("^$", "swagger");
            app.UseRewriter(swaggerRewriteOptions);

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
