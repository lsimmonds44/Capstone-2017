﻿using System;
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
        /// Ariel Sigo
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
            Supplier sup = supMgr.RetrieveSupplierByUserId(userId);
            return View(plMgr.RetrieveSupplierProductLotsBySupplier(sup));
        }

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created: 
        /// 2017/04/29
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
            Supplier sup = supMgr.RetrieveSupplierByUserId(userId);
            var lot = plMgr.RetrieveSupplierProductLotsBySupplier(sup).Find(i => i.SupplierProductLotId == (int)id);
            if (lot == null)
            {
                return HttpNotFound();
            }
            return View(lot);
        }

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// GET: /SupplierGoods/Create
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
        /// Ariel Sigo
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
        /// Ariel Sigo
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
                // TODO: Write backend to update a product lot
                //db.Entry(supplierinvoice).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lot);
        }

        /// <summary>
        /// Ariel Sigo
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
        /// Ariel Sigo
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
