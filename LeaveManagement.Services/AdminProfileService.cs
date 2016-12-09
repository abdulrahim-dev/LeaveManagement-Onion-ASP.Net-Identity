using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Core.Data;
using LeaveManagement.Core.DomainModels;
using LeaveManagement.Core.DomainModels.AdminProfile;
using LeaveManagement.Core.Interfaces;
using LeaveManagement.Core.Services;

namespace LeaveManagement.Services
{
    public class AdminProfileService:IAdminProfileService
    {
        private readonly IAdminProfileRepository _adminProfile;
        private readonly IRepository<UserProfile> _repository;
        public IUnitOfWork UnitOfWork { get; private set; }
        public AdminProfileService(IAdminProfileRepository adminProfile, IRepository<UserProfile> repository, IUnitOfWork unitOfWork)
        {
            _adminProfile = adminProfile;
            _repository = repository;
            UnitOfWork = unitOfWork;
        }

        public List<ProfileViewModel> GetList()
        {
           return _adminProfile.GetList();
        }
        public ProfileViewModel GetUserById(int id)
        {
            return _adminProfile.GetUserById(id);
        }

        public bool UpdateUSer(ProfileViewModel profileViewModel)
        {
            var user = _adminProfile.GetUserProfileById(profileViewModel.UserId);
            if (user != null)
            {
                user.Name = profileViewModel.Name;
                _repository.Update(user);
                UnitOfWork.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
