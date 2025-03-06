using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDB.Data.Entities
{
    public class Order
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("order_date"), BsonRepresentation(BsonType.String)]
        public string? OrderDate { get; set; }

        [BsonElement("total_amount"), BsonRepresentation(BsonType.String)]
        public string? TotalAmount { get; set; }

        [BsonElement("user_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }

        [BsonElement("products")]
        public List<Product>? Products { get; set; }
    }
}
