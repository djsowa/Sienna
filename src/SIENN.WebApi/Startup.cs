using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SIENN.DbAccess.Context;
using SIENN.DbAccess.Entity;
using SIENN.DbAccess.Repositories;
using SIENN.Services.Command;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using SIENN.Services.Models;
using SIENN.Services.ControllerServices.Crud;
using SIENN.Services.ControllerServices.Search;
using System;
using System.Linq;

namespace SIENN.WebApi
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "SIENN Recruitment API"
                });
            });


            //Context
            services.AddDbContext<StoreDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("OnePostgres")));

            //Repositories
            services.AddScoped<IGenericRepository<Product>, ProductRepository>();
            services.AddScoped<IGenericRepository<ProductCategory>, ProductCategoryRepository>();
            services.AddScoped<IGenericRepository<ProductType>, ProductTypeRepository>();
            services.AddScoped<IGenericRepository<ProductUnit>, ProductUnitRepository>();


            //CRUD Controller services
            services.AddScoped<ICrudControllerService<ProductModel, ProductBaseModel, Product>, ProductCrudControllerService>();
            services.AddScoped<ICrudControllerService<CategoryModel, CategoryBaseModel, ProductCategory>, ProductCategoryCrudControllerService>();
            services.AddScoped<ICrudControllerService<TypeModel, TypeBaseModel, ProductType>, ProductTypeCrudControllerService>();
            services.AddScoped<ICrudControllerService<UnitModel, UnitBaseModel, ProductUnit>, ProductUnitCrudControllerService>();

            //Search Services
            services.AddScoped<IProductSearchControllerService<ProductModel, ProductFilterModel>, ProductSearchControllerService>();
            services.AddScoped<IStoreControllerService<ProductViewModel, ProductFilterModel, Product>, StoreControllerService>();



            services.AddMvc();
            services.AddAutoMapper();
        }

        public void Seed(IApplicationBuilder app)
        {
            // Get an instance of the DbContext from the DI container
            using (var context = new StoreDbContext(new DbContextOptionsBuilder<StoreDbContext>().UseNpgsql(Configuration.GetConnectionString("OnePostgres")).Options))
            {
                // perform database delete
                context.Database.EnsureDeleted();

                context.Database.EnsureCreated();
                context.SeedDb();

            }
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Console.WriteLine($"Environment: {env.EnvironmentName}");

            if (env.IsDevelopment() || env.EnvironmentName.ToUpper() == "LOCAL")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SIENN Recruitment API v1");
            });

            app.UseMvc();

            Seed(app);
        }
    }
}
