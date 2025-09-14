using System.Collections.ObjectModel;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        public required string Name { get; set; }
        public  Collection<Activity>? Activities { get; set; }

    }
}
