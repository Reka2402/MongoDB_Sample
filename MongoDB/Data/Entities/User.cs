using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDB.Data.Entities
{
    public class User
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("user_name"), BsonRepresentation(BsonType.String)]
        public string? UserName { get; set; }

        [BsonElement("email"), BsonRepresentation(BsonType.String)]
        public string? Email { get; set; }

        [BsonElement("address"), BsonRepresentation(BsonType.String)]
        public string? Address { get; set; }

        [BsonElement("phone_number"), BsonRepresentation(BsonType.String)]
        public string? PhoneNumber { get; set; }

        [BsonElement("date_of_birth"), BsonRepresentation(BsonType.DateTime)]
        public DateTime? DateOfBirth { get; set; }

        [BsonElement("is_active"), BsonRepresentation(BsonType.Boolean)]
        public bool IsActive { get; set; }

        [BsonElement("created_at"), BsonRepresentation(BsonType.DateTime)]
        public DateTime? CreatedAt { get; set; }

        [BsonElement("updated_at"), BsonRepresentation(BsonType.DateTime)]
        public DateTime? UpdatedAt { get; set; }

        [BsonElement("role"), BsonRepresentation(BsonType.String)]
        public string? Role { get; set; }  
    }
}
