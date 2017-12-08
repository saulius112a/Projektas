using Eshop.BusinessLogic;
using Eshop.BusinessLogic.Interfaces;
using Eshop.Data.Entities;
using Eshop.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Eshop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        private readonly ICategoryService CategoryService;
        private readonly IRepository Repository;
        public ProductController(ICategoryService cs, IRepository rep)
        {
            Repository = rep;
            CategoryService = cs;
        }
        // GET: Product
        public ActionResult Index()
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadCategories(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null || upload.ContentLength > 0)
                {
                    if (upload.FileName.EndsWith(".json"))
                    {
                        Stream stream = upload.InputStream;
                        Repository.InsertCategoriesFromJsonFile(new StreamReader(stream));
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
        public ActionResult UploadCategories()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadAttributes(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null || upload.ContentLength > 0)
                {
                    if (upload.FileName.EndsWith(".json"))
                    {
                        Stream stream = upload.InputStream;
                        Repository.InsertCategoryAttributesFromJsonFile(new StreamReader(stream));
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
        public ActionResult Upload()
        {
            return View();
        }
        public ActionResult UploadAttributes()
        {
            return View();
        }
        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult AttributeEntryRow()
        {
            return PartialView("AttributeEntryEditor");
        }
        public ActionResult AddProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category cat = Repository.GetCategory((int)id);
            if (cat == null)
            {
                return HttpNotFound();
            }
            ProductModel product = new ProductModel
            {
                Category = cat
            };
            PopulateManufacturerList();
            return View(product);
        }
        private void PopulateManufacturerList(object selectedManufacturer = null)
        {
            var list = Repository.GetManufacturers();

            ViewBag.ManufacturerId = new SelectList(list, "Id", "Name");
        }
        [HttpPost]
        public ActionResult AddProduct(ProductModel product)
        {
            PopulateManufacturerList(product.ManufacturerId);
            return View();
        }
        [HttpPost]
        public ActionResult AddAttributes(CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                Repository.InsertAttributes((Category)category);
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public ActionResult AddAttributes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryModel category = (CategoryModel)Repository.GetCategory((int)id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
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
        // GET: Product/Create
        public ActionResult Create()
        {
            PopulateDropDownList();
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(CategoryModel category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Repository.InsertCategory((Category)category);
                    return RedirectToAction("Index");
                }
                // TODO: Add insert logic here


            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

            }
            PopulateDropDownList(category.ParentId);
            return View(category);
        }
        private void PopulateDropDownList(object selectedCategory = null)
        {
            var list = Repository.GetParentCategories();

            ViewBag.ParentId = new SelectList(list, "Id", "Name", selectedCategory);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
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
