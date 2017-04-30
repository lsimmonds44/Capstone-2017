using DataObjects;
using LogicLayer;
using MVCPresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentationLayer.Controllers
{
    /// <summary>
    /// Ariel Sigo
    /// 
    /// Created:
    /// 2017/04/29
    /// 
    /// Commercial Invoice Controller
    /// </summary>
    public class CommercialInvoiceController : Controller
    {
        ICommercialInvoiceManager _commercialInvoiceManager;

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// </summary>
        /// <param name="_commercialInvoiceManager"></param>
        public CommercialInvoiceController(ICommercialInvoiceManager _commercialInvoiceManager)
        {
            this._commercialInvoiceManager = _commercialInvoiceManager;
        }

        
        
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// GET: CommercialInvoice
        /// </summary>
        /// <returns>View of InvoiceList</returns>
        public ActionResult Index()
        {
            var userName = User.Identity.Name;
            var invoiceList = new List<CommercialInvoice>();
            try
            {
                invoiceList = _commercialInvoiceManager.RetrieveCommercialInvoiceByUserName(userName);
            }
            catch (Exception ex)
            {
                ViewBag["ErrorMessage"] = "Internal error: " + ex.Message;
            }
            return View(invoiceList);
        }

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>view of Invoice Detail</returns>
        public ActionResult Details(int? id)
        {
            var invoiceDetails = new CommercialInvoiceDetails();
            try
            {
                invoiceDetails.CommercialInvoice = _commercialInvoiceManager.RetrieveCommercialInvoiceByInvoiceID((int)id);
                invoiceDetails.CommercialInvoiceLines = _commercialInvoiceManager.RetrieveCommercialInvoiceLinesByInvoiceId((int)id);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Internal error: " + ex.Message;
            }
            return View(invoiceDetails);
            
        }
    }
}