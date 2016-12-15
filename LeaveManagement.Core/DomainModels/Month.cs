using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Core.DomainModels
{
    public class Month:BaseEntity
    {
        public string Name { get; set; }
    }

    public class Year : BaseEntity
    {
        public string Name { get; set; }
    }
}
