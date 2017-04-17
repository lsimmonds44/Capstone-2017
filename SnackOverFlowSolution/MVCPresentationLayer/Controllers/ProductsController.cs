using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using MVCPresentationLayer.Models;
using LogicLayer;

namespace MVCPresentationLayer.Controllers
{
    public class ProductsController : Controller
    {
        IProductManager _productManager;
        public int PageSize = 10; // change in v2?

        public ProductsController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        /// <summary>
        /// Author: Skyler Hiscock
        /// Updated: 4/14/17
        /// </summary>
        /// <param name="page">Page Number</param>
        /// <returns></returns>
        // GET: Products
        public ActionResult Index(string search = "", int page = 1)
        {
            
            var productsList = _productManager.RetrieveProductsToBrowseProducts();
            
            if (!search.Equals(""))
            {
                var tempSearch = search.ToLower();
                productsList = productsList.FindAll(p => p.CategoryID.ToLower().Contains(tempSearch) || 
                                                         p.Description.ToLower().Contains(tempSearch) || 
                                                         p.Name.ToLower().Contains(tempSearch) ||
                                                         p.Supplier_Name.ToLower().Contains(tempSearch));
            }

            // for testing pagination. copy this line a couple of times to give you extra data
            // productsList.AddRange(productsList);


            var products = new ProductsListViewModel {
                    Products = productsList.OrderBy(p => p.ProductId)
                                           .Skip((page - 1) * PageSize)
                                           .Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = productsList.Count()
                    },
                    searchPhrase = search
                };
            return View(products);
        }

        /// <summary>
        /// Author: Skyler Hiscock
        /// Updated 4/14/17
        /// </summary>
        /// <param name="id"></param>
        /// <param name="supplierId"></param>
        /// <returns>Detail view for specific product.</returns>
        // GET: Products/Details/5
        public ActionResult Details(int? id, int? supplierId)
        {
            if (id == null || supplierId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = _productManager.RetrieveProductsToBrowseProducts().Find(i => i.ProductId == id && i.SupplierID == supplierId);
            var gradeOptionList = _productManager.RetrieveGradeForProduct((int)id);
            product.GradeOptions = gradeOptionList;
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        /// <summary>
        /// Created on 2017/04/06 by William Flood
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FileContentResult PictureOf(int? id)
        {
            try
            {
                Product requested = _productManager.RetrieveProductById((int)id);
                if(null!=requested)
                {
                    var pictureArray = requested.ImageBinary;
                    return new FileContentResult(pictureArray, "image/jpeg");
                }
            } catch
            {

            }
            Response.StatusCode = 404;
            return null;
        }

        //// GET: Products/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Products/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ProductId,Name,Description,UnitPrice,ImageName,Active,UnitOfMeasurement,DeliveryChargePerUnit,ImageBinary")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Products.Add(product);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(product);
        //}

        //// GET: Products/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product product = db.Products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}

        //// POST: Products/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ProductId,Name,Description,UnitPrice,ImageName,Active,UnitOfMeasurement,DeliveryChargePerUnit,ImageBinary")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(product).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(product);
        //}

        //// GET: Products/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Product product = db.Products.Find(id);
        //    if (product == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(product);
        //}

        //// POST: Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Product product = db.Products.Find(id);
        //    db.Products.Remove(product);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
