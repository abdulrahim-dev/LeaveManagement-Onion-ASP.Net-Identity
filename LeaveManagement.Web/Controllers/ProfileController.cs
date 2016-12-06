using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaveManagement.Web.Helper;

namespace LeaveManagement.Web.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Edit()
        {
            ViewBag.PageName = "Profile";
            return View();
        }
    }
}