using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum Statuses
    {
        UnKnown,
        Success = 200,
        NotExist = 404,
        Failed = 400,
        Forbidden = 403,
        Exception = 500,  
        Unauthorized = 401
    }
}
