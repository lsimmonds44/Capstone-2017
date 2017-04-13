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
    public class CompanyOrderWithLinesController : Controller
    {
        private ICompanyOrderManager _companyOrderManager;

        public CompanyOrderWithLinesController(ICompanyOrderManager companyOrderManager)
        {
            _companyOrderManager = companyOrderManager;
        }

        // GET: CompanyOrderWithLines
        [Authorize]
        public ActionResult Index()
        {
            List<CompanyOrderWithLines> orders;
            try
            {
                orders = _companyOrderManager.RetrieveCompanyOrdersWithLines();
                return View(orders);
            }
            catch (Exception)
            {

                return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
            }
            
        }

        // GET: CompanyOrderWithLines/Details/5
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
