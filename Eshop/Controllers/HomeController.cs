﻿using Eshop.BusinessLogic;
using Eshop.BusinessLogic.Interfaces;
using Eshop.Data.Entities;
using Eshop.Data.Models;
using Eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService CategoryService;
        private readonly IRepository Repository;
        public HomeController(ICategoryService cs,IRepository rep)
        {
            Repository = rep;
            CategoryService = cs;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult Forbidden()
        {
            Response.TrySkipIisCustomErrors = true;
            Response.StatusCode = 403;
            return View();
        }

        public int InsertCategory(CategoryViewModel category)
        {
            CategoryModel cat = (CategoryModel)category;
            CategoryService.InsertCategory((CategoryModel)cat);
            return 1;
        }
    }
}