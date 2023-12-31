﻿using ProductRetailApp.Core.Interfaces;
using ProductRetailApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductRetailApp.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<ProductDetailsInfo>, IProductRepository
    {
        public ProductRepository(DbContextClass dbContext) : base(dbContext)
        {

        }


        public void Update(ProductDetailsInfo product)
        {
            _dbContext.Products.Update(product);
        }

        public async Task<ProductDetailsInfo> GetById(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }
        public async Task Add(ProductDetailsInfo product)
        {
            await _dbContext.Products.AddAsync(product);
        }

        public async Task<IEnumerable<ProductDetailsInfo>> SearchProducts(string searchTerm, decimal? minPrice, decimal? maxPrice, DateTime? minPostedDate, DateTime? maxPostedDate)
        {
            var query = _dbContext.Products.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(searchTerm))
                query = query.Where(p => p.ProductName.Contains(searchTerm));

            if (minPrice.HasValue)
                query = query.Where(p => p.ProductPrice >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.ProductPrice <= maxPrice.Value);

            if (minPostedDate.HasValue)
                query = query.Where(p => p.PostedDate >= minPostedDate.Value);

            if (maxPostedDate.HasValue)
                query = query.Where(p => p.PostedDate <= maxPostedDate.Value);

            return await query.OrderByDescending(p => p.PostedDate).ToListAsync();
        }
    }
}
