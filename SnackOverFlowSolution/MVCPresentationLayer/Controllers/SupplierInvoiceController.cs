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
    /// <summary>
    /// Victor Algarin
    /// Controller logic for supplier invoices
    /// 
    /// Created: 04/05/2017
    /// </summary>
    public class SupplierInvoiceController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        ISupplierInvoiceManager invMgr = new SupplierInvoiceManager();

        // GET: /SupplierInvoice/
        public ActionResult Index()
        {
            return View(invMgr.RetrieveAllSupplierInvoices());
        }

        // GET: /SupplierInvoice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var supplierInvoice = invMgr.RetrieveAllSupplierInvoices().Find(i => i.SupplierInvoiceId == (int)id);
            var invoiceDetails = invMgr.RetrieveSupplierInvoiceLinesByInvoiceId(supplierInvoice.SupplierInvoiceId).Find(i => i.SupplierInvoiceId == (int)id);
            var tuple = Tuple.Create(invoiceDetails, supplierInvoice);

            if (supplierInvoice == null)
            {
                return HttpNotFound();
            }
            return View(tuple);
        }

        // GET: /SupplierInvoice/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /SupplierInvoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="SupplierInvoiceId,SupplierId,InvoiceDate,SubTotal,TaxAmount,Total,AmountPaid,Approved,Active")] SupplierInvoice supplierInvoice)
        {
            if (ModelState.IsValid)
            {
                if (invMgr.CreateSupplierInvoice(supplierInvoice) != 0) 
                {
                    return RedirectToAction("Index");
                }                
            }
            return View(supplierInvoice);
        }

        // GET: /SupplierInvoice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var supplierInvoice = invMgr.RetrieveAllSupplierInvoices().Find(i => i.SupplierInvoiceId == (int)id);
            if (supplierInvoice == null)
            {
                return HttpNotFound();
            }
            return View(supplierInvoice);
        }

        // POST: /SupplierInvoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SupplierInvoiceId,SupplierId,InvoiceDate,SubTotal,TaxAmount,Total,AmountPaid,Approved,Active")] SupplierInvoice newSupplierInvoice)
        {
            if (ModelState.IsValid)
            {
                var oldSupplierInvoice = invMgr.RetrieveAllSupplierInvoices().Find(i => i.SupplierInvoiceId == (int?)newSupplierInvoice.SupplierInvoiceId);

                if (invMgr.UpdateSupplierInvoice(oldSupplierInvoice, newSupplierInvoice) == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
                }
            }
            return View(newSupplierInvoice);
        }

        // GET: /SupplierInvoice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var supplierInvoice = invMgr.RetrieveAllSupplierInvoices().Find(i => i.SupplierInvoiceId == (int)id);
            if (supplierInvoice == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (invMgr.DeleteSupplierInvoice(supplierInvoice) == true)
                {
                    return RedirectToAction("Index");
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        //// POST: /SupplierInvoice/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    SupplierInvoice supplierinvoice = db.SupplierInvoices.Find(id);
        //    db.SupplierInvoices.Remove(supplierinvoice);
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
