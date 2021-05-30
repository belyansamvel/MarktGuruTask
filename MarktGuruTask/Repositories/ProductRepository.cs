using MarktGuruTask.Entities;
using Microsoft.EntityFrameworkCore;
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
            try
            {
                await _dbContext.Products.AddAsync(product);
                await _dbContext.SaveChangesAsync();
                return product;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetProducts(int offset = 0, int count = 100)
        {
            var data = await _dbContext.Products.Skip(offset).Take(100).ToListAsync();
            return data;
        }

        public async Task<Product> GetProductDetails(int id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateProduct(Product product)
        {
            try
            {
                _dbContext.Products.Update(product);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product==null)
            {
                throw new KeyNotFoundException("Product does not exist");
            }
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}
