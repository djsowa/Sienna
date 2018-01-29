using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SIENN.DbAccess.Context;
using SIENN.DbAccess.Entity;
using SIENN.DbAccess.Repositories;
using SIENN.Services.Command;
using AutoMapper;
using SIENN.Services.Models;
using SIENN.Services.ControllerServices.Crud;
using SIENN.Services.ControllerServices.Search;
using System;
using System.Collections.Generic;
using SIENN.Services.Mappings;

namespace SIENN.Tests
{
    public abstract class BaseInMemoryTest
    {
        protected static DbContextOptions<StoreDbContext> DbContextOptions;
        protected static IMapper AutoMapper;

        static BaseInMemoryTest()
        {
            Console.WriteLine("DB initializing...");
            SetupDb();
            Console.WriteLine("DB initialized");
            Console.WriteLine("DB seeding...");
            SeedDb();
            Console.WriteLine("DB seed completed");
            SetAutoMapper();
            Console.WriteLine("Finished");
        }

        private static void SetAutoMapper()
        {
            var config = new AutoMapper.MapperConfiguration(x => { x.CreateMissingTypeMaps = true; x.AddProfile<StoreMappingProfile>(); });

            config.CompileMappings();
            config.AssertConfigurationIsValid();

            AutoMapper = config.CreateMapper();
        }

        private static void SetupDb()
        {
            DbContextOptions = new DbContextOptionsBuilder<StoreDbContext>().UseInMemoryDatabase("SIENN_IN_MEM").EnableSensitiveDataLogging(true).Options;
        }

        private static void SeedDb()
        {
            using (var context = new StoreDbContext(DbContextOptions))
            {
                context.SeedDb();
            }
        }
    }
}