using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Cache;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LeaveManagement.Core.DomainModels;
using LeaveManagement.Core.Services;
using LeaveManagement.Web.Helper;

namespace LeaveManagement.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IService<EmployeeDetails> _employeeService;

        public ProfileController(IService<EmployeeDetails> employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: Profile
        public ActionResult Edit()
        {
            ViewBag.PageName = "Profile";
            var userId = Session["LEAVEPORTAL.AUTH"] != null ? Convert.ToInt32(Session["LEAVEPORTAL.AUTH"]) : 0;
            if (userId != 0)
            {
                var model = _employeeService.GetAll().FirstOrDefault(x => x.UserId == userId);
                return View(model);
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
                            if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/ProfilePictures/"+employee.ProfilePicturePath)))
                            {
                                System.IO.File.SetAttributes(System.Web.HttpContext.Current.Server.MapPath("~/ProfilePictures/" + employee.ProfilePicturePath), FileAttributes.Normal);
                                System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath("~/ProfilePictures/" + employee.ProfilePicturePath));
                            }
                        }
                        fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/ProfilePictures/"), fileName);
                        file.SaveAs(path);
                    }
                    employee.ProfilePicturePath = file != null && file.ContentLength > 0?fileName:employee.ProfilePicturePath;
                    employee.Name = model.Name;
                    await _employeeService.UpdateAsync(employee);
                    Session["Name"] = employee.Name;
                }
                return RedirectToAction("Edit");
            }
            ModelState.AddModelError("", "Please Try again.");
            return View(model);
        }
    }
}