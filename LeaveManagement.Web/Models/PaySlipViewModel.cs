using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LeaveManagement.Core.DomainModels;
using LeaveManagement.Core.DomainModels.AdminProfile;
using LeaveManagement.Core.DomainModels.Identity;

namespace LeaveManagement.Web.Models
{
    public class PaySlipViewModel
    {
        public List<Users> Users { get; set; }

        [Required]
        public int UserId { get; set; }

        public List<Year> Years { get; set; }

        [Required]
        public int YearId { get; set; }

        public List<Month> Months { get; set; }

        [Required]
        public int MonthId { get; set; }

        public PaySlip PaySlip { get; set; }

        public HttpPostedFileBase File { get; set; }
    }

    public class PaySlipListViewModel
    {
        public int PaySlipId { get; set; }

        public string Month { get; set; }

        public string Year { get; set; }
    }
}