using System.Collections.Generic;
using System.Threading.Tasks;
using Belatrix.WebApi.Models;
using Belatrix.WebApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Belatrix.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IRepository<Supplier> _repository;
        public SupplierController(IRepository<Supplier> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
        {
            return Ok(await _repository.Read());
        }

        [HttpPost]
        public async Task<ActionResult<Supplier>> PostSupplier(Supplier supplier)
        {
            await _repository.Create(supplier);
            return Ok(supplier.Id);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> PutSupplier(Supplier supplier)
        {
            return Ok(await _repository.Update(supplier));
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteSupplier(int supplierId)
        {
            return Ok(await _repository.Delete(new Supplier { Id = supplierId }));
        }
    }
}