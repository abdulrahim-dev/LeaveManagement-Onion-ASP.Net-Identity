using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Core.DomainModels.AdminProfile;

namespace LeaveManagement.Core.Services
{
    public interface IAdminProfileService
    {
        List<ProfileViewModel> GetList();
        ProfileViewModel GetUserById(int id);

        bool UpdateUSer(ProfileViewModel profileViewModel);
    }
}
