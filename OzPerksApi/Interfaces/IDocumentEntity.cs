using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OzPerksApi.Interfaces
{
    public interface IDocumentEntity
    {
        string Id { get; set; }

        bool IsDeleted { get; set; }
    }
}
