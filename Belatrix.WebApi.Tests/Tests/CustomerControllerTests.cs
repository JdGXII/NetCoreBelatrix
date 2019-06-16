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
    public class CustomerControllerTests
    {
        private readonly BelatrixDbContextBuilder _builder;
        public CustomerControllerTests()
        {
            _builder = new BelatrixDbContextBuilder();
        }

        [Fact]
        public async Task CustomerController_GetCustomers_Ok()
        {
            //Arrange
            var db = _builder
                .ConfigureInMemory()
                .AddTenRandomCustomers()
                .Build();
            var repository = new Repository<Models.Customer>(db);
            var controller = new CustomerController(repository);

            //Act
            var response = (await controller.GetCustomers())
                .Result as OkObjectResult;
            var values = response.Value as List<Models.Customer>;
            
            //Assert
            values.Count.Should().Be(10);
        }

        [Fact]
        public async Task CustomerController_PostCustomer_Ok()
        {
            //Arrange
            var db = _builder
                .ConfigureInMemory()
                .AddRandomCustomer()
                .Build();
            Repository<Models.Customer> repository = new Repository<Models.Customer>(db);
            CustomerController controller = new CustomerController(repository);
            Models.Customer customerToAdd = new Models.Customer();

            //Act
            await controller.PostCustomer(customerToAdd);
            var response = await repository.Read() as List<Models.Customer>;

            //Assert
            response.Count.Should().Be(2);
        }

        [Fact]
        public async Task CustomerController_PutCustomer_Ok()
        {
            //Arrange
            Models.Customer customer = new Models.Customer { Id = 1, FirstName = "Jose" };
            var db = _builder
                .ConfigureInMemory()
                .AddSpecificCustomer(customer)
                .Build();
            Repository<Models.Customer> repository = new Repository<Models.Customer>(db);
            CustomerController controller = new CustomerController(repository);
            customer.FirstName = "Cesar";

            //Act
            await controller.PutCustomer(customer);
            var response = await repository.Read() as List<Models.Customer>;

            //Assert
            response[0].FirstName.Should().Be("Cesar");
        }

        [Fact]
        public async Task CustomerController_DeleteCustomer_Ok()
        {
            //Arrange
            Models.Customer customer = new Models.Customer { Id = 2, FirstName = "Jose" };
            var db = _builder
                .ConfigureInMemory()
                .AddSpecificCustomer(customer)
                .Build();
            Repository<Models.Customer> repository = new Repository<Models.Customer>(db);
            CustomerController controller = new CustomerController(repository);

            //Act
            await controller.DeleteCustomer(2);
            var response = await repository.Read() as List<Models.Customer>;

            //Assert
            response.Count.Should().Be(0);
        }
    }
}
