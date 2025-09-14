using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;
public class Activity : BaseEntity
{

     public required string Description { get; set; }

     public  DateTime Date {get; set;}
    
     [Required]
     public required string Title {get; set;}

     public bool IsCancelled {get; set;}

    [Required]
    public  string Venue {get; set;} = string.Empty;

    [Required]
    public string City { get; set; }

    public double Latitude {get; set;}

    public double Longitude { get; set;}

    public  Category Category  { get; set; }

    [Required]
    public  required Guid CategoryId { get; set; }
}