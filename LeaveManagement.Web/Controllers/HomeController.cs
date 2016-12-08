using System.Linq;
using System.Web.Mvc;
using LeaveManagement.Core.DomainModels;
using LeaveManagement.Core.Identity;
using LeaveManagement.Core.Services;
using LeaveManagement.Web.Models;

namespace LeaveManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IService<UserProfile> _employeeService;

        public HomeController(IApplicationUserManager userManager, IService<UserProfile> employeeService)
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
            ViewBag.PageName = "Home";
            return View();
        }

        public PartialViewResult SideMenu(string pageName)
        {
            LayoutViewModel layoutViewModel=new LayoutViewModel();
            var user = _userManager.FindByName(UserName);
            if (user != null)
            {
                var emp = _employeeService.GetAll().FirstOrDefault(x => x.UserId == user.Id);
                if (emp != null)
                {
                    layoutViewModel.Name = emp.Name;
                    layoutViewModel.PageName = pageName;
                }
            }
            return PartialView("SideMenu", layoutViewModel);
        }
    }
}