using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using MyApiProject.Data.Repositories;
using MyApiProject.Models;
using MyApiProject.Validators;

namespace MyApiProject.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductValidator _validator;
        private readonly IProductRepository _repository;

        public ProductService(IProductValidator validator, IProductRepository repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public async Task ImportProductsFromCsv(IFormFile file)
        {
            await ImportProductsFromCsvAndReturnCount(file);
        }

        public async Task<int> ImportProductsFromCsvAndReturnCount(IFormFile file)
        {            

            Console.WriteLine($"Importing products");
            var products = new List<Product>();
            using (var stream = file.OpenReadStream())
            using (var reader = new StreamReader(stream))
            {
                var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true
                };
                using (var csv = new CsvReader(reader, config))
                {
                    var dateTimeOptions = new CsvHelper.TypeConversion.TypeConverterOptions { Formats = new[] { "dd/MM/yyyy", "yyyy-MM-dd" } };
                    csv.Context.TypeConverterOptionsCache.AddOptions<System.DateTime>(dateTimeOptions);
                    products = csv.GetRecords<Product>().ToList();
                }
            }

            var validProducts = products.Where(p => !_validator.Validate(p).Any()).ToList();
            foreach (var product in validProducts)
            {
                await _repository.AddAsync(product);
            }
            await _repository.SaveChangesAsync();
            return validProducts.Count;
        }
    }
}
