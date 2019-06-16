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
    public class OrderItemControllerTests
    {
        private readonly BelatrixDbContextBuilder _builder;
        public OrderItemControllerTests()
        {
            _builder = new BelatrixDbContextBuilder();
        }

        [Fact]
        public async Task OrderItemController_GetOrderItems_Ok()
        {
            //Arrange
            var db = _builder
                .ConfigureInMemory()
                .AddTenRandomOrderItems()
                .Build();
            var repository = new Repository<Models.OrderItem>(db);
            var controller = new OrderItemController(repository);

            //Act
            var response = (await controller.GetOrderItems())
                .Result as OkObjectResult;
            var values = response.Value as List<Models.OrderItem>;

            //Assert
            values.Count.Should().Be(10);
        }

        [Fact]
        public async Task OrderItemController_PostOrderItem_Ok()
        {
            //Arrange
            var db = _builder
                .ConfigureInMemory()
                .AddRandomOrderItem()
                .Build();
            Repository<Models.OrderItem> repository = new Repository<Models.OrderItem>(db);
            OrderItemController controller = new OrderItemController(repository);
            Models.OrderItem OrderItemToAdd = new Models.OrderItem();

            //Act
            await controller.PostOrderItem(OrderItemToAdd);
            var response = await repository.Read() as List<Models.OrderItem>;

            //Assert
            response.Count.Should().Be(2);
        }

        [Fact]
        public async Task OrderItemController_PutOrderItem_Ok()
        {
            //Arrange
            Models.OrderItem OrderItem = new Models.OrderItem { Id = 1, Quantity = 11 };
            var db = _builder
                .ConfigureInMemory()
                .AddSpecificOrderItem(OrderItem)
                .Build();
            Repository<Models.OrderItem> repository = new Repository<Models.OrderItem>(db);
            OrderItemController controller = new OrderItemController(repository);
            OrderItem.Quantity = 22;

            //Act
            await controller.PutOrderItem(OrderItem);
            var response = await repository.Read() as List<Models.OrderItem>;

            //Assert
            response[0].Quantity.Should().Be(22);
        }

        [Fact]
        public async Task OrderItemController_DeleteOrderItem_Ok()
        {
            //Arrange
            Models.OrderItem OrderItem = new Models.OrderItem { Id = 2 };
            var db = _builder
                .ConfigureInMemory()
                .AddSpecificOrderItem(OrderItem)
                .Build();
            Repository<Models.OrderItem> repository = new Repository<Models.OrderItem>(db);
            OrderItemController controller = new OrderItemController(repository);

            //Act
            await controller.DeleteOrderItem(2);
            var response = await repository.Read() as List<Models.OrderItem>;

            //Assert
            response.Count.Should().Be(0);
        }
    }
}
