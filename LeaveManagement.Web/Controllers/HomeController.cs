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

        public HomeController(IApplicationUserManager userManager)
        {
            _userManager = userManager;
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
    }
}