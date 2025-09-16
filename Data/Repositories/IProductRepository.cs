// Define IProductRepository with Task AddAsync(Product p), Task<IEnumerable<Product>> GetAllAsync(), Task SaveChangesAsync()
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApiProject.Models; 

namespace MyApiProject.Data.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(Product p);
        Task<IEnumerable<Product>> GetAllAsync();
        Task SaveChangesAsync();
    }
}
