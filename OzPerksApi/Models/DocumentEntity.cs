using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OzPerksApi.Interfaces;

namespace OzPerksApi.Models
{
    public abstract class DocumentEntity : IDocumentEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }= string.Empty;
        public bool IsDeleted { get; set; } = false;
    }
}
