using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDB.Data.Entities
{
    public class Product
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("product_name"), BsonRepresentation(BsonType.String)]
        public string? ProductName { get; set; }

        [BsonElement("quantity"), BsonRepresentation(BsonType.String)]
        public string? Quantity { get; set; }

        [BsonElement("price"), BsonRepresentation(BsonType.String)]
        public string? Price { get; set; }

        [BsonElement("description"), BsonRepresentation(BsonType.String)]
        public string? Description { get; set; }

        [BsonElement("category"), BsonRepresentation(BsonType.String)]
        public string? Category { get; set; }

        [BsonElement("created_at"), BsonRepresentation(BsonType.DateTime)]
        public DateTime? CreatedAt { get; set; }

        [BsonElement("updated_at"), BsonRepresentation(BsonType.DateTime)]
        public DateTime? UpdatedAt { get; set; }

        [BsonElement("is_active"), BsonRepresentation(BsonType.Boolean)]
        public bool IsActive { get; set; }
    }
}
