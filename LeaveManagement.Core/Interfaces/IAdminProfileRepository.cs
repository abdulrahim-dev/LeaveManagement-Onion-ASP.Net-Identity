using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Core.DomainModels;
using LeaveManagement.Core.DomainModels.AdminProfile;

namespace LeaveManagement.Core.Interfaces
{
    public interface IAdminProfileRepository
    {
        List<ProfileViewModel> GetList();
        ProfileViewModel GetUserById(int id);
        UserProfile GetUserProfileById(int id);
    }
}
