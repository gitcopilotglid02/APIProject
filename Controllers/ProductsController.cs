using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiProject.Services;

namespace MyApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
    
        [HttpPost("import-csv")]
public async Task<IActionResult> ImportCsv([FromForm] ImportProductsRequest request)
{
    var file = request.File;
    if (file == null || file.Length == 0)
        return BadRequest("File is required.");

    try
    {
        int savedCount = await _productService.ImportProductsFromCsvAndReturnCount(file);
        return Ok(new { savedCount });
    }
    catch (System.Exception ex)
    {
        return StatusCode(500, $"Error importing products: {ex.Message}");
    }
}
    
}
}

