using MongoDB.Driver;
using MongoDB.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Data;

namespace MongoDB.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserController(MongoDBContext mongoDBService)
        {
            _userCollection = mongoDBService.Database?.GetCollection<User>("Users");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userCollection.Find(_ => true).ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            await _userCollection.InsertOneAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] User user)
        {
            var result = await _userCollection.ReplaceOneAsync(u => u.Id == id, user);
            if (result.MatchedCount == 0)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userCollection.DeleteOneAsync(u => u.Id == id);
            if (result.DeletedCount == 0)
                return NotFound();
            return NoContent();
        }
    }
}
