using System.Collections.Generic;
using MyApiProject.Models;

namespace MyApiProject.Validators
{
    public interface IProductValidator
    {
        bool IsValid(Product p);
        IEnumerable<string> Validate(Product p);
    }
}
