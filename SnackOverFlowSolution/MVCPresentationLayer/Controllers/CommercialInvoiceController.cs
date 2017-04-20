using DataObjects;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentationLayer.Controllers
{
    public class CommercialInvoiceController : Controller
    {
        ICommercialInvoiceManager _commercialInvoiceManager;

        public CommercialInvoiceController(ICommercialInvoiceManager _commercialInvoiceManager)
        {
            this._commercialInvoiceManager = _commercialInvoiceManager;
        }
        // GET: CommercialInvoice
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
    }
}