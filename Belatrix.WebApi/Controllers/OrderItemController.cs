using System.Collections.Generic;
using System.Threading.Tasks;
using Belatrix.WebApi.Models;
using Belatrix.WebApi.Repository;
using Microsoft.AspNetCore.Mvc;


namespace Belatrix.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IRepository<OrderItem> _repository;
        public OrderItemController(IRepository<OrderItem> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItems()
        {
            return Ok(await _repository.Read());
        }

        [HttpPost]
        public async Task<ActionResult<OrderItem>> PostOrderItem(OrderItem order)
        {
            await _repository.Create(order);
            return Ok(order.Id);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> PutOrderItem(OrderItem order)
        {
            return Ok(await _repository.Update(order));
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteOrderItem(int orderId)
        {
            return Ok(await _repository.Delete(new OrderItem { Id = orderId }));
        }
    }
}