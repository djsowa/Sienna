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
using SIENN.Services.ControllerServices;
using SIENN.Services.Models;

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


            //Controller services
            services.AddScoped<ICrudControllerService<ProductModel, ProductBaseModel, Product>, ProductCrudControllerService>();
            services.AddScoped<ICrudControllerService<CategoryModel, CategoryBaseModel, ProductCategory>, ProductCategoryCrudControllerService>();
            services.AddScoped<ICrudControllerService<TypeModel, TypeBaseModel, ProductType>, ProductTypeCrudControllerService>();
            services.AddScoped<ICrudControllerService<UnitModel, UnitBaseModel, ProductUnit>, ProductUnitCrudControllerService>();

            services.AddMvc();
            services.AddAutoMapper();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SIENN Recruitment API v1");
            });

            app.UseMvc();
        }
    }
}
