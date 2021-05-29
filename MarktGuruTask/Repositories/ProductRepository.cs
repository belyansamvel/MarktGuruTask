using MarktGuruTask.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarktGuruTask.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _dbContext;

        public ProductRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> Add(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts(int offset = 0, int count = 100)
        {
            var data = await _dbContext.Products.Skip(offset).Take(100).ToListAsync();
            return data;
        }
    }
}
