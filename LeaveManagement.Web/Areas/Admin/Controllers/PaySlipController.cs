using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LeaveManagement.Core.Data;
using LeaveManagement.Core.DomainModels;
using LeaveManagement.Core.Services;
using LeaveManagement.Web.Models;

namespace LeaveManagement.Web.Areas.Admin.Controllers
{
    public class PaySlipController : Controller
    {
        private readonly IAdminProfileService _adminProfileService;

        private readonly IService<Month> _monthService;
        private readonly IService<Year> _yearService;
        private readonly IService<PaySlip> _payslipService;
        private readonly IUnitOfWork UnitOfWork;

        public PaySlipController(IAdminProfileService adminProfileService, IService<Month> monthService, IService<Year> yearService, IService<PaySlip> payslipService, IUnitOfWork unitOfWork)
        {
            _adminProfileService = adminProfileService;
            _monthService = monthService;
            _yearService = yearService;
            _payslipService = payslipService;
            UnitOfWork = unitOfWork;
        }

        // GET: Admin/PaySlip
        public ActionResult List()
        {
            ViewBag.PageName = "PaySlip";
            PaySlipViewModel model = new PaySlipViewModel { Users = _adminProfileService.GetUsers(), Months = _monthService.GetAll(), Years = _yearService.GetAll() };
            return View(model);
        }

        [HttpPost]
        public PartialViewResult GetPaySlipList(PaySlipViewModel model)
        {
            IList<PaySlipListViewModel> modelList=new List<PaySlipListViewModel>();
            modelList.Add(new PaySlipListViewModel() {PaySlipId=1,Month = "Jan",Year="1998"});
            return PartialView("_PaySlipList", modelList);
        }

        public ActionResult New()
        {
            ViewBag.PageName = "PaySlipAdd";
            PaySlipViewModel model = new PaySlipViewModel {Users = _adminProfileService.GetUsers(),Months=_monthService.GetAll(),Years=_yearService.GetAll()};
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> New(PaySlipViewModel model)
        {
            if (ModelState.IsValid)
            {
                var fileName = "";
                if (model.File != null && model.File.ContentLength > 0)
                {
                    fileName = Path.GetFileName(model.File.FileName);
                    if (fileName != null)
                    {
                        var path = Path.Combine(Server.MapPath("~/PaySlip/"), fileName);
                        model.File.SaveAs(path);
                    }
                }
                var paylSlip = new PaySlip()
                {
                    UserId = model.UserId,
                    Month = model.MonthId,
                    Year = model.YearId,
                    SavedPath = fileName
                };
               await _payslipService.AddAsync(paylSlip);
               await UnitOfWork.SaveChangesAsync();
            }
            return RedirectToAction("New");
        }


    }
}