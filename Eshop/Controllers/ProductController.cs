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
using PagedList;
using System.Data.Entity.Infrastructure;
using Eshop.Models;

namespace Eshop.Controllers
{
    public class ProductController : BaseController
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
        public ActionResult ProductList(FilterViewModel filter)
        {
            if (filter.CategoryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Data.Entities.Attribute> att = Repository.GetAttributes((int)filter.CategoryId);
            if (att == null|| att.Count==0)
            {
                return HttpNotFound();
            }
            
            filter.Attributes = att;
            filter.FilterAttributes = Repository.Temp(att);
            List<Product> list = Repository.GetProducts((int)filter.CategoryId);
            filter.Products = list;
            return View(filter);
        }
        [HttpPost, ActionName("ProductList")]
        public ActionResult ProductListPost(FilterViewModel filter)
        {
            if (filter.CategoryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Data.Entities.Attribute> att = Repository.GetAttributes((int)filter.CategoryId);
            if (att == null || att.Count == 0)
            {
                return HttpNotFound();
            }
            filter.Attributes = att;
            List<Product> list = Repository.GetProducts((FilterModel)filter);
            filter.Products = list;
            filter.FilterAttributes = Repository.Temp(att);
            return View(filter);
        }
        public ActionResult Categories()
        {
            List<Category> viewModel = Repository.GetCategories(null,false,true);
            return View(viewModel);
        }
        public ActionResult Product(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var list = Repository.GetProducts(searchString);
            ViewBag.CurrentFilter = searchString;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult SelectCategory(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var list = Repository.GetCategories(searchString);
            ViewBag.CurrentFilter = searchString;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Manufacturer(string currentFilter, string searchString, int? page)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadProducts(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null || upload.ContentLength > 0)
                {
                    if (upload.FileName.EndsWith(".json"))
                    {
                        Stream stream = upload.InputStream;
                        Repository.InsertProductsFromJsonFile(new StreamReader(stream));
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
        
        public ActionResult UploadProducts()
        {
            return View();
        }
        public ActionResult Upload()
        {
            return View();
        }
        
        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = Repository.GetProduct((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View((ProductModel)product);
        }
        public ActionResult AttributeEntryRow()
        {
            return PartialView("AttributeEntryEditor");
        }
        public ActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = Repository.GetProduct((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }
            PopulateManufacturerList(product.ManufacturerId);
            return View((ProductModel)product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(ProductModel productModel)
        {
            if (productModel.Id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Repository.EditProduct((Product)productModel);
                return RedirectToAction("Product");
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateManufacturerList(productModel.ManufacturerId);
            return View(productModel);
        }
        public ActionResult AddProduct(int? categoryId)
        {
            if (categoryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category cat = Repository.GetCategory((int)categoryId);
            if (cat == null)
            {
                return HttpNotFound();
            }
            ProductModel product = new ProductModel
            {
                Category = cat,
                CategoryId=cat.Id
            };
            PopulateManufacturerList();
            return View(product);
        }
        private void PopulateManufacturerList(object selectedManufacturer = null)
        {
            var list = Repository.GetManufacturers();

            ViewBag.ManufacturerId = new SelectList(list, "Id", "Name", selectedManufacturer);
        }
        [HttpPost]
        public ActionResult AddProduct(ProductModel product)
        {
            product.Category = Repository.GetCategory(product.CategoryId);
            product.Manufacturer = Repository.GetManufacturer(product.ManufacturerId);
            product.CreationDate = DateTime.Now;
            product.UpdateDate = DateTime.Now;
            for(int i = 0; i < product.Attributes.Count; i++)
            {
                product.Attributes[i].Attribute = product.Category.Attributes[i];
                product.Attributes[i].AttributeId = product.Category.Attributes[i].Id;
            }
            try
            {
                    Repository.InsertProduct((Product)product);
                    return RedirectToAction("Product");
                // TODO: Add insert logic here
            }
            catch
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");

            }
            PopulateManufacturerList(product.ManufacturerId);
            return View(product);
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

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                Repository.DeleteProduct((int)id);
            }
            catch (RetryLimitExceededException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Product");
        }
    }
}
