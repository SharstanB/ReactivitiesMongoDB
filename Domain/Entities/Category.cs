using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        [BsonRepresentation(BsonType.String)]
        public required string Name { get; set; }

    }
}
