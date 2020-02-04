using AnemicPizza.Domain;
using AnemicPizza.Domain.Models.Basket;
using AnemicPizza.Domain.Models.Ordering;
using AnemicPizza.Domain.Models.Products;
using AnemicPizza.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            services
                .AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("SqlServer"))
                );

            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            services.AddScoped<IRepository<CustomerBasket>, BasketRepository>();
            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddScoped<IRepository<Pizza>, PizzaRepository>();
            services.AddScoped<IRepository<Supplier>, SupplierRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
