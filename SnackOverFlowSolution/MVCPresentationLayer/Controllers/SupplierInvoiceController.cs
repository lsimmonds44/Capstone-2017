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
    /// Controller logic for supplier invoices
    /// 
    /// Victor Algarin
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
            if (supplierInvoice == null)
            {
                return HttpNotFound();
            }
            return View(supplierInvoice);
        }

        //// GET: /SupplierInvoice/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: /SupplierInvoice/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include="SupplierInvoiceId,SupplierId,InvoiceDate,SubTotal,TaxAmount,Total,AmountPaid,Approved,Active")] SupplierInvoice supplierinvoice)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.SupplierInvoices.Add(supplierinvoice);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(supplierinvoice);
        //}

        //// GET: /SupplierInvoice/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SupplierInvoice supplierinvoice = db.SupplierInvoices.Find(id);
        //    if (supplierinvoice == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(supplierinvoice);
        //}

        //// POST: /SupplierInvoice/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include="SupplierInvoiceId,SupplierId,InvoiceDate,SubTotal,TaxAmount,Total,AmountPaid,Approved,Active")] SupplierInvoice supplierinvoice)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(supplierinvoice).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(supplierinvoice);
        //}

        //// GET: /SupplierInvoice/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SupplierInvoice supplierinvoice = db.SupplierInvoices.Find(id);
        //    if (supplierinvoice == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(supplierinvoice);
        //}

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
