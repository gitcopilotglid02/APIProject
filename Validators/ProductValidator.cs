//Implement basic checks: Name not null/empty, Price > 0, SKU not null and unique-check left for service/db
using System; 
using System.Collections.Generic;
using MyApiProject.Models;

namespace MyApiProject.Validators
{
    public class ProductValidator : IProductValidator
    {
        public bool IsValid(Product p)
        {
            return !string.IsNullOrEmpty(p.Name) && p.Price > 0 && !string.IsNullOrEmpty(p.Sku);
        }

        public IEnumerable<string> Validate(Product p)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(p.Name))
                errors.Add("Name is required.");

            if (p.Price <= 0)
                errors.Add("Price must be greater than zero.");

            if (string.IsNullOrEmpty(p.Sku))
                errors.Add("SKU is required.");

            return errors;
        }
    }
}
