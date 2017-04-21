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
    /// Created by Victor Algarin
    /// Date: 04/21/2017
    /// 
    /// Controller for Product Orders
    /// Note: In order to create the controller, I had to remove nullable option for fields 
    /// under ProductOrder object, as well as the lists under that object
    /// </summary>

    public class ProductOrdersController : Controller
    {
        IProductOrderManager ordMgr = new ProductOrderManager();


        // GET: ProductOrders
        public ActionResult Index()
        {
            var order = ordMgr.RetrieveProductOrdersByStatus("Open");
            return View(order);
        }

        // GET: ProductOrders/Details/5
        public ActionResult Details(int? id)
        {

            var order = ordMgr.RetrieveProductOrdersByStatus("Open").Find(o => o.OrderId == (int)id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }            
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: ProductOrders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "DummyOrderId,CustomerId,EmployeeId,OrderTypeId,AddressType,DeliveryTypeId,Amount,OrderDate,DateExpected,Discount,OrderStatusId,UserAddressId,HasArrived")] ProductOrder productOrder)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.DummyOrders.Add(dummyOrder);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(dummyOrder);
        //}

        //// GET: ProductOrders/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DummyOrder dummyOrder = db.DummyOrders.Find(id);
        //    if (dummyOrder == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(dummyOrder);
        //}

        //// POST: ProductOrders/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "DummyOrderId,CustomerId,EmployeeId,OrderTypeId,AddressType,DeliveryTypeId,Amount,OrderDate,DateExpected,Discount,OrderStatusId,UserAddressId,HasArrived")] DummyOrder dummyOrder)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(dummyOrder).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(dummyOrder);
        //}

        //// GET: ProductOrders/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DummyOrder dummyOrder = db.DummyOrders.Find(id);
        //    if (dummyOrder == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(dummyOrder);
        //}

        //// POST: ProductOrders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    DummyOrder dummyOrder = db.DummyOrders.Find(id);
        //    db.DummyOrders.Remove(dummyOrder);
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
