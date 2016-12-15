using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LeaveManagement.Core.DomainModels;
using LeaveManagement.Core.DomainModels.AdminProfile;
using LeaveManagement.Core.DomainModels.Identity;
using LeaveManagement.Core.Identity;
using LeaveManagement.Core.Services;
using LeaveManagement.Web.Models;

namespace LeaveManagement.Web.Areas.Admin.Controllers
{
    public class AdminProfileController : Controller
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationRoleManager _roleManager;
        private readonly IService<UserProfile> _employeeService;
        private readonly IAdminProfileService _adminProfileService;

        public AdminProfileController(IApplicationUserManager userManager, IApplicationRoleManager roleManager, IService<UserProfile> employeeService,
            IAdminProfileService adminProfileService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _employeeService = employeeService;
            _adminProfileService = adminProfileService;
        }

        public string UserName
        {
            get
            {
                var userName = User.Identity.Name;
                return userName ?? "";
            }
        }
        //
        // GET: /Account/Register
        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            ViewBag.PageName = "Profile";
            model.ApplicationRoles = _roleManager.GetRoles().ToList();
            return View(model);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var user = new AppUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var currentuser = await _userManager.FindByNameAsync(model.Email);
                    if (currentuser != null)
                    {
                        await _userManager.AddUserToRolesAsync(currentuser.Id, new List<string> { model.UserRole });
                        UserProfile emp = new UserProfile
                        {
                            Name = model.Name,
                            UserId = currentuser.Id
                        };
                        await _employeeService.AddAsync(emp);
                    }
                    return RedirectToAction("Index", "Admin");


                }
            }
            return View(model);
        }

        public ActionResult Users()
        {
            int pageIndex=0;
            int pageSize = ConfigurationManager.AppSettings["PageSize"]!=null?Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]):5;
            pageIndex = Request.QueryString["page"] != null ? Convert.ToInt32(Request.QueryString["page"]) : 1;
            var model = _adminProfileService.GetList(UserName, pageIndex, pageSize);
            ViewBag.PageName = "Users";
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var model = _adminProfileService.GetUserById(id);
            ViewBag.PageName = "Users";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _adminProfileService.UpdateUSer(model);
                if (result)
                {
                    ViewBag.PageName = "Users";
                    return RedirectToAction("Users");
                }
            }
            return View(model);
        }

    }
}