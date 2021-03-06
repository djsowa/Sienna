﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SIENN.DbAccess.Entity;
using SIENN.DbAccess.Context;

namespace SIENN.DbAccess.Repositories
{
    public class ProductRepository : IGenericRepository<Product>
    {
        private DbSet<Product> _entities;
        private StoreDbContext _context;

        public ProductRepository(StoreDbContext context)
        {
            _context = context;
            _entities = context.Set<Product>();
        }

        public virtual async Task<Product> GetAsync(int id)
        {
            return await GetQueryable().SingleOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _entities.ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<Product>> GetRangeAsync(int start, int count)
        {
            return await GetQueryable().OrderBy(x => x.Id)
                                       .Skip(start).Take(count)
                                       .ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<Product>> GetRangeAsync(int start, int count,
                                                                      Expression<Func<Product, bool>> predicate)
        {
            return await GetQueryable().Where(predicate)
                                       .OrderBy(x => x.Id)
                                       .Skip(start).Take(count)
                                       .ToListAsync().ConfigureAwait(false);
        }

        public virtual IEnumerable<Product> Find(Expression<Func<Product, bool>> predicate)
        {
            return GetQueryable().Where(predicate);
        }

        public virtual async Task<int> CountAsync()
        {
            return await _entities.CountAsync().ConfigureAwait(false);
        }

        public virtual async Task AddAsync(Product entity)
        {
            if (await _entities.AnyAsync(p => p.Code == entity.Code))
            {
                throw new InvalidOperationException("Product with the same code already exists.");
            }

            await _entities.AddAsync(entity).ConfigureAwait(false);
        }

        public virtual void Remove(Product entity)
        {
            _entities.Remove(entity);
        }

        public void Update(Product entity)
        {
            List<int> newCategories = entity.Categories.Select(x => x.CategoryId).ToList();

            if (_entities.Any(p => p.Code == entity.Code && p.Id != entity.Id))
            {
                throw new InvalidOperationException("Product with the same code already exists.");
            }

            foreach (var prodToCat in entity.Categories)
            {
                prodToCat.ProductId = entity.Id;
            }

            if (_context.Entry(entity).State == EntityState.Detached)
                _context.Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;

            var existingCategories = _context.ProductsToCategories.Where(c => c.ProductId == entity.Id);

            foreach (var protToCat in entity.Categories)
            {
                //check for new assigned categories
                if (!existingCategories.Any(e => e.CategoryId == protToCat.CategoryId))
                {
                    _context.Entry(protToCat).State = EntityState.Added;
                }
            }

            foreach (var protToCat in existingCategories)
            {
                //check for deleted categories connection
                if (!newCategories.Any(e => e == protToCat.CategoryId))
                {
                    _context.Entry(protToCat).State = EntityState.Deleted;
                }
            }
        }

        public IQueryable<Product> GetQueryable()
        {
            return _entities.Include(p => p.Unit)
                            .Include(p => p.ProductType)
                            .Include(p => p.Categories)
                            .ThenInclude(c => c.Category)
                            .AsQueryable();
        }
    }
}
