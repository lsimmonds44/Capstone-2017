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
    public class SupplierWithAgreementsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private ISupplierManager _supplierManager;
        private IProductManager _productManager;
        private IAgreementManager _agreementManager;

        public SupplierWithAgreementsController(ISupplierManager supplierManager, IProductManager productManager, IAgreementManager agreementManager)
        {
            _supplierManager = supplierManager;
            _productManager = productManager;
            _agreementManager = agreementManager;
        }

        [Authorize]
        // GET: SupplierWithAgreements
        public ActionResult Index()
        {
            return View(_supplierManager.RetrieveSuppliersWithAgreements());
        }

        //// GET: SupplierWithAgreements/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SupplierWithAgreements supplierWithAgreements = db.SupplierWithAgreements.Find(id);
        //    if (supplierWithAgreements == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(supplierWithAgreements);
        //}

        // GET: SupplierWithAgreements/Create
        [Authorize]
        public ActionResult Create()
        {
            if (User.IsInRole("Supplier"))
            {
                ViewBag.Account = "supplier";
                return View("AlreadyAccepted");
            }
            List<Product> products = _productManager.ListProducts();
            SupplierWithAgreements dummy = new SupplierWithAgreements()
            {
                Agreements = new List<AgreementWithProductName>()
            };
            foreach (Product p in products)
            {
                dummy.Agreements.Add(new AgreementWithProductName()
                {
                    ProductId = p.ProductId,
                    ProductName = p.Name
                });
            }
            return View(dummy);
        }

        // POST: SupplierWithAgreements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "FarmTaxID,UserId,FarmName,FarmAddress,FarmCity,FarmState,Agreements")] SupplierWithAgreements supplierWithAgreements)
        public ActionResult Create([Bind(Include = "FarmTaxID,FarmName,FarmAddress,FarmCity,FarmState,ProductIDs")] SupplierWithAgreements supplierWithAgreements)
        {
            supplierWithAgreements.Agreements = new List<AgreementWithProductName>();
            if (supplierWithAgreements.ProductIDs != null)
            {
                for (int i = 0; i < supplierWithAgreements.ProductIDs.Length; i++)
                {
                    supplierWithAgreements.Agreements.Add(new AgreementWithProductName()
                    {
                        ProductId = supplierWithAgreements.ProductIDs[i],
                        ProductName = _productManager.RetrieveProductById(supplierWithAgreements.ProductIDs[i]).Name
                    });
                }
            }
            if (ModelState.IsValid)
            {

                //Christian Lopez - Will want to change when we have a current user implemented.
                supplierWithAgreements.UserId = 10000;
                try
                {


                    if (_supplierManager.ApplyForSupplierAccount(supplierWithAgreements))
                    {
                        supplierWithAgreements.SupplierID = _supplierManager.RetrieveSupplierByUserId(supplierWithAgreements.UserId).SupplierID;
                        foreach (Agreement a in supplierWithAgreements.Agreements)
                        {
                            if (!_agreementManager.CreateAgreementsForSupplier(supplierWithAgreements, _productManager.RetrieveProductById(a.ProductId)))
                            {
                                return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
                            }
                        }
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
                    }
                }
                catch (Exception)
                {

                    return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
                }

            }

            List<Product> products = _productManager.ListProducts();
            supplierWithAgreements.Agreements = new List<AgreementWithProductName>();
            foreach (Product p in products)
            {
                supplierWithAgreements.Agreements.Add(new AgreementWithProductName()
                {
                    ProductId = p.ProductId,
                    ProductName = p.Name
                });
            }

            return View(supplierWithAgreements);
        }

        //// GET: SupplierWithAgreements/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SupplierWithAgreements supplierWithAgreements = db.SupplierWithAgreements.Find(id);
        //    if (supplierWithAgreements == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(supplierWithAgreements);
        //}

        //// POST: SupplierWithAgreements/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,SupplierID,IsApproved,ApprovedBy,FarmTaxID,UserId,FarmName,FarmAddress,FarmCity,FarmState,Active")] SupplierWithAgreements supplierWithAgreements)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(supplierWithAgreements).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(supplierWithAgreements);
        //}

        //// GET: SupplierWithAgreements/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SupplierWithAgreements supplierWithAgreements = db.SupplierWithAgreements.Find(id);
        //    if (supplierWithAgreements == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(supplierWithAgreements);
        //}

        //// POST: SupplierWithAgreements/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    SupplierWithAgreements supplierWithAgreements = db.SupplierWithAgreements.Find(id);
        //    db.SupplierWithAgreements.Remove(supplierWithAgreements);
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
