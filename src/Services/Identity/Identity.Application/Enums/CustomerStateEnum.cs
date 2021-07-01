using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Enums
{
    public enum CustomerStateEnum : short
    {
        Created = 1,
        Activated = 2,
        Blocked = 3,
        Deleted = 4
    }
}
