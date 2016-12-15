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

        [EmailAddress(ErrorMessage = "Please Enter a valid email Adrdress!!")]
        public string UserName { get; set; }

    }

    public class ProfileViewModelList
    {
        public IEnumerable<ProfileViewModel> ProfileViewModels;
        public int TotalCount;
    }

    public class Users
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }
        
    }
}