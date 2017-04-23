using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentationLayer.Controllers
{
    /// <summary>
    /// Created by Natacha Ilunga
    /// 4/23/17
    /// 
    /// Main Menu for Supplier en relation Fonction
    /// </summary>
    public class SupplierController : Controller
    {
        // GET: Supplier
        public ActionResult Index()
        {
            return View();
        }
    }
}