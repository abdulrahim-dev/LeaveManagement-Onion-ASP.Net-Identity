using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Core.DomainModels.AdminProfile
{
    public class ProfileViewModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Please Enter a valid email Adrdress!!")]
        public string UserName { get; set; }

    }
}
