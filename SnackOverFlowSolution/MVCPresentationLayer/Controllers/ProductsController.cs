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
using System.Diagnostics;

namespace MVCPresentationLayer.Controllers
{
    /// <summary>
    /// Ariel Sigo
    /// 
    /// Created:
    /// 2017/04/29
    /// 
    /// Products Controller
    /// </summary>
    public class ProductsController : Controller
    {
        IProductManager _productManager;
        private IUserCartManager _userCartManager;
        private IProductOrderManager _orderManager;

        public int PageSize = 10; // change in v2?

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// </summary>
        /// <param name="productManager"></param>
        public ProductsController(IProductManager productManager, IProductOrderManager orderManager, IUserCartManager userCartManager)
        {
            _productManager = productManager;
            _userCartManager = userCartManager;
            _orderManager = orderManager;
        }

        /// <summary>
        /// Skyler Hiscock
        /// 
        /// Created:
        /// 2017/04/13
        /// 
        /// Skyler Hiscock
        /// Updated:
        /// 2017/04/20
        /// 
        /// Michael Takrama
        /// Updated:
        /// 2017/05/09
        /// 
        ///  GET: Products
        /// </summary>
        /// <param name="page">Page Number</param>
        /// <returns>View(ProductS)</returns>
        public ActionResult Index(string category, string search = "", int page = 1)
        {

            var productsList = _productManager.RetrieveProductsToBrowseProducts();

            if (!search.Equals(""))
            {
                var tempSearch = search.ToLower();
                foreach (var str in tempSearch.Split())
                {
                    productsList = productsList.FindAll(p => p.CategoryID.ToLower().Contains(str) ||
                                                         p.Description.ToLower().Contains(str) ||
                                                         p.Name.ToLower().Contains(str) ||
                                                         p.Supplier_Name.ToLower().Contains(str));
                }

            }

            // for testing pagination. copy this line a couple of times to give you extra data
            // productsList.AddRange(productsList);

            IEnumerable<string> categories = productsList.Select(x => x.CategoryID)
                                                         .Distinct()
                                                         .OrderBy(x => x);

            var products = new ProductsListViewModel
            {
                Products = productsList.Where(p => category == null || p.CategoryID == category)
                                           .OrderBy(p => p.ProductId)
                                           .Skip((page - 1) * PageSize)
                                           .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? productsList.Count() : productsList.Where(e => e.CategoryID == category).Count()
                },
                SearchPhrase = search,
                CurrentCategory = category,
                Categories = categories,
                CartPageModel = ExtractCartInformation()
            };
            return View(products);
        }

        /// <summary>
        /// Skyler Hiscock
        /// 
        /// Created:
        /// 2017/04/07
        /// 
        /// Skyler Hiscock
        /// Updated:
        /// 2017/04/14
        /// 
        /// GET: Products/Details/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="supplierId"></param>
        /// <returns>Detail view for specific product.</returns>
        public ActionResult Details(int? id, int? supplierId, string errored = "")
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
            if (errored.Equals("runout")) {
                ViewBag.Error = "Out of stock";
            }
            return View(product);
        }

        /// <summary>
        /// William Flood
        /// 
        /// Created:
        /// 2017/04/06
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FileContentResult PictureOf(int? id)
        {
            try
            {
                Product requested = _productManager.RetrieveProductById((int)id);
                if (null != requested)
                {
                    var pictureArray = requested.ImageBinary;
                    return new FileContentResult(pictureArray, "image/jpeg");
                }
            }
            catch
            {

            }
            Response.StatusCode = 404;
            return null;
        }

        /// <summary>
        /// Skyler Hiscock
        /// 
        /// Created:
        /// 2017/04/20
        /// </summary>
        /// <param name="categories"></param>
        /// <param name="searchPhrase"></param>
        /// <param name="selectedCategory"></param>
        /// <returns>Nav Menu Partial View</returns>
        public PartialViewResult NavMenu(IEnumerable<string> categories, string searchPhrase = "", string selectedCategory = "")
        {

            var navViewModel = new NavMenuViewModel {
                Categories = categories,
                SearchPhrase = searchPhrase,
                SelectedCategory = selectedCategory
            };

            return PartialView(navViewModel);

        }

        /// <summary>
        /// Created by Michael Takrama
        /// 05/09/2017
        /// 
        /// Populates Product/Index flyout
        /// </summary>
        /// <returns></returns>
        public CartPageModel ExtractCartInformation()
        {
            var userName = User.Identity.Name;
            var pageModel = new CartPageModel();

            try
            {
                pageModel.ItemsInCart = _userCartManager.RetrieveUserCart(userName);
                pageModel.SavedOrderList = _orderManager.RetrieveSaveOrders(userName);
            }
            catch
            {
                Debug.WriteLine("Cart Load Failed");
                return new CartPageModel
                {
                    ItemsInCart = new List<UserCartLine>(),
                    SavedOrderList = new List<int>()
                };
            }

            if (pageModel != null)
            {
                return pageModel;
            }
            else
            {
                return new CartPageModel
                {
                    ItemsInCart = new List<UserCartLine>(),
                    SavedOrderList = new List<int>()
                };
            }
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
