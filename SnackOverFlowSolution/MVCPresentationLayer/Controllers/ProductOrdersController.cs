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
    /// 
    /// Victor Algarin
    /// Created:
    /// 2017/04/21
    /// 
    /// Controller for Product Orders
    /// Note: In order to create the controller, I had to remove nullable option for fields 
    /// under ProductOrder object, as well as the lists under that object
    /// </summary>

    public class ProductOrdersController : Controller
    {
        IProductOrderManager ordMgr = new ProductOrderManager();


        /// <summary>
        /// Victor Algarin
        /// 
        /// Created:
        /// 2017/04/21
        /// 
        ///  GET: ProductOrders
        /// </summary>
        /// <returns>View(Order)</returns>
        public ActionResult Index()
        {
            var order = ordMgr.RetrieveProductOrdersByStatus("Open");
            return View(order);
        }

        /// <summary>
        /// Victor Algarin
        /// 
        /// Created:
        /// 2017/04/21
        /// 
        /// GET: ProductOrders/Details/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View(Order)</returns> 
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

        /// <summary>
        /// Victor Algarin
        /// 
        /// Created:
        /// 2017/04/21
        /// 
        /// GET: ProductOrders/Create
        /// 
        /// </summary>
        /// <returns></returns> 
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Victor Algarin
        /// 
        /// Created:
        /// 2017/04/30
        /// 
        /// POST: ProductOrders/Create
        /// 
        /// </summary>
        /// <returns></returns>
        // POST: ProductOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductOrderId,CustomerId,EmployeeId,OrderTypeId,AddressType,Address1,City,State,Zip,DeliveryTypeId,Amount,OrderDate,DateExpected,Discount,OrderStatusId,HasArrived")] ProductOrder productOrder)
        {
            if (ModelState.IsValid)
            {
                if (ordMgr.createProductOrder(productOrder) != 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
                }
                
            }

            return View(productOrder);
        }

        /// <summary>
        /// Victor Algarin
        /// 
        /// Created:
        /// 2017/04/30
        /// 
        /// GET: ProductOrders/Edit
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: ProductOrders/Edit/5
        public ActionResult Edit(int? id)
        {            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = ordMgr.RetrieveProductOrdersByStatus("Open").Find(o => o.OrderId == (int)id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }


        /// <summary>
        /// Victor Algarin
        /// 
        /// Created:
        /// 2017/05/01
        /// 
        /// POST: ProductOrders/Edit
        /// 
        /// </summary>
        /// <returns></returns>
        // POST: ProductOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductOrderId,CustomerId,EmployeeId,OrderTypeId,AddressType,DeliveryTypeId,Amount,OrderDate,DateExpected,Discount,OrderStatusId,UserAddressId,HasArrived")] ProductOrder newOrder)
        {
            if (ModelState.IsValid)
            {
                var oldOrder = ordMgr.RetrieveProductOrdersByStatus("Open").Find(o => o.OrderId == (int)newOrder.OrderId);
                if (ordMgr.UpdateProductOrder(oldOrder, newOrder) == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
                }                
            }
            return View(newOrder);
        }


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

        /// <summary>
        /// William Flood
        /// 
        /// Created:
        /// 2017/04/27
        /// </summary>
        /// <param name="id"></param>
        /// <returns>RedirectToAction("Index")</returns>
        public ActionResult Save(int? id)
        {
            try
            {
                ordMgr.SaveOrder((int)id);
            }
            catch (Exception ex)
            {
                ViewBag["Error"] = ex.Message + ex.InnerException.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
