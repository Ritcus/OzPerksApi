using MongoDB.Bson.Serialization.Attributes;
using static OzPerksApi.Models.Enum.Enums;

namespace OzPerksApi.Models
{
    public class Post : DocumentEntity
    {
        [BsonElement("title")]
        public string Title { get; set; } = string.Empty;

        [BsonElement("body")]
        public string Body { get; set; } = string.Empty;

        [BsonElement("IsActive")]
        public bool IsActive { get; set; } = true;

        [BsonElement("image")]
        public byte[]? Image { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [BsonElement("lastUpdatedAt")]
        public DateTime? LastUpdatedAt { get; set; }

        [BsonElement("type")]
        public PostType Type { get; set; }
    }
}
