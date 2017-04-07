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
    /// Ethan Jorgensen
    /// Created: 04/06/2017
    /// </summary>
    public class SupplierGoodsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        IProductLotManager plMgr = new ProductLotManager();

        // dummy supplier
        Supplier sup = new Supplier() { SupplierID = 10000 };

        // GET: /SupplierGoods/
        public ActionResult Index()
        {
            return View(plMgr.RetrieveProductLotsBySupplier(sup));
        }

        // GET: /SupplierGoods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lot = plMgr.RetrieveProductLotsBySupplier(sup).Find(i => i.ProductLotId == (int)id);
            if (lot == null)
            {
                return HttpNotFound();
            }
            return View(lot);
        }

        // GET: /SupplierGoods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /SupplierGoods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ProductLotId,WarehouseId,SupplierId,LocationId,ProductId,SupplyManagerId,Quantity,AvailableQuantity,Grade,Price,DateReceived,ExpirationDate,ProductName")] ProductLot lot)
        {
            if (ModelState.IsValid)
            {
                //plMgr.AddProductLot(lot);
                return RedirectToAction("Index");
            }

            return View(lot);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Cast int? to int since we know id is not null
            var lot = plMgr.RetrieveProductLotById((int)id);
            if (lot == null)
            {
                return HttpNotFound();
            }
            return View(lot);
        }

        // POST: /SupplierGoods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ProductLotId,WarehouseId,SupplierId,LocationId,ProductId,SupplyManagerId,Quantity,AvailableQuantity,Grade,Price,DateReceived,ExpirationDate,ProductName")] ProductLot lot)
        {
            if (ModelState.IsValid)
            {
                // TODO: Write backend to update a product lot
                //db.Entry(supplierinvoice).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lot);
        }

        // GET: /SupplierGoods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lot = plMgr.RetrieveProductLotById((int)id);
            if (lot == null)
            {
                return HttpNotFound();
            }
            return View(lot);
        }

        // POST: /SupplierGoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var lot = plMgr.RetrieveProductLotById((int)id);
            plMgr.DeleteProductLot(lot);
            return RedirectToAction("Index");
        }

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
