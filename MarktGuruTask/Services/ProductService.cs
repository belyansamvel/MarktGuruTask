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

        public async Task<Product> Add(Product product)
        {
            return await _productRepository.Add(product);
        }

        public async Task<IEnumerable<Product>> GetProducts(int offset = 0, int count = 100)
        {
            return await _productRepository.GetProducts(offset, count);
        }
    }
}
