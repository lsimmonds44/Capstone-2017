using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentationLayer.Controllers
{
    /// <summary>
    /// Natacha Ilunga
    /// 
    /// Created:
    /// 2017/23/04
    /// 
    /// Main Menu for Supplier and relation Function
    /// </summary>
    public class SupplierController : Controller
    {
        
        /// <summary>
        /// Natcha Ilunga
        ///
        /// 
        /// Created:
        /// 2017/23/04
        /// 
        /// GET: Supplier
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}