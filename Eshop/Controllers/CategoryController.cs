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
    public class CategoryController : BaseController
    {
        private readonly ICategoryService CategoryService;
        private readonly IRepository Repository;
        public CategoryController(ICategoryService cs, IRepository rep)
        {
            Repository = rep;
            CategoryService = cs;
        }
        // GET: Category
        public ActionResult Index()
        {
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
                        try
                        {
                            Stream stream = upload.InputStream;
                            Repository.InsertCategoriesFromJsonFile(new StreamReader(stream));
                            return RedirectToAction("Index");
                        }
                        catch
                        {
                            ModelState.AddModelError("File", "Netinkamas failo formatas");
                        }
                        
                    }
                    else
                    {
                        ModelState.AddModelError("File", "Netinkamas failo formatas");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Prašome įdėti failą");
                }
            }
            //return RedirectToAction("Index");
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
                        try
                        {
                            Stream stream = upload.InputStream;
                            Repository.InsertCategoryAttributesFromJsonFile(new StreamReader(stream));
                            return RedirectToAction("Index");
                        }
                        catch
                        {
                            ModelState.AddModelError("File", "Netinkamas failo formatas");
                            return View();
                        }
                        
                    }
                    else
                    {
                        ModelState.AddModelError("File", "Netinkamas failo formatas");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Prašome įdėti failą");
                }
            }
            //return RedirectToAction("Index");
            return View();
        }
        public ActionResult AttributeEntryRow()
        {
            return PartialView("AttributeEntryEditor");
        }
        public ActionResult Details(int? id)
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
            return View((CategoryModel)cat);
        }
        public ActionResult EditCategory(int? id)
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
            PopulateDropDownList(cat.ParentId);
            return View((CategoryModel)cat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(CategoryModel categoryModel)
        {
            if (categoryModel.Id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Repository.EditCategory((Category)categoryModel);
                return RedirectToAction("CategoryList");
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateDropDownList(categoryModel.ParentId);
            return View(categoryModel);
        }
        public ActionResult CategoryList(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var list = Repository.GetCategories(searchString,true,false);
            ViewBag.CurrentFilter = searchString;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        private void PopulateDropDownList(object selectedCategory = null)
        {
            var list = Repository.GetParentCategories();

            ViewBag.ParentId = new SelectList(list, "Id", "Name", selectedCategory);
        }
        public ActionResult UploadAttributes()
        {

            return View();
        }
        public ActionResult UploadCategories()
        {
            return View();
        }
        // GET: Category/Details/5

        // GET: Category/Create
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

        // GET: Category/Edit/5

        // POST: Category/Edit/5
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

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Repository.DeleteCategory((int)id);
            }
            catch (RetryLimitExceededException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("CategoryList");
        }
    }
}
