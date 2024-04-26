using Market.DAL.Interfaces;
using Market.DAL.Repositories;
using Market.DTO;
using Market.Misc;
using Market.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [ApiController]
    [Route("v1/customers/{customerId:Guid}/cart")]
    public class CartsController : ControllerBase
    {
        private readonly ICartsRepository _cartsRepository;

        public CartsController(ICartsRepository repository)
        {
            _cartsRepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCartsById([FromRoute] Guid customerId)
        {
            var cartResult = await _cartsRepository.GetCartByIdAsync(customerId);

            return DbResultHelper.DbResultIsSuccessful(cartResult, out var error)
                ? new JsonResult(cartResult.Result)
                : error;
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProductAsync([FromRoute] Guid customerId, [FromBody] CartAddProductDto product)
        {
            var result = await _cartsRepository.AddProductToCartAsync(customerId, product.IdProduct);

            return DbResultHelper.DbResultIsSuccessful(result, out var error)
                ? Ok() 
                : error;
        }

        [HttpDelete("remove-product")]
        public async Task<IActionResult> RemoveAsync([FromRoute] Guid customerId, [FromBody] CartRemoveProductDto product)
        {
            var result = await _cartsRepository.RemoveProductFromCartAsync(customerId, product.IdProduct);

            return DbResultHelper.DbResultIsSuccessful(result, out var error)
                ? Ok()
                : error;
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearAllAsync([FromRoute] Guid customerId)
        {
            var result = await _cartsRepository.CartClearAllAsync(customerId);

            return DbResultHelper.DbResultIsSuccessful(result, out var error)
                ? Ok()
                : error;
        }
    }
}
