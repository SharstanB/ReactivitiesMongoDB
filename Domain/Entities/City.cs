using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
     public class City : BaseEntity
    {

        public required string CityName { get; set; }

        public Collection<Activity>? Activities { get; set; }


    }
}
