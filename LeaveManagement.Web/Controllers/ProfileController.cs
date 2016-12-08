using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Cache;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LeaveManagement.Core.DomainModels;
using LeaveManagement.Core.Identity;
using LeaveManagement.Core.Services;
using LeaveManagement.Web.Helper;

namespace LeaveManagement.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IService<UserProfile> _employeeService;

        public ProfileController(IApplicationUserManager userManager, IService<UserProfile> employeeService)
        {
            _userManager = userManager;
            _employeeService = employeeService;
        }

        public string UserName
        {
            get
            {
                var userName = User.Identity.Name;
                return userName ?? "";
            }
        }
        // GET: Profile
        public ActionResult Edit()
        {
            ViewBag.PageName = "Profile";
            var user = _userManager.FindByName(UserName);
            if (user != null)
            {
                var userId = user.Id;
                if (userId != 0)
                {
                    var model = _employeeService.GetAll().FirstOrDefault(x => x.UserId == userId);
                    return View(model);
                }
            }
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserProfile model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeService.GetAll().FirstOrDefault(x => x.UserId == model.UserId);
                if (employee != null)
                {
                    employee.Name = model.Name;
                    await _employeeService.UpdateAsync(employee);
                }
                return RedirectToAction("Edit");
            }
            ModelState.AddModelError("", "Please Try again.");
            return View(model);
        }
    }
}