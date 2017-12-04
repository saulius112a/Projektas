using Eshop.BusinessLogic;
using Eshop.BusinessLogic.Implementations;
using Eshop.BusinessLogic.Interfaces;
using Eshop.Data.Entities;
using Eshop.Data.Models;
using Eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace Eshop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService AccountService;
        private readonly IRepository Repository;
        // GET: Account
        public AccountController(IAccountService accs, IRepository rep)
        {
            Repository = rep;
            AccountService = accs;
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LogInViewModel l)
        {
            if(IsValid(l.Email, l.Password))
            {
                FormsAuthentication.SetAuthCookie(l.Email, false);
                return RedirectToAction("Index", "Home");
            } else
            {
                ViewBag.Error = "Blogai įvesti duomenys";
                return View();
            }
        }
        
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel r)
        {
            AccountModel accModel = (AccountModel)r;
            var hashed = Crypto.HashPassword(accModel.Password);
            accModel.Password = hashed;
            string regRsp = AccountService.Register(accModel);
            if(String.IsNullOrWhiteSpace(regRsp))
            {
                return RedirectToAction("LogIn", "Account");
            } else if(regRsp.Equals("1"))
            {
                ViewBag.Error = "Toks elektroninio pašto adresas jau egzistuoja";
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private bool IsValid(string email, string password)
        {
            var acc = Repository.GetAccountByEmail(email);
            if (acc == null)
            {
                return false;
            }
            AccountModel am = (AccountModel)Repository.GetAccountByEmail(email);
            if(Crypto.VerifyHashedPassword(acc.Password, password))
            {
                return true;
            }
            return false;
        }
    }
}