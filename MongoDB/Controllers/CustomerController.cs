using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Data;
using MongoDB.Data.Entities;
using MongoDB.Driver;

namespace MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMongoCollection<Customer> _customers;
        public CustomerController(MongoDBService mongoDBService)
        {
            _customers = mongoDBService.Database?.GetCollection<Customer>("customer");
        }
        [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            return await _customers.Find(FilterDefinition<Customer>.Empty).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer?>> GetById(string id)
        {
            var filter = Builders<Customer>.Filter.Eq(x => x.Id,id);
            var customer = _customers.Find(filter).FirstOrDefault();
            return customer is not null ? Ok(customer) : NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> Post(Customer customer)
        {
            await _customers.InsertOneAsync(customer);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }
        [HttpPut]
        public async Task<ActionResult> Update(Customer customer)
        {
            var filter = Builders<Customer>.Filter.Eq(x => x.Id, customer.Id);
            var update = Builders<Customer>.Update
                .Set(x => x.CustomerName, customer.CustomerName)
                .Set(x => x.Email, customer.Email);
            await _customers.UpdateOneAsync(filter, update);
            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var filter = Builders<Customer>.Filter.Eq(x => x.Id, id);
            await _customers.DeleteOneAsync(filter);
            return Ok();
        }
    }
}