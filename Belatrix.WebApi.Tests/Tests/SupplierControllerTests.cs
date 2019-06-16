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
    public class SupplierControllerTests
    {
        private readonly BelatrixDbContextBuilder _builder;
        public SupplierControllerTests()
        {
            _builder = new BelatrixDbContextBuilder();
        }

        [Fact]
        public async Task SupplierController_GetSuppliers_Ok()
        {
            //Arrange
            var db = _builder
                .ConfigureInMemory()
                .AddTenRandomSuppliers()
                .Build();
            var repository = new Repository<Models.Supplier>(db);
            var controller = new SupplierController(repository);

            //Act
            var response = (await controller.GetSuppliers())
                .Result as OkObjectResult;
            var values = response.Value as List<Models.Supplier>;

            //Assert
            values.Count.Should().Be(10);
        }

        [Fact]
        public async Task SupplierController_PostSupplier_Ok()
        {
            //Arrange
            var db = _builder
                .ConfigureInMemory()
                .AddRandomSupplier()
                .Build();
            Repository<Models.Supplier> repository = new Repository<Models.Supplier>(db);
            SupplierController controller = new SupplierController(repository);
            Models.Supplier SupplierToAdd = new Models.Supplier();

            //Act
            await controller.PostSupplier(SupplierToAdd);
            var response = await repository.Read() as List<Models.Supplier>;

            //Assert
            response.Count.Should().Be(2);
        }

        [Fact]
        public async Task SupplierController_PutSupplier_Ok()
        {
            //Arrange
            Models.Supplier Supplier = new Models.Supplier { Id = 1, CompanyName = "Supplier" };
            var db = _builder
                .ConfigureInMemory()
                .AddSpecificSupplier(Supplier)
                .Build();
            Repository<Models.Supplier> repository = new Repository<Models.Supplier>(db);
            SupplierController controller = new SupplierController(repository);
            Supplier.CompanyName = "Test Supplier";

            //Act
            await controller.PutSupplier(Supplier);
            var response = await repository.Read() as List<Models.Supplier>;

            //Assert
            response[0].CompanyName.Should().Be("Test Supplier");
        }

        [Fact]
        public async Task SupplierController_DeleteSupplier_Ok()
        {
            //Arrange
            Models.Supplier Supplier = new Models.Supplier { Id = 2 };
            var db = _builder
                .ConfigureInMemory()
                .AddSpecificSupplier(Supplier)
                .Build();
            Repository<Models.Supplier> repository = new Repository<Models.Supplier>(db);
            SupplierController controller = new SupplierController(repository);

            //Act
            await controller.DeleteSupplier(2);
            var response = await repository.Read() as List<Models.Supplier>;

            //Assert
            response.Count.Should().Be(0);
        }
    }
}
