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

        public HomeController(IApplicationUserManager userManager, IService<EmployeeDetails> employeeService)
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
        public ActionResult Index()
        {
            if (Session == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var user = _userManager.FindByName(UserName);
            if (user != null)
            {
                var emp = _employeeService.GetAll().FirstOrDefault(x => x.UserId == user.Id);
                Session["LEAVEPORTAL.AUTH"] = user.Id;
                if (emp != null) Session["Name"] = emp.Name;
            }
            ViewBag.PageName = "Home";
            return View();
        }
    }
}