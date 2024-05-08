using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace OzPerksApi.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("fullname")]
        [JsonPropertyName("fullname")]
        public string FullName { get; set; } = null;

        [BsonElement("email")]
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("role")]
        [JsonPropertyName("role")]
        public string? Role { get; set; } = string.Empty;

        [BsonElement("phone")]
        [JsonPropertyName("phone")]
        public string? Phone { get; set; } = string.Empty;
    }
}
