// Implement ProductRepository using AppDbContext and IProductRepository methods (AddAsync, GetAllAsync, SaveChangesAsync)
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyApiProject.Data;
using MyApiProject.Models; 

namespace MyApiProject.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product p)
        {
            await _context.Products.AddAsync(p);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
