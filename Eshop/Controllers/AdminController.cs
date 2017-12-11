using Eshop.BusinessLogic;
using Eshop.BusinessLogic.Interfaces;
using Eshop.Data.Entities;
using Eshop.Models;
using System.Collections.Generic;
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

        public ActionResult EmployeeMain()
        {
            return View();
        }

        public ActionResult EmployeeList()
        {
            var empoyeeList = _adminService.GetEmployeeAccountList();
            return View(empoyeeList);
        }

        public ActionResult ClientList()
        {
            var clientList = _adminService.GetClientAccountList();
            return View(clientList);
        }

        public ActionResult DeleteEmployee(int id, bool isClient)
        {
            _repository.DeleteAccount(id);
            if (isClient)
            {
                TempData["ShowSuccessMessage"] = "Klientas sėkmingai ištrintas!";
                return RedirectToAction("ClientList");
            }
            else
            {
                TempData["ShowSuccessMessage"] = "Darbuotojas sėkmingai ištrintas!";
                return RedirectToAction("EmployeeList");
            }

        }

        public ActionResult ChangeRole(int id, Account.AccRole newRole)
        {
            _repository.ChangeAccountRole(id, newRole);
            if (newRole == Account.AccRole.client)
            {
                TempData["ShowSuccessMessage"] = "Darbuotojui sėkmingai panaikinta darbuotojo rolė!";
                return RedirectToAction("EmployeeList");
            }
            else
            {
                TempData["ShowSuccessMessage"] = "Klientui sėkmingai suteikta darbuotojo rolė!";
                return RedirectToAction("ClientList");
            }
        }
        [HttpGet]
        public ActionResult ChooseNewEmployee()
        {
            var model = new NewEmployeeSelectViewModel();

            var listItems = new List<SelectListItem>();
            foreach (var item in _adminService.GetClientAccountList())
            {
                listItems.Add(new SelectListItem() {
                    Text = string.Format("{0} {1}, {2}", item.AccountInfo.Name, item.AccountInfo.LastName, item.Email),
                    Value = item.AccountInfo.AccountId.ToString()
                });
            }

            model.Options = listItems;
            return View(model);

        }

        [HttpPost]
        public ActionResult ChooseNewEmployee(NewEmployeeSelectViewModel vm)
        {
            _repository.ChangeAccountRole(int.Parse(vm.State), Account.AccRole.employee);
            TempData["ShowSuccessMessage"] = "Darbuotojas pridėtas!";
            return RedirectToAction("EmployeeList");
        }


    }
}
