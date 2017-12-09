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
        public ActionResult Profile()
        {
            AccountModel am = AccountService.GetAccountByEmail(User.Identity.Name);
            AccountInfoModel aim = AccountService.GetAccountInfo(am.Id);
            return View(aim);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel e)
        {
            int id = AccountService.GetAccountByEmail(User.Identity.Name).Id;
            string editRsp = AccountService.UpdateAccountInfo(id, (AccountInfoModel)e);
            if (String.IsNullOrWhiteSpace(editRsp))
            {
                return RedirectToAction("Profile");
            } else
            {
                ViewBag.Error = editRsp;
                return View();
            }
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LogInViewModel l)
        {
            var logRsp = IsValid(l.Email, l.Password);
            if(String.IsNullOrWhiteSpace(logRsp))
            {
                Session["role"] = Repository.GetAccountByEmail(l.Email).Role;

                FormsAuthentication.SetAuthCookie(l.Email, false);
                AccountService.LogLogin(true, l.Email, Request.UserHostAddress);
                return RedirectToAction("Index", "Home");
            } else if(logRsp == "1")
            {
                ViewBag.Error = "Blogai įvesti duomenys";
                AccountService.LogLogin(false, l.Email, Request.UserHostAddress);
                return View();
            } else if(logRsp == "2")
            {
                ViewBag.Error = "Blogai įvesti duomenys";
                return View();
            }
            return View();
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
            AccountInfoModel acciModel = (AccountInfoModel)r;
            var hashed = Crypto.HashPassword(accModel.Password);
            accModel.Password = hashed;
            string regRsp = AccountService.Register(accModel, acciModel);
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

        private string IsValid(string email, string password)
        {
            var acc = Repository.GetAccountByEmail(email);
            if (acc == null)
            {
                return "2";
            }
            AccountModel am = (AccountModel)Repository.GetAccountByEmail(email);
            if (Crypto.VerifyHashedPassword(acc.Password, password))
            {
                return "";
            }
            else
            {
                return "1";
            }
        }
    }
}