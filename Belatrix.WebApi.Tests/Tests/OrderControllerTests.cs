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
    public class OrderControllerTests
    {
        private readonly BelatrixDbContextBuilder _builder;
        public OrderControllerTests()
        {
            _builder = new BelatrixDbContextBuilder();
        }

        [Fact]
        public async Task OrderController_GetOrders_Ok()
        {
            //Arrange
            var db = _builder
                .ConfigureInMemory()
                .AddTenRandomOrders()
                .Build();
            var repository = new Repository<Models.Order>(db);
            var controller = new OrderController(repository);

            //Act
            var response = (await controller.GetOrders())
                .Result as OkObjectResult;
            var values = response.Value as List<Models.Order>;

            //Assert
            values.Count.Should().Be(10);
        }

        [Fact]
        public async Task OrderController_PostOrder_Ok()
        {
            //Arrange
            var db = _builder
                .ConfigureInMemory()
                .AddRandomOrder()
                .Build();
            Repository<Models.Order> repository = new Repository<Models.Order>(db);
            OrderController controller = new OrderController(repository);
            Models.Order OrderToAdd = new Models.Order();
            
            //Act
            await controller.PostOrder(OrderToAdd);
            var response = await repository.Read() as List<Models.Order>;

            //Assert
            response.Count.Should().Be(2);
        }

        [Fact]
        public async Task OrderController_PutOrder_Ok()
        {
            //Arrange
            Models.Order Order = new Models.Order { Id = 1, TotalAmount = 25 };
            var db = _builder
                .ConfigureInMemory()
                .AddSpecificOrder(Order)
                .Build();
            Repository<Models.Order> repository = new Repository<Models.Order>(db);
            OrderController controller = new OrderController(repository);
            Order.TotalAmount = 50;

            //Act
            await controller.PutOrder(Order);
            var response = await repository.Read() as List<Models.Order>;

            //Assert
            response[0].TotalAmount.Should().Be(50);
        }

        [Fact]
        public async Task OrderController_DeleteOrder_Ok()
        {
            //Arrange
            Models.Order Order = new Models.Order { Id = 2 };
            var db = _builder
                .ConfigureInMemory()
                .AddSpecificOrder(Order)
                .Build();
            Repository<Models.Order> repository = new Repository<Models.Order>(db);
            OrderController controller = new OrderController(repository);
            
            //Act
            await controller.DeleteOrder(2);
            var response = await repository.Read() as List<Models.Order>;

            //Assert
            response.Count.Should().Be(0);
        }
    }
}
