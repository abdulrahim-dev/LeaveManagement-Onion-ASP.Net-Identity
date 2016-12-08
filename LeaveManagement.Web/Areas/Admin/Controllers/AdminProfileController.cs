using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LeaveManagement.Core.DomainModels;
using LeaveManagement.Core.Identity;
using LeaveManagement.Core.Services;

namespace LeaveManagement.Web.Areas.Admin.Controllers
{
    public class AdminProfileController : Controller
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IService<EmployeeDetails> _employeeService;

        public AdminProfileController(IApplicationUserManager userManager, IService<EmployeeDetails> employeeService)
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
        public async Task<ActionResult> Edit(EmployeeDetails model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeService.GetAll().FirstOrDefault(x => x.UserId == model.UserId);
                if (employee != null)
                {
                    var fileName = "";
                    if (file != null && file.ContentLength > 0)
                    {
                        if (!string.IsNullOrEmpty(employee.ProfilePicturePath))
                        {
                            if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/ProfilePictures/" + employee.ProfilePicturePath)))
                            {
                                System.IO.File.SetAttributes(System.Web.HttpContext.Current.Server.MapPath("~/ProfilePictures/" + employee.ProfilePicturePath), FileAttributes.Normal);
                                System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath("~/ProfilePictures/" + employee.ProfilePicturePath));
                            }
                        }
                        fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/ProfilePictures/"), fileName);
                        file.SaveAs(path);
                    }
                    employee.ProfilePicturePath = file != null && file.ContentLength > 0 ? fileName : employee.ProfilePicturePath;
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