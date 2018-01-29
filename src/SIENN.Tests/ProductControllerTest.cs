using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIENN.DbAccess.Context;
using SIENN.DbAccess.Repositories;
using SIENN.Services.ControllerServices.Crud;
using SIENN.Services.ControllerServices.Search;
using SIENN.Services.Models;
using SIENN.WebApi.Controllers;
using Xunit;

namespace SIENN.Tests
{
    public class ProductControllerTest : BaseInMemoryTest
    {
        public ProductControllerTest() : base()
        {

        }

        private ProductController CreateController(StoreDbContext context)
        {
            var repository = new ProductRepository(context);
            return new ProductController(new ProductCrudControllerService(context, AutoMapper, repository),
                                         new ProductSearchControllerService(context, AutoMapper, repository));
        }

        public int AddNew(string code, int seed, decimal price = decimal.Zero)
        {
            using (var context = new StoreDbContext(DbContextOptions))
            {

                var controller = CreateController(context);

                var addProductModel = new ProductBaseModel()
                {
                    Code = code,
                    Description = $"{code} DESC1",
                    UnitId = 1,
                    ProductTypeId = 1,
                    Price = price != decimal.Zero ? price : Convert.ToDecimal(new Random(seed).Next(20, 100)),
                    NextDelivery = DateTime.UtcNow.AddDays(new Random(seed).Next(0, 31)),
                    Categories = new List<int>(new[] { 4 })
                };

                var result = controller.Add(addProductModel).GetAwaiter().GetResult();

                var createdAtResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
                var product = createdAtResult.Value.Should().BeAssignableTo<ProductModel>().Subject;

                Assert.True(product.Id > 0);
                return product.Id;
            }
        }


        [Fact]
        public void Add_OK()
        {
            var newId = AddNew("AddOK", 123);
        }

        [Fact]
        public void Edit_OK()
        {
            int addeddId = AddNew("EDIT_TEST_OK", 4321);

            using (var context = new StoreDbContext(DbContextOptions))
            {

                var controller = CreateController(context);

                var editProductModel = new ProductBaseModel()
                {
                    Code = "TEST_EDIT_OK",
                    Description = "New description",
                    UnitId = 2,
                    ProductTypeId = 1,
                    Price = 20,
                    NextDelivery = DateTime.UtcNow.AddDays(14),
                    Categories = new List<int>(new[] { 1, 2, 3 })
                };


                var editResult = controller.Update(addeddId, editProductModel).GetAwaiter().GetResult();

                var editedAtResult = editResult.Should().BeOfType<CreatedAtActionResult>().Subject;
                var updatedProduct = editedAtResult.Value.Should().BeAssignableTo<ProductModel>().Subject;

                Assert.Equal("New description", updatedProduct.Description);
                Assert.Equal(2, updatedProduct.UnitId);

                Assert.True(updatedProduct.Categories.All(c => c == 1 || c == 2 || c == 3));
            }
        }

        [Fact]
        public void Delete_OK()
        {
            int addeddId = AddNew("DELETE_TEST_OK", 43295121);

            using (var context = new StoreDbContext(DbContextOptions))
            {

                var controller = CreateController(context);

                var deleteResult = controller.Remove(addeddId).GetAwaiter().GetResult();

                var okResult = deleteResult.Should().BeOfType<OkResult>().Subject;
                Assert.Equal(200, okResult.StatusCode);
            }

            using (var context = new StoreDbContext(DbContextOptions))
            {
                Assert.Null(context.Products.FirstOrDefault(x => x.Id == addeddId));
                Assert.Null(context.Products.FirstOrDefault(x => x.Code == "DELETE_TEST_OK"));
            }
        }


    }
}
