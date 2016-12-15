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
        ProfileViewModelList GetList(string userName, int pageIndex, int pageSize);
        List<Users> GetUsers();
        ProfileViewModel GetUserById(int id);

        bool UpdateUSer(ProfileViewModel profileViewModel);
    }
}
