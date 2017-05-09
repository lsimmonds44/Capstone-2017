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
using Microsoft.AspNet.Identity;
using System.Diagnostics;

namespace MVCPresentationLayer.Controllers
{
    /// <summary>
    /// Ethan Jorgensen
    /// 
    /// Created: 
    /// 
    /// 2017/04/06
    /// 
    /// Controller logic for supplier product lots
    /// </summary>
    public class SupplierGoodsController : Controller
    {
        private ISupplierProductLotManager plMgr;
        private ISupplierManager supMgr;
        private IUserManager usrMgr;
        public SupplierGoodsController(ISupplierProductLotManager plMgr, ISupplierManager supMgr, IUserManager usrMgr)
        {
            this.plMgr = plMgr;
            this.supMgr = supMgr;
            this.usrMgr = usrMgr;
        }

        /// <summary>
        /// Ethan Jorgensen
        /// 
        /// Created: 
        /// 2017/04/29
        /// 
        /// GET: /SupplierGoods/
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var userId = usrMgr.RetrieveUserByUserName(User.Identity.GetUserName()).UserId;
            Supplier sup;
            try
            {
                sup = supMgr.RetrieveSupplierByUserId(userId);
                Debug.WriteLine("Supplier fetched: here's some pointless data about it " + sup.FarmTaxID);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (sup == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            foreach (SupplierProductLot whatever in plMgr.RetrieveSupplierProductLotsBySupplier(sup))
            {
                Debug.WriteLine(whatever.ProductName + " " + whatever.Price);
            }
            return View(plMgr.RetrieveSupplierProductLotsBySupplier(sup));
        }

        /// <summary>
        /// Dan Brown
        /// 
        /// Created: 
        /// 2017/05/06
        /// 
        ///GET: /SupplierGoods/Details/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View of supplier good lot</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userId = usrMgr.RetrieveUserByUserName(User.Identity.GetUserName()).UserId;
            Supplier sup;
            try
            {
                sup = supMgr.RetrieveSupplierByUserId(userId);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            if (sup == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            var lot = plMgr.RetrieveSupplierProductLotsBySupplier(sup).Find(i => i.SupplierProductLotId == (int)id);
            if (lot == null)
            {
                return HttpNotFound();
            }
            return View(lot);
        }

        /// <summary>
        /// Dan Brown
        /// 
        /// Created:
        /// 2017/05/09
        /// 
        /// GET: /SupplierGoods/Create
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var userId = usrMgr.RetrieveUserByUserName(User.Identity.GetUserName()).UserId;
            Supplier sup;
            try
            {
                sup = supMgr.RetrieveSupplierByUserId(userId);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            SupplierProductLot lot = new SupplierProductLot();
            lot.SupplierId = sup.SupplierID;
            lot.ExpirationDate = DateTime.Now;

            List<Product> productList = new List<Product>();
            ProductManager prodMgr = new ProductManager();
            productList = prodMgr.ListProducts();
            ViewBag.products = productList;
           
            return View(lot);
        }

        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        /// <summary>
        /// Ethan Jorgensen
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// POST: /SupplierGoods/Create
        /// </summary>
        /// <param name="lot"></param>
        /// <returns>View of Lot</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierProductLotId,ExpirationDate,ProductId,Quantity,SupplierId,Price")] SupplierProductLot lot)
        {
            if (ModelState.IsValid)
            {
                plMgr.CreateSupplierProductLot(lot);
                return RedirectToAction("Index");
            }

            return View(lot);
        }

        /// <summary>
        /// Ethan Jorgensen
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View of Lot if successful</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Cast int? to int since we know id is not null
            var lot = plMgr.RetrieveSupplierProductLotById((int)id);
            if (lot == null)
            {
                return HttpNotFound();
            }
            return View(lot);
        }

        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Ethan Jorgensen
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// POST: /SupplierGoods/Edit/5
        /// </summary>
        /// <param name="lot"></param>
        /// <returns>View of Lot</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "SupplierProductLotId,ExpirationDate,ProductId,Quantity,SupplierId,Price")] SupplierProductLot lot)
        {
            if (ModelState.IsValid)
            {
                //plMgr.UpdateSupplierProductLot(lot);
                plMgr.DeleteSupplierProductLot(lot);
                plMgr.CreateSupplierProductLot(lot);
                return RedirectToAction("Index");
            }
            return View(lot);
        }

        /// <summary>
        /// Ethan Jorgensen
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// GET: /SupplierGoods/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View of Lots</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lot = plMgr.RetrieveSupplierProductLotById((int)id);
            if (lot == null)
            {
                return HttpNotFound();
            }
            return View(lot);
        }

        /// <summary>
        /// Ethan Jorgensen
        /// 
        /// Created
        /// 2017/04/29
        /// 
        /// POST: /SupplierGoods/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns>RedirectToAction("Index")</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var lot = plMgr.RetrieveSupplierProductLotById((int)id);
            plMgr.DeleteSupplierProductLot(lot);
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
