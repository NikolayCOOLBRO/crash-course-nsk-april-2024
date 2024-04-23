using Market.DAL.Repositories;
using Market.DTO;
using Market.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [ApiController]
    [Route("v1/customers/{customerId:Guid}/cart")]
    public class CartsController : ControllerBase
    {
        private CartsRepository _cartsRepository { get; }

        public CartsController()
        {
            _cartsRepository = new CartsRepository();
        }

        [HttpGet]
        public async Task<IActionResult> GetCartsById([FromRoute] Guid customerId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProductAsync([FromRoute] Guid customerId, [FromBody] CartAddProductDto product)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("remove-product")]
        public async Task<IActionResult> RemoveAsync([FromRoute] Guid customerId, [FromBody] CartRemoveProductDto product)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearAllAsync([FromRoute] Guid customerId)
        {
            throw new NotImplementedException();
        }
    }
}
