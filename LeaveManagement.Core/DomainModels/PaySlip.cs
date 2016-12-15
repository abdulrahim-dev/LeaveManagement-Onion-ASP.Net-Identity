using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Core.DomainModels
{
    public class PaySlip:BaseEntity
    {
        public int Month { get; set; }

        public int Year { get; set; }

        public int UserId { get; set; }

        public string SavedPath { get; set; }

        [ForeignKey("UserId")]
        public ApplicationIdentityUser ApplicationIdentityUser { get; set; }

    }
}
