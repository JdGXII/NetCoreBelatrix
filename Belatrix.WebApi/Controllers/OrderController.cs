using System.Collections.Generic;
using System.Threading.Tasks;
using Belatrix.WebApi.Models;
using Belatrix.WebApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Belatrix.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IRepository<Order> _repository;
        public OrderController(IRepository<Order> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return Ok(await _repository.Read());
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            await _repository.Create(order);
            return Ok(order.Id);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> PutOrder(Order order)
        {
            return Ok(await _repository.Update(order));
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteOrder(int orderId)
        {
            return Ok(await _repository.Delete(new Order { Id = orderId }));
        }
    }
}