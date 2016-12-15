using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagement.Core.Data;
using LeaveManagement.Core.DomainModels;
using LeaveManagement.Core.DomainModels.AdminProfile;
using LeaveManagement.Core.Interfaces;

namespace LeaveManagement.Data.Implementations
{
    public class AdminProfileRepository : IAdminProfileRepository
    {
        readonly LeaveContext _context = new LeaveContext();

        
        public ProfileViewModelList GetList(string userName, int pageIndex, int pageSize)
        {
            var listOfUsers = (_context.Users.Where(x=>x.UserName!=userName).Join(_context.Employee,
                user => user.Id, userprofile => userprofile.UserId,
                (user, userprofile) => new { UserId = user.Id, Name = userprofile.Name, UserName = user.UserName }));

            int totalCount = listOfUsers.Count();
            int pages = pageSize*(pageIndex - 1);
            listOfUsers = listOfUsers.OrderBy(x=>x.Name).Skip(pages).Take(pageSize);
            ProfileViewModelList model=new ProfileViewModelList()
            {
                ProfileViewModels = listOfUsers.Select(item => new ProfileViewModel() { Name = item.Name, UserId = item.UserId, UserName = item.UserName }).ToList(),
                TotalCount = totalCount
            };
            return model;
        }

        public List<Users> GetUsers()
        {
            var listOfUsers = (_context.Users.Join(_context.Employee,
                user => user.Id, userprofile => userprofile.UserId,
                (user, userprofile) => new { UserId = user.Id, Name = userprofile.Name })).OrderBy(x=>x.Name);
           
            return listOfUsers.Select(item => new Users() { Name = item.Name, UserId = item.UserId }).ToList();
        }


        public ProfileViewModel GetUserById(int id)
        {
            var listOfUsers = (_context.Users.Where(user => user.Id == id)
                   .Join(_context.Employee, user => user.Id, userprofile => userprofile.UserId,
                    (user, userprofile) => new {UserId = user.Id, UserName = user.UserName, Name = userprofile.Name})).FirstOrDefault();
            if (listOfUsers != null)
            {
                ProfileViewModel profleModel=new ProfileViewModel() {Name=listOfUsers.Name,UserId=id,UserName = listOfUsers.UserName};

                return profleModel;
            }
            return null;
        }

        public UserProfile GetUserProfileById(int id)
        {
            return _context.Employee.FirstOrDefault(x => x.UserId == id);
        }

      


    }
}
