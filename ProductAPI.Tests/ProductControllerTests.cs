
using Xunit;
using Moq;
using ProductAPI.Controllers;
using ProductAPI.Services;
using ProductAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ProductAPI.Tests
{
    public class ProductControllerTests
    {
        private readonly Mock<ProductService> _mockService;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockService = new Mock<ProductService>(null);
            _controller = new ProductController(_mockService.Object);
        }

        [Fact]
        public async Task CreateProduct_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var product = new Product { Id = "1", Name = "TestProduct", Price = 10.99M, Category = "TestCategory" };
            _mockService.Setup(s => s.CreateProductAsync(product)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateProduct(product);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetProductById", actionResult.ActionName);
        }

        [Fact]
        public async Task GetProductById_ReturnsOkObjectResult_WhenProductExists()
        {
            // Arrange
            var product = new Product { Id = "1", Name = "TestProduct", Price = 10.99M, Category = "TestCategory" };
            _mockService.Setup(s => s.GetProductByIdAsync("1")).ReturnsAsync(product);

            // Act
            var result = await _controller.GetProductById("1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(product, okResult.Value);
        }

        [Fact]
        public async Task GetProductById_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            _mockService.Setup(s => s.GetProductByIdAsync("1")).ReturnsAsync((Product)null);

            // Act
            var result = await _controller.GetProductById("1");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
