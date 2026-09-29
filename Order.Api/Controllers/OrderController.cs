using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Application.DTOs;
using Order.Application.Service;

namespace Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
        {
            var orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create([FromBody] OrderCreateDto dto)
        {
            try
            {
                var createdOrder = await _orderService.CreateOrderAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdOrder.Id }, createdOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
