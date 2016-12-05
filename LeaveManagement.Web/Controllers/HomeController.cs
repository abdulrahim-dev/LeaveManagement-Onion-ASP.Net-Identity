using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LeaveManagement.Core.DomainModels;
using LeaveManagement.Core.Identity;
using LeaveManagement.Core.Services;

namespace LeaveManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IService<EmployeeDetails> _employeeService;

        public HomeController(IApplicationUserManager userManager,IService<EmployeeDetails> employeeService)
        {
            _userManager = userManager;
            _employeeService = employeeService;
        }

        public string UserName
        {
            get
            {
                var userId = User.Identity.Name;
                return userId ?? "";
            }
        }

        // GET: Home
        [Authorize]
        public async Task<ActionResult> Index()
        {
            if (UserName != null)
            {
                var user = await _userManager.FindByNameAsync(UserName);
                var emp = _employeeService.GetAll().FirstOrDefault(x => x.UserId == user.Id);
                if (emp != null) ViewBag.Name = emp.Name;
            }
            return View();
        }
    }
}