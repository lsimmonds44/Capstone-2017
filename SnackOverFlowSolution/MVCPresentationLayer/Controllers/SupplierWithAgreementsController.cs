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
    public class SupplierWithAgreementsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        private ISupplierManager _supplierManager;
        private IProductManager _productManager;
        private IAgreementManager _agreementManager;
        private IUserManager _userManager;

        public SupplierWithAgreementsController(ISupplierManager supplierManager, IProductManager productManager, IAgreementManager agreementManager, IUserManager userManager)
        {
            _supplierManager = supplierManager;
            _productManager = productManager;
            _agreementManager = agreementManager;
            _userManager = userManager;
        }

        [Authorize]
        // GET: SupplierWithAgreements
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

        // GET: SupplierWithAgreements/Create
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

        // POST: SupplierWithAgreements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

                        return RedirectToAction("Index","Home");
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

        //// GET: SupplierWithAgreements/Edit/5
        //public ActionResult Edit(int? id)
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

        //// POST: SupplierWithAgreements/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,SupplierID,IsApproved,ApprovedBy,FarmTaxID,UserId,FarmName,FarmAddress,FarmCity,FarmState,Active")] SupplierWithAgreements supplierWithAgreements)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(supplierWithAgreements).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(supplierWithAgreements);
        //}

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
