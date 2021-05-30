using MarktGuruTask.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarktGuruTask.Repositories
{
    public interface IProductRepository
    {
        Task<Product> Add(Product product);
        Task<IEnumerable<Product>> GetProducts(int offset, int count);
        Task<Product> GetProductDetails(int id);
        Task UpdateProduct(Product product);
        Task Delete(int id);
    }
}
