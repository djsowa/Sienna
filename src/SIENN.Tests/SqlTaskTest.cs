using System;
using System.Collections.Generic;
using System.Data;
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
    public class SqlTaskTest : BaseInMemoryTest
    {
        public SqlTaskTest() : base()
        {

        }


        [Fact]
        public void ShowNexMonthDelivery()
        {
            var nextMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.AddMonths(1).Month, 1).Date;
            Console.WriteLine($"List of not available products with delivery in next month:{nextMonth}");

            using (var context = new StoreDbContext(DbContextOptions))
            {
                var query = context.Products.AsQueryable().Where(x => x.IsAvailable == false && x.NextDelivery >= nextMonth).ToList();
                foreach (var item in query)
                {
                    Console.WriteLine($"Code: {item.Code}, Price:{item.Price}, Delivery: {item.NextDelivery}");
                }
            }
            Console.WriteLine($"------------------------END----------------------------");
        }

        [Fact]
        public void ShowAvailableProductsWithMoreThanOneCategory()
        {
            Console.WriteLine($"List of available products with more than one category");

            using (var context = new StoreDbContext(DbContextOptions))
            {
                var query = context.Products.AsQueryable().Include(p => p.Categories).ThenInclude(c => c.Category)
                                            .Where(x => x.IsAvailable == true && x.Categories.Count > 1).ToList();
                foreach (var item in query)
                {
                    Console.WriteLine($"Code: {item.Code}, Price:{item.Price}, Delivery: {item.NextDelivery}, Categories: {string.Join("; ", item.Categories.Select(c => c.Category.Code).ToList())}");
                }
            }
            Console.WriteLine($"------------------------END----------------------------");
        }

        [Fact]
        public void ShowTop3Categories()
        {
            Console.WriteLine($"List of top 3 categories with avarage price of available products");

            using (var context = new StoreDbContext(DbContextOptions))
            {
                var query = context.Categories.AsQueryable().Include(p => p.Products).ThenInclude(c => c.Product)
                                              .Where(c => c.Products.Any(p => p.Product.IsAvailable))
                                              .Select(x => new
                                              {
                                                  Code = x.Code,
                                                  Name = x.Description,
                                                  AvailableProducts = x.Products.Count(p => p.Product.IsAvailable),
                                                  AveragePrice = x.Products.Where(p => p.Product.IsAvailable).Average(p => p.Product.Price)
                                              }).OrderByDescending(x => x.AveragePrice).Take(3).ToList();
                int i = 1;
                foreach (var item in query)
                {
                    Console.WriteLine($"{i} Category: {item.Code}-{item.Name}, AvgPrice:{item.AveragePrice}, ProductCounts: {item.AvailableProducts}");
                    Console.WriteLine($"     Producs details");

                    foreach (var product in context.Products.Where(p => p.Categories.Any(c => c.Category.Code == item.Code)))
                    {
                        Console.WriteLine($"     Product: {product.Code}, Price:{product.Price}, Available: {product.IsAvailable}");
                    }
                    i++;
                }
            }
            Console.WriteLine($"------------------------END----------------------------");
        }

        [Fact]
        public void ShowTop3Categories_2()
        {
            Console.WriteLine($"List of top 3 categories with avarage price of all products");

            using (var context = new StoreDbContext(DbContextOptions))
            {
                var query = context.Categories.AsQueryable().Include(p => p.Products).ThenInclude(c => c.Product)
                                              .Select(x => new
                                              {
                                                  Code = x.Code,
                                                  Name = x.Description,
                                                  AvailableProducts = x.Products.Count(p => p.Product.IsAvailable),
                                                  AveragePrice = x.Products.Average(p => p.Product.Price)
                                              }).OrderByDescending(x => x.AveragePrice).Take(3).ToList();
                int i = 1;
                foreach (var item in query)
                {
                    Console.WriteLine($"{i} Category: {item.Code}-{item.Name}, AvgPrice:{item.AveragePrice}, ProductCounts: {item.AvailableProducts}");
                    Console.WriteLine($"     Producs details");

                    foreach (var product in context.Products.Where(p => p.Categories.Any(c => c.Category.Code == item.Code)))
                    {
                        Console.WriteLine($"     Product: {product.Code}, Price:{product.Price}, Available: {product.IsAvailable}");
                    }
                    i++;
                }
            }
            Console.WriteLine($"------------------------END----------------------------");
        }
    }
}
