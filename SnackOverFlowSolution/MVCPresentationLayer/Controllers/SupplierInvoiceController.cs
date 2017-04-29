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
    /// 
    /// Created: 
    /// 2017/04/05
    /// 
    /// Controller logic for supplier invoices
    /// </summary>
    public class SupplierInvoiceController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        ISupplierInvoiceManager invMgr;

        /// <summary>
        /// Victor Algarin
        /// 
        /// Created:
        /// 2017/04/05
        /// </summary>
        /// <param name="_invMgr"></param>
        public SupplierInvoiceController(ISupplierInvoiceManager _invMgr)
        {
            invMgr = _invMgr;
        }

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// GET: /SupplierInvoice/
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(invMgr.RetrieveAllSupplierInvoices());
        }

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        ///  GET: /SupplierInvoice/Details/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View of Tuple with Supplier Invoice Details</returns>
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

        
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        ///GET: /SupplierInvoice/Create
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// POST: /SupplierInvoice/Create
        /// </summary>
        /// <param name="supplierInvoice"></param>
        /// <returns>View of Supplier Invoice</returns>
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

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// GET: /SupplierInvoice/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View of supplier Invoice</returns>
 
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

        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        ///  POST: /SupplierInvoice/Edit/5
        /// </summary>
        /// <param name="newSupplierInvoice"></param>
        /// <returns>View of New Supplier invoice if successful</returns>
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

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// GET: /SupplierInvoice/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
