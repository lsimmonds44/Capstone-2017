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
using System.Threading.Tasks;

namespace MVCPresentationLayer.Controllers
{
    /// <summary>
    /// Ariel Sigo
    /// 
    /// Created: 
    /// 2017/04/29
    /// 
    /// Supplier With Agreements Controller
    /// </summary>
    public class SupplierWithAgreementsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private ISupplierManager _supplierManager;
        private IProductManager _productManager;
        private IAgreementManager _agreementManager;
        private IUserManager _userManager;


        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// </summary>
        /// <param name="supplierManager"></param>
        /// <param name="productManager"></param>
        /// <param name="agreementManager"></param>
        /// <param name="userManager"></param>
        public SupplierWithAgreementsController(ISupplierManager supplierManager, IProductManager productManager, IAgreementManager agreementManager, IUserManager userManager)
        {
            _supplierManager = supplierManager;
            _productManager = productManager;
            _agreementManager = agreementManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// GET: SupplierWithAgreements
        /// </summary>
        /// <returns>View of Suppliers with Agreements</returns>
        [Authorize]
        public ActionResult Index()
        {
            return View(_supplierManager.RetrieveSuppliersWithAgreements());
        }

        //// GET: SupplierWithAgreements/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SupplierWithAgreements supplierWithAgreements = db.SupplierWithAgreements.Find(id);
        //    if (supplierWithAgreements == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(supplierWithAgreements);
        //}

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// GET: SupplierWithAgreements/Create
        /// </summary>
        /// <returns>View of Dummy Product List</returns>
        public ActionResult Create()
        {
            //if (User.IsInRole("Supplier"))
            //{
            //    ViewBag.Account = "supplier";
            //    return View("AlreadyAccepted");
            //}
            List<Product> products = _productManager.ListProducts();
            //SupplierWithAgreements dummy = new SupplierWithAgreements()
            SupplierApplicantViewModel dummy = new SupplierApplicantViewModel()
            {
                Agreements = new List<AgreementWithProductName>()
            };
            foreach (Product p in products)
            {
                dummy.Agreements.Add(new AgreementWithProductName()
                {
                    ProductId = p.ProductId,
                    ProductName = p.Name
                });
            }
            ViewBag.Message = "";
            return View(dummy);
        }

        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// POST: SupplierWithAgreements/Create
        /// </summary>
        /// <param name="supplierApplicant"></param>
        /// <returns>View of Supplier Applicant</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "FarmTaxID,UserId,FarmName,FarmAddress,FarmCity,FarmState,Agreements")] SupplierWithAgreements supplierWithAgreements)
        public async Task<ActionResult> Create([Bind(Include = 
            "FirstName,LastName,Phone,AddressLineOne,AddressLineTwo,City,State,Zip,EmailAddress,UserName,Password,ConfirmPassword,FarmTaxID,FarmName,FarmAddress,FarmCity,FarmState,ProductIDs")] 
            SupplierApplicantViewModel supplierApplicant)
        {



            supplierApplicant.Agreements = new List<AgreementWithProductName>();
            if (supplierApplicant.ProductIDs != null)
            {
                for (int i = 0; i < supplierApplicant.ProductIDs.Length; i++)
                {
                    supplierApplicant.Agreements.Add(new AgreementWithProductName()
                    {
                        ProductId = supplierApplicant.ProductIDs[i],
                        ProductName = _productManager.RetrieveProductById(supplierApplicant.ProductIDs[i]).Name
                    });
                }
            }
            if (ModelState.IsValid)
            {
                //Need to first make a user with the information, then the supplier
                User newUser = new User()
                {
                    FirstName = supplierApplicant.FirstName,
                    LastName = supplierApplicant.LastName,
                    City = supplierApplicant.City,
                    State = supplierApplicant.State,
                    AddressLineOne = supplierApplicant.AddressLineOne,
                    AddressLineTwo = supplierApplicant.AddressLineTwo,
                    Zip = supplierApplicant.Zip,
                    EmailAddress = supplierApplicant.EmailAddress,
                    EmailPreferences = true,
                    Active = true,
                    Phone = supplierApplicant.Phone,
                    UserName = supplierApplicant.UserName
                };

                try
                {
                    string result = _userManager.CreateNewUser(newUser, supplierApplicant.Password, supplierApplicant.ConfirmPassword);
                    if (!("Created".Equals(result)))
                    {
                        ViewBag.Message = result;
                        return View(supplierApplicant);
                    }
                }
                catch (Exception)
                {

                    return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
                }

                // If we are here, it means we created the user
                User newlyCreated = null;
                try
                {
                    newlyCreated = _userManager.RetrieveUserByUserName(newUser.UserName);
                }
                catch (Exception)
                {

                    return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
                }

                if (null == newlyCreated) // this shouldn't ever be true
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }

                // Make a supplier and store the associated data to the db
                SupplierWithAgreements supplierWithAgreements = new SupplierWithAgreements()
                {
                    FarmName = supplierApplicant.FarmName,
                    FarmAddress = supplierApplicant.FarmAddress,
                    FarmCity = supplierApplicant.FarmCity,
                    FarmState = supplierApplicant.FarmState,
                    FarmTaxID = supplierApplicant.FarmTaxID,
                    UserId = newlyCreated.UserId,
                    ProductIDs = supplierApplicant.ProductIDs,
                    Agreements = supplierApplicant.Agreements,
                    Active = true,
                    IsApproved = false
                };
                try
                {


                    if (_supplierManager.ApplyForSupplierAccount(supplierWithAgreements))
                    {
                        supplierWithAgreements.SupplierID = _supplierManager.RetrieveSupplierByUserId(supplierWithAgreements.UserId).SupplierID;
                        foreach (Agreement a in supplierWithAgreements.Agreements)
                        {
                            if (!_agreementManager.CreateAgreementsForSupplier(supplierWithAgreements, _productManager.RetrieveProductById(a.ProductId)))
                            {
                                return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
                            }
                        }

                        RegisterViewModel registerModel = new RegisterViewModel()
                        {
                            Email = newUser.EmailAddress,
                            Password = supplierApplicant.Password,
                            ConfirmPassword = supplierApplicant.ConfirmPassword
                        };
                        

                        var controller = DependencyResolver.Current.GetService<AccountController>();
                        controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);

                        var result = await controller.Register(registerModel);
                        
                        return RedirectToAction("ApplicationSuccess", "Home", new { username = newlyCreated.UserName, supOrCom = true });
                        //return RedirectToAction("Register", "Account", registerModel);
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
                    }
                }
                catch (Exception)
                {

                    return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
                }

            }

