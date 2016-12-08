using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaveManagement.Core.DomainModels;
using LeaveManagement.Core.Identity;
using LeaveManagement.Core.Services;
using LeaveManagement.Web.Models;

namespace LeaveManagement.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IService<EmployeeDetails> _employeeService;

        public AdminController(IApplicationUserManager userManager, IService<EmployeeDetails> employeeService)
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

        public PartialViewResult AdminSideMenu(string pageName)
        {
            LayoutViewModel layoutViewModel = new LayoutViewModel();
            var user = _userManager.FindByName(UserName);
            if (user != null)
            {
                var emp = _employeeService.GetAll().FirstOrDefault(x => x.UserId == user.Id);
                if (emp != null)
                {
                    layoutViewModel.Name = emp.Name;
                    layoutViewModel.ProfilePath = emp.ProfilePicturePath;
                    layoutViewModel.PageName = pageName;
                }
            }
            return PartialView("AdminSideMenu", layoutViewModel);
        }
    }
}