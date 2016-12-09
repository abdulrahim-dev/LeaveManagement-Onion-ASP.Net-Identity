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

        
        public List<ProfileViewModel> GetList()
        {
            var listOfUsers = (_context.Users.Join(_context.Employee,
                user => user.Id, userprofile => userprofile.UserId,
                (user, userprofile) => new { UserId = user.Id, Name = user.UserName, UserName = userprofile.Name }));


            //(from user in _context.Users
            // join
            //     userprofile in _context.Employee on user.Id equals userprofile.UserId
            // select new { UserId = user.Id, Name = user.UserName, UserName = userprofile.Name }).ToList()

            return listOfUsers.Select(item => new ProfileViewModel() { Name = item.Name, UserId = item.UserId, UserName = item.UserName }).ToList();
        }
        public ProfileViewModel GetUserById(int id)
        {
            //var listOfUsers = (from user in _context.Users
            //                   where user.Id == id
            //                   join
            //                   userprofile in _context.Employee on user.Id equals userprofile.UserId
            //                   select new { UserId = user.Id, Name = user.UserName, UserName = userprofile.Name }).FirstOrDefault();
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
            return _context.Employee.FirstOrDefault(x => x.Id == id);
        }

      


    }
}
