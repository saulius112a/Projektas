using Eshop.BusinessLogic;
using Eshop.BusinessLogic.Interfaces;
using Eshop.Data.Entities;
using Eshop.Data.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Eshop.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly ICategoryService CategoryService;
        private readonly IRepository Repository;
        public ManufacturerController(ICategoryService cs, IRepository rep)
        {
            Repository = rep;
            CategoryService = cs;
        }
        // GET: Manufacturer
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var list = Repository.GetManufacturers(searchString);
            ViewBag.CurrentFilter = searchString;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult CreateManufacturer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateManufacturer(ManufacturerModel manufacturer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Repository.InsertManufacturer((Manufacturer)manufacturer);
                    return RedirectToAction("Index");
                }
                // TODO: Add insert logic here
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(manufacturer);
        }
        public ActionResult EditManufacturer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer man = Repository.GetManufacturer((int)id);
            if (man == null)
            {
                return HttpNotFound();
            }
            return View((ManufacturerModel)man);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditManufacturer(ManufacturerModel manufacturerModel)
        {
            if (manufacturerModel.Id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Repository.EditManufacturer((Manufacturer)manufacturerModel);
                return RedirectToAction("Manufacturer");
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(manufacturerModel);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturer man = Repository.GetManufacturer((int)id);
            if (man == null)
            {
                return HttpNotFound();
            }
            return View((ManufacturerModel)man);
        }
        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null || upload.ContentLength > 0)
                {
                    if (upload.FileName.EndsWith(".json"))
                    {
                        Stream stream = upload.InputStream;
                        Repository.InsertManufacturersFromJsonFile(new StreamReader(stream));
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            //return RedirectToAction("Index");
            return View();
        }
        // GET: Manufacturer/Details/5

        // GET: Manufacturer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manufacturer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: Manufacturer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Manufacturer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
