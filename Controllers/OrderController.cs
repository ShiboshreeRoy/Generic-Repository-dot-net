using GenericRepository.Model;
using GenericRepository.Repository;
using GenericRepository.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenericRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IRepository<Order> _orderRepsitory;

        public OrderController(IRepository<Order> orderRepsitory)
        {
            _orderRepsitory = orderRepsitory;
        }

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            var Oerder = await _orderRepsitory.GetAllAsync();
            return Ok(Oerder);
        }

        [HttpGet("id")]

        public async Task<IActionResult> GetById(int id)
        {
            var Order = await _orderRepsitory.GetByIdAsync(id);
            return Ok(Order);
        }

        [HttpPost("Add Order")]

        public async Task<IActionResult> Post( [FromBody] Request.OrderRequest order)
        {
            var neworder = new Order()
            {
                OrderId = order.OrderId,
                ProductId = order.ProductId,
                OrderDate = order.OrderDate,

            };

            var createdProductResponse = await _orderRepsitory.AddAsync(neworder);
            return CreatedAtAction(nameof(GetById), new { id = createdProductResponse.ProductId }, createdProductResponse);

        }

        [HttpPut("id")]

        public async Task<IActionResult> Put(int id, [FromBody] Request.OrderRequest order)
        {
            var orders = await _orderRepsitory.GetByIdAsync(id);
            if(orders == null)
            {
                return NotFound();
            }
            order.ProductId = order.ProductId;
            order.OrderDate = order.OrderDate;
            await _orderRepsitory.UpdateAsync(orders);
            return Ok(orders);


        }

        [HttpDelete("id")]

        public async Task<IActionResult> Delete(int Id)
        {
            var order = await _orderRepsitory.GetByIdAsync(Id);
            if(order == null)
            {
                return NotFound();
            }
            await _orderRepsitory.DeleteAsync(order);
            return Ok(order);
        }
    }
}
