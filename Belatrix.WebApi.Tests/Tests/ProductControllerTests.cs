using Belatrix.WebApi.Controllers;
using Belatrix.WebApi.Repository.Postgresql;
using Belatrix.WebApi.Tests.Builder.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Belatrix.WebApi.Tests.Tests
{
    public class ProductControllerTests
    {
        private readonly BelatrixDbContextBuilder _builder;
        public ProductControllerTests()
        {
            _builder = new BelatrixDbContextBuilder();
        }

        [Fact]
        public async Task ProductController_GetProducts_Ok()
        {
            //Arrange
            var db = _builder
                .ConfigureInMemory()
                .AddTenRandomProducts()
                .Build();
            var repository = new Repository<Models.Product>(db);
            var controller = new ProductController(repository);

            //Act
            var response = (await controller.GetProducts())
                .Result as OkObjectResult;
            var values = response.Value as List<Models.Product>;

            //Assert
            values.Count.Should().Be(10);
        }

        [Fact]
        public async Task ProductController_PostProduct_Ok()
        {
            //Arrange
            var db = _builder
                .ConfigureInMemory()
                .AddRandomProduct()
                .Build();
            Repository<Models.Product> repository = new Repository<Models.Product>(db);
            ProductController controller = new ProductController(repository);
            Models.Product ProductToAdd = new Models.Product();

            //Act
            await controller.PostProduct(ProductToAdd);
            var response = await repository.Read() as List<Models.Product>;

            //Assert
            response.Count.Should().Be(2);
        }

        [Fact]
        public async Task ProductController_PutProduct_Ok()
        {
            //Arrange
            Models.Product Product = new Models.Product { Id = 1, ProductName = "Product" };
            var db = _builder
                .ConfigureInMemory()
                .AddSpecificProduct(Product)
                .Build();
            Repository<Models.Product> repository = new Repository<Models.Product>(db);
            ProductController controller = new ProductController(repository);
            Product.ProductName = "Test Product";

            //Act
            await controller.PutProduct(Product);
            var response = await repository.Read() as List<Models.Product>;

            //Assert
            response[0].ProductName.Should().Be("Test Product");
        }

        [Fact]
        public async Task ProductController_DeleteProduct_Ok()
        {
            //Arrange
            Models.Product Product = new Models.Product { Id = 2 };
            var db = _builder
                .ConfigureInMemory()
                .AddSpecificProduct(Product)
                .Build();
            Repository<Models.Product> repository = new Repository<Models.Product>(db);
            ProductController controller = new ProductController(repository);

            //Act
            await controller.DeleteProduct(2);
            var response = await repository.Read() as List<Models.Product>;

            //Assert
            response.Count.Should().Be(0);
        }
    }
}