            List<Product> products = _productManager.ListProducts();
            supplierApplicant.Agreements = new List<AgreementWithProductName>();
            foreach (Product p in products)
            {
                supplierApplicant.Agreements.Add(new AgreementWithProductName()
                {
                    ProductId = p.ProductId,
                    ProductName = p.Name
                });
            }

            return View(supplierApplicant);
        }

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// GET: SupplierWithAgreements/Edit/5
        /// </summary>
        /// <param name="username"></param>
        /// <returns>View of Supplier with Agreements</returns>
        [Authorize(Roles="Supplier")]
        
        public ActionResult Edit(string username)
        {
            if (username == null || username.Equals(""))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DataObjects.User usr = _userManager.RetrieveUserByUserName(username.ToString());
            SupplierWithAgreements supplierWithAgreements = _supplierManager.RetrieveSupplierWithAgreementsByUserId(usr.UserId);
            if (supplierWithAgreements == null)
            {
                return HttpNotFound();
            }
            supplierWithAgreements.Agreements.RemoveAll(a => a.IsApproved == false);
            return View(supplierWithAgreements);
        }

        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created: 
        /// 2017/04/29
        /// 
        /// POST: SupplierWithAgreements/Edit/5
        /// </summary>
        /// <param name="supplierWithAgreements"></param>
        /// <returns>View of SupplierWithAgreements</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Supplier")]
        public ActionResult Edit([Bind(Include = "FarmTaxID,FarmName,FarmAddress,FarmCity,FarmState")] SupplierWithAgreements supplierWithAgreements)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(supplierWithAgreements).State = EntityState.Modified;
                //db.SaveChanges();
                try
                {
                    DataObjects.User usr = _userManager.RetrieveUserByUserName(User.Identity.Name);
                    if (usr == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    SupplierWithAgreements oldSupplier = _supplierManager.RetrieveSupplierWithAgreementsByUserId(usr.UserId);
                    if (oldSupplier == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    //set unchangable values
                    supplierWithAgreements.SupplierID = oldSupplier.SupplierID;
                    supplierWithAgreements.IsApproved = oldSupplier.IsApproved;
                    supplierWithAgreements.UserId = oldSupplier.UserId;
                    supplierWithAgreements.Active = oldSupplier.Active;
                    supplierWithAgreements.Agreements = oldSupplier.Agreements; // will be modifiable in a different area
                    supplierWithAgreements.ApprovedBy = oldSupplier.ApprovedBy;

                    if (!_supplierManager.UpdateSupplierAccount(oldSupplier, supplierWithAgreements))
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                    }
                }
                catch (Exception)
                {

                    return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
                }

                return RedirectToAction("Index", "Manage", new { Message = MVCPresentationLayer.Controllers.ManageController.ManageMessageId.UpdatedSuccess });
            }
            return View(supplierWithAgreements);
        }

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// GET: SupplierWithAgreements/UpdateAgreements/5
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns>view of updated agreement model</returns>
        [Authorize(Roles="Supplier")]
        public ActionResult UpdateAgreements(int? supplierId)
        {
            if (null == supplierId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                SupplierWithAgreements supplier = _supplierManager.RetrieveSupplierWithAgreementsBySupplierId((int)supplierId);
                if (null == supplier)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (null != supplier.Agreements)
                {
                    supplier.Agreements.RemoveAll(a => a.IsApproved == false);
                }
                

                SupplierUpdateAgreementViewModel model = new SupplierUpdateAgreementViewModel()
                {
                    SupplierId = (int)supplierId,
                    ApprovedAgreements = supplier.Agreements,
                    ProductsToSelect = _productManager.RetrieveProducts()
                };

                if (model.ApprovedAgreements != null)
                {
                    foreach (AgreementWithProductName a in model.ApprovedAgreements)
                    {
                        model.ProductsToSelect.RemoveAll(p => p.ProductId == a.ProductId);
                    }
                }

                return View("UpdateAgreements", model);
                

            }
            catch (Exception)
            {

                return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
            }
            
        }

        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created;
        /// 2017/04/29
        /// 
        ///  POST: SupplierWithAgreements/UpdateAgreements/5
        /// </summary>
        /// <param name="model"></param>
        /// <returns>RedirectToAction("Index", "Manage",</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Supplier")]
        public ActionResult UpdateAgreements([Bind(Include = "ProductIDs")] SupplierUpdateAgreementViewModel model)
        {
            if (model.ProductIDs.Length == 0)
            {
                return RedirectToAction("Index", "Manage");
            }
            SupplierWithAgreements supplier = null;
            try
            {
                DataObjects.User usr = _userManager.RetrieveUserByUserName(User.Identity.Name);
                supplier = _supplierManager.RetrieveSupplierWithAgreementsByUserId(usr.UserId);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable, ex.Message);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable, ex.Message + "\n" + ex.InnerException.Message);
                }
                
            }

            if (null == supplier)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Unable to find supplier");
            }

            try
            {
                foreach (int id in model.ProductIDs)
                {
                    Product product = _productManager.RetrieveProductById(id);
                    _agreementManager.CreateAgreementsForSupplier(supplier, product, null, false);
                }
            }
            catch (Exception ex)
            {

                if (ex.InnerException == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable, ex.Message);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable, ex.Message + "\n" + ex.InnerException.Message);
                }
            }

            return RedirectToAction("Index", "Manage", new { Message = MVCPresentationLayer.Controllers.ManageController.ManageMessageId.AppliedSuccess });
            
        }


        //// GET: SupplierWithAgreements/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SupplierWithAgreements supplierWithAgreements = db.SupplierWithAgreements.Find(id);
        //    if (supplierWithAgreements == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(supplierWithAgreements);
        //}

        //// POST: SupplierWithAgreements/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    SupplierWithAgreements supplierWithAgreements = db.SupplierWithAgreements.Find(id);
        //    db.SupplierWithAgreements.Remove(supplierWithAgreements);
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
