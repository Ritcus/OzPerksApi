using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace OzPerksApi.Models
{
    public class Admin : DocumentEntity
    {
        [BsonElement("fullname")]
        public string FullName { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

    }
}
