using MarktGuruTask.Entities;
using MarktGuruTask.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarktGuruTask.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<Product> Add(Product product)
        {
            return _productRepository.Add(product);
        }

        public async Task<IEnumerable<Product>> GetProducts(int offset = 0, int count = 100)
        {
            return await _productRepository.GetProducts(offset, count);
        }

        public async Task<Product> GetProductDetails(int id)
        {
            return await _productRepository.GetProductDetails(id);
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            try
            {
                await _productRepository.UpdateProduct(product);
                var prod = await _productRepository.GetProductDetails(product.Id);
                return prod;
            }
            catch
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                await _productRepository.Delete(id);
            }
            catch
            {
                throw;
            }
        }
    }
}
