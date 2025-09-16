using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyApiProject.Services
{
    public interface IProductService
    {
        Task ImportProductsFromCsv(IFormFile file);
        Task<int> ImportProductsFromCsvAndReturnCount(IFormFile file);
    }
}
