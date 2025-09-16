using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Moq;
using MyApiProject.Data.Repositories;
using MyApiProject.Models;
using MyApiProject.Services;
using MyApiProject.Validators;
using Xunit;

namespace MyApiProject.Tests
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task ImportProductsFromCsvAndReturnCount_ValidProducts_ReturnsCorrectCount()
        {
            // Arrange
            var csvContent = "Id,Name,Price,Sku,CreatedAt\n1,Test,10.5,SKU1,2025-09-15";
            var fileMock = new Mock<IFormFile>();
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(csvContent));
            fileMock.Setup(f => f.OpenReadStream()).Returns(ms);
            fileMock.Setup(f => f.Length).Returns(ms.Length);

            var validatorMock = new Mock<IProductValidator>();
            validatorMock.Setup(v => v.Validate(It.IsAny<Product>())).Returns(new List<string>());

            var repoMock = new Mock<IProductRepository>();
            repoMock.Setup(r => r.AddAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
            repoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            var service = new ProductService(validatorMock.Object, repoMock.Object);

            // Act
            var count = await service.ImportProductsFromCsvAndReturnCount(fileMock.Object);

            // Assert
            Assert.Equal(1, count);
            repoMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
            repoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
