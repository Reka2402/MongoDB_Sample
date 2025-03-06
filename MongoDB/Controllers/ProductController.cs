using MongoDB.Driver;
using MongoDB.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Data;

namespace MongoDB.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMongoCollection<Product> _productCollection;

        public ProductController(MongoDBContext mongoDBService)
        {
            _productCollection = mongoDBService.Database?.GetCollection<Product>("Products");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productCollection.Find(_ => true).ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var product = await _productCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            await _productCollection.InsertOneAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] Product product)
        {
            var result = await _productCollection.ReplaceOneAsync(p => p.Id == id, product);
            if (result.MatchedCount == 0)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var result = await _productCollection.DeleteOneAsync(p => p.Id == id);
            if (result.DeletedCount == 0)
                return NotFound();
            return NoContent();
        }
    }
}
