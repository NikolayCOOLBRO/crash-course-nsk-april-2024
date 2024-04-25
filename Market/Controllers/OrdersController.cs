using Market.DAL.Interfaces;
using Market.DAL.Repositories;
using Market.DTO;
using Market.Misc;
using Market.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [Route("v1/api/orders")]
    [ApiController]
    public sealed class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _repository;

        public OrdersController(IOrderRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchOrder([FromBody] SerachOrderDto getOrderDto)
        {
            var result = await _repository.GetOrdersBySeller(getOrderDto.SellerId, getOrderDto.StatusOrder);

            return DbResultHelper.DbResultIsSuccessful(result, out var error)
                ? new JsonResult(result)
                : error;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
        {
            var order = new Order()
            {
                Id = Guid.NewGuid(),
                CustomerId = dto.CustomerId,
                ProductId = dto.ProductId,
                SeiledId = dto.SeildId,
                Status = StatusOrder.Created
            };

            var result = await _repository.CreateOrderAsync(order);

            return DbResultHelper.DbResultIsSuccessful(result, out var error)
                ? Ok()
                : error;
        }

        [HttpPatch("set-status")]
        public async Task<IActionResult> SetStatusOrder([FromBody] SetStatusOrderDto dto)
        {
            var result = await _repository.ChangeOrderStatusAsync(dto.OrderId, dto.StatusOrder);

            return DbResultHelper.DbResultIsSuccessful(result, out var error)
                ? NoContent()
                : error;
        }
    }
}
