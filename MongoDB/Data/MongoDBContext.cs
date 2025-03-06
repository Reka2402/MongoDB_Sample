using MongoDB.Driver;

namespace MongoDB.Data
{
    public class MongoDBContext
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase? _database;
        public MongoDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = _configuration.GetConnectionString("DbConnection");
            var mongoURL =  MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(mongoURL); 
            _database=mongoClient.GetDatabase(mongoURL.DatabaseName);
        }
        public IMongoDatabase? Database => _database;
    }
}
