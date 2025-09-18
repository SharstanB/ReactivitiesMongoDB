using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities;
public class Activity : BaseEntity
{
    [BsonRepresentation(BsonType.String)]
    public required string Description { get; set; }


    [BsonRepresentation(BsonType.DateTime)]
    public  DateTime Date {get; set;}

    
    [BsonRepresentation(BsonType.String)]
    public required string Title {get; set;}


    [BsonRepresentation(BsonType.Boolean)]
    public bool IsCancelled {get; set;}


    [BsonRepresentation(BsonType.String)]
    public  string Venue {get; set;} = string.Empty;


    [BsonRepresentation(BsonType.String)]
    public string City { get; set; }


    [BsonRepresentation(BsonType.Double)]
    public double Latitude {get; set;}


    [BsonRepresentation(BsonType.Double)]
    public double Longitude { get; set;}

    public  Category Category  { get; set; }

  
}