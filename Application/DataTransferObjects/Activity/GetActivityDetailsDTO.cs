using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransferObjects.Activity
{
    public class GetActivityDetailsDTO
    {
        public List<GetActivitiesDTO> Activities { get; set; }
        public List<BasicListDTO> Cities { get; set; }
        public List<BasicListDTO> Categories { get; set; }
    }
}
