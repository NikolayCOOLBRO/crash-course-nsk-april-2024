using Market.DAL.Repositories;
using Market.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [Route("v1/api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        public OrderRepository _orderRepository;

        public OrdersController()
        {
            _orderRepository = new OrderRepository();
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchOrder([FromBody] SerachOrderDto getOrderDto)
        {
            throw new NotImplementedException();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("set-status")]
        public async Task<IActionResult> SetStatusOrder([FromBody] SetStatusOrderDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
