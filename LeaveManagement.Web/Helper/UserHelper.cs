using System.Linq;
using System.Threading.Tasks;
using LeaveManagement.Core.DomainModels;
using LeaveManagement.Core.Identity;
using LeaveManagement.Core.Services;
using System.Security;
using System.Web;

namespace LeaveManagement.Web.Helper
{
    public class UserHelper
    {
        private  readonly IApplicationUserManager _userManager;
        private  readonly IService<UserProfile> _employeeService;
        public  string UserName
        {
            get
            {
                var userName = HttpContext.Current.User.Identity.Name;
                return userName ?? "";
            }
        }
        public UserHelper(IApplicationUserManager userManager, IService<UserProfile> employeeService)
        {
            _userManager = userManager;
            _employeeService = employeeService;
        }
        public  async Task<string> GetCurrentUser()
        {
            if (UserName != null)
            {
                var user = await _userManager.FindByNameAsync(UserName);
                var emp = _employeeService.GetAll().FirstOrDefault(x => x.UserId == user.Id);
                if (emp != null)  return emp.Name;
            }
            return "";
        }
    }
}