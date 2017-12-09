using Eshop.BusinessLogic;
using Eshop.BusinessLogic.Interfaces;
using Eshop.Data.Entities;
using System.Web.Mvc;

namespace Eshop.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IRepository _repository;

        public AdminController(IAdminService adminService, IRepository repository)
        {
            _adminService = adminService;
            _repository = repository;
        }

        public ActionResult AdminMain()
        {
            return View();
        }

        public ActionResult EmployeeList()
        {
            var empoyeeList = _adminService.GetClientAccountList();
            return View(empoyeeList);
        }

        public ActionResult DeleteEmployee(int id)
        {
            _repository.DeleteAccount(id);
            TempData["ShowEmployeeDeleted"] = true;
            return RedirectToAction("EmployeeList");
        }
    }
}