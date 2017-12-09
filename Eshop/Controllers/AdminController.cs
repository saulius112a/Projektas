using Eshop.BusinessLogic.Implementations;
using Eshop.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshop.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public ActionResult AdminMain(IAccountService accs)
        {
            return View();
        }

        public ActionResult ClientList()
        {
            var clientList = _adminService.GetClientAccountList();
            return View(clientList);
        }
    }
}