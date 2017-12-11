using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshop.Controllers
{
    public class BaseController : Controller
    {

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;

            // dont let users without admin role to access admin panel
            if (controllerName == "Admin"
                && !IsAdmin())
                filterContext.Result = Redirect("/Home/Forbidden");

            // dont let logged out users to access profile or log out
            if (controllerName == "Account"
                && (actionName == "Profile" || actionName == "LogOut")
                && IsLoggedOut())
                filterContext.Result = Redirect("/Home/Forbidden");

            // dont let logged in users to login
            if (controllerName == "Account"
                && (actionName == "LogIn")
                && IsLoggedIn())
                filterContext.Result = Redirect("/Home/Forbidden");


            // dont let not employees to access manufacturer control
            if (controllerName == "Manufacturer"
                && !IsAtleastEmployee())
                filterContext.Result = Redirect("/Home/Forbidden");

            base.OnActionExecuting(filterContext);
        }

        private bool IsAdmin()
        {
            return Request.Cookies["role"] != null && Request.Cookies["role"].Value == "admin";
        }

        private bool IsAtleastEmployee()
        {
            return (Request.Cookies["role"] != null && Request.Cookies["role"].Value == "employee") || IsAdmin();
        }

        private bool IsAtleastClient()
        {
            return (Request.Cookies["role"] != null && Request.Cookies["role"].Value == "client") || IsAtleastEmployee();
        }

        private bool IsLoggedOut()
        {
            return !IsLoggedIn();
        }

        private bool IsLoggedIn()
        {
            return IsAtleastClient();
        }
    }
}