using MongoDB.Driver;
using MongoDB.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Data;

namespace MongoDB.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMongoCollection<Order> _orderCollection;

        public OrderController(MongoDBContext mongoDBService)
        {
            _orderCollection = mongoDBService.Database?.GetCollection<Order>("Orders");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderCollection.Find(_ => true).ToListAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(string id)
        {
            var order = await _orderCollection.Find(o => o.Id == id).FirstOrDefaultAsync();
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            await _orderCollection.InsertOneAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(string id, [FromBody] Order order)
        {
            var result = await _orderCollection.ReplaceOneAsync(o => o.Id == id, order);
            if (result.MatchedCount == 0)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            var result = await _orderCollection.DeleteOneAsync(o => o.Id == id);
            if (result.DeletedCount == 0)
                return NotFound();
            return NoContent();
        }
    }
}
