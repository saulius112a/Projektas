﻿using Eshop.BusinessLogic;
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
    public class AccountController : BaseController
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
        public ActionResult AddFavorites(int id)
        {
            AccountModel am = AccountService.GetAccountByEmail(User.Identity.Name);
            AccountService.AddFavorites(id, am.Id);
            return RedirectToAction("Details", "Product", new { id = id });
        }

        [HttpGet]
        public ActionResult RemoveFavorites(int id)
        {
            AccountModel am = AccountService.GetAccountByEmail(User.Identity.Name);
            AccountService.RemoveFavorite(id, am.Id);
            return RedirectToAction("Favorites", "Account");
        }

        [HttpGet]
        public ActionResult Favorites()
        {
            AccountModel am = AccountService.GetAccountByEmail(User.Identity.Name);
            List<Product> list = AccountService.GetFavoriteProducts(am.Id);
            return View(list);
        }

        [HttpGet]
        public ActionResult AddCart(int id)
        {
            AccountModel am = AccountService.GetAccountByEmail(User.Identity.Name);
            AccountService.AddCart(id, am.Id);
            return RedirectToAction("Cart", "Account");
        }

        [HttpGet]
        public ActionResult RemoveCart(int id)
        {
            AccountModel am = AccountService.GetAccountByEmail(User.Identity.Name);
            AccountService.RemoveCart(id, am.Id);
            return RedirectToAction("Cart", "Account");
        }

        [HttpGet]
        public ActionResult Cart()
        {
            AccountModel am = AccountService.GetAccountByEmail(User.Identity.Name);
            List<Product> list = AccountService.GetCartProducts(am.Id);
            double amount = 0;
            for(int i = 0; i < list.Count; i++)
            {
                amount += list[i].Price;
            }
            ViewBag.amount = amount;
            return View(list);
        }

        [HttpGet]
        public ActionResult Profile()
        {
            AccountModel am = AccountService.GetAccountByEmail(User.Identity.Name);
            AccountInfoModel aim = AccountService.GetAccountInfo(am.Id);
            return View(aim);
        }

        [HttpPost]
        public ActionResult Profile(EditViewModel e)
        {
            int id = AccountService.GetAccountByEmail(User.Identity.Name).Id;
            string editRsp = AccountService.UpdateAccountInfo(id, (AccountInfoModel)e);
            if (String.IsNullOrWhiteSpace(editRsp))
            {
                TempData["ShowSuccessMessage"] = "Profilis atnaujintas!";
                return RedirectToAction("Profile");
            }
            else
            {
                TempData["ShowErrorMessage"] = editRsp;
                //ViewBag.Error = editRsp;
                return View();
            }
        }

        public ActionResult CheckOut()
        {
            int uid = AccountService.GetAccountByEmail(User.Identity.Name).Id;
            int id =AccountService.CreatePurchase(uid);
            return RedirectToAction("Purchase", "Account", new { id = id });
        }
        
        public ActionResult Purchase(int id)
        {
            List<Product> list = AccountService.GetPurchaseProducts(id);
            double amount = 0;
            for (int i = 0; i < list.Count; i++)
            {
                amount += list[i].Price;
            }
            ViewBag.amount = amount;
            return View(list);
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
                TempData["ShowSuccessMessage"] = "Sėkmingai prisijungta!";
                var cookie = new HttpCookie("role", Repository.GetAccountByEmail(l.Email).Role.ToString());
                Response.Cookies.Add(cookie);
                FormsAuthentication.SetAuthCookie(l.Email, false);
                AccountService.LogLogin(true, l.Email, Request.UserHostAddress);
                return RedirectToAction("Index", "Home");
            } else if(logRsp == "1")
            {
                TempData["ShowErrorMessage"] = "Blogai įvesti duomenys.";
                AccountService.LogLogin(false, l.Email, Request.UserHostAddress);
                return View();
            } else if(logRsp == "2")
            {
                TempData["ShowErrorMessage"] = "Blogai įvesti duomenys.";
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
                TempData["ShowSuccessMessage"] = "Vartotojas sėkmingai sukurtas!";
                return RedirectToAction("LogIn", "Account");
            }
            else if(regRsp.Equals("1"))
            {
                TempData["ShowErrorMessage"] = "Toks el. pašto adresas jau egzistuoja.";
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public ActionResult LogOut()
        {
            var cookie = new HttpCookie("role", null);
            Response.Cookies.Add(cookie);
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