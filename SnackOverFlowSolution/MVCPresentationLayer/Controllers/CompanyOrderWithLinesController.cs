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
    /// Ariel Sigo
    /// 
    /// Created:
    /// 2017/04/29
    /// 
    /// Company Order With Lines Controller
    /// </summary>
    public class CompanyOrderWithLinesController : Controller
    {
        private ICompanyOrderManager _companyOrderManager;
        private ISupplierManager _supplierManager;
        private IUserManager _userManager;

        public CompanyOrderWithLinesController(ICompanyOrderManager companyOrderManager, ISupplierManager supplierManager,
            IUserManager userManager)
        {
            _companyOrderManager = companyOrderManager;
            _supplierManager = supplierManager;
            _userManager = userManager;
        }

        
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        ///
        /// GET: CompanyOrderWithLines
        /// </summary>
        /// <returns>View of orders for Supplier Role</returns>
        [Authorize(Roles="Supplier")]
        public ActionResult Index()
        {
            List<CompanyOrderWithLines> orders;
            try
            {
                DataObjects.User usr = _userManager.RetrieveUserByUserName(User.Identity.Name);
                Supplier supplier = _supplierManager.RetrieveSupplierByUserId(usr.UserId);
                //orders = _companyOrderManager.RetrieveCompanyOrdersWithLines();
                orders = _companyOrderManager.RetrieveCompanyOrdersWithLinesBySupplierId(supplier.SupplierID);
                return View(orders);
            }
            catch (Exception)
            {

                return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
            }
            
        }

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// GET: CompanyOrderWithLines/Details/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View of Company order with lines if successful, error if not.</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                CompanyOrderWithLines companyOrderWithLines = _companyOrderManager.RetrieveCompanyOrderWithLinesById((int)id);
                if (companyOrderWithLines == null)
                {
                    return HttpNotFound();
                }
                return View(companyOrderWithLines);
            }
            catch (Exception)
            {

                return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
            }
            
        }

        /// <summary>
        /// Christian Lopez
        /// 
        /// Created: 
        /// 2017/04/29
        /// 
        /// </summary>
        /// <param name="companyOrder"></param>
        /// <returns>View of invoice and company order if successful</returns>
        [Authorize(Roles="Supplier")]
        public ActionResult InvoiceDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyOrderWithLines companyOrder = null;
            try
            {
                companyOrder = _companyOrderManager.RetrieveCompanyOrderWithLinesById((int)id);
            }
            catch (Exception)
            {
                
                return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
            }

            if (null == companyOrder)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            return View("Invoice", companyOrder);
        }

        //// GET: CompanyOrderWithLines/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: CompanyOrderWithLines/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,CompanyOrderID,EmployeeId,SupplierId,Amount,OrderDate,HasArrived,Active")] CompanyOrderWithLines companyOrderWithLines)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.CompanyOrderWithLines.Add(companyOrderWithLines);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(companyOrderWithLines);
        //}

        //// GET: CompanyOrderWithLines/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CompanyOrderWithLines companyOrderWithLines = db.CompanyOrderWithLines.Find(id);
        //    if (companyOrderWithLines == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(companyOrderWithLines);
        //}

        //// POST: CompanyOrderWithLines/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,CompanyOrderID,EmployeeId,SupplierId,Amount,OrderDate,HasArrived,Active")] CompanyOrderWithLines companyOrderWithLines)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(companyOrderWithLines).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(companyOrderWithLines);
        //}

        //// GET: CompanyOrderWithLines/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CompanyOrderWithLines companyOrderWithLines = db.CompanyOrderWithLines.Find(id);
        //    if (companyOrderWithLines == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(companyOrderWithLines);
        //}

        //// POST: CompanyOrderWithLines/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    CompanyOrderWithLines companyOrderWithLines = db.CompanyOrderWithLines.Find(id);
        //    db.CompanyOrderWithLines.Remove(companyOrderWithLines);
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
