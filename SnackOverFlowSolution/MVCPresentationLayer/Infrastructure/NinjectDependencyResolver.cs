using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using LogicLayer;

namespace MVCPresentationLayer.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IProductManager>().To<ProductManager>();
            kernel.Bind<ISupplierManager>().To<SupplierManager>();
            kernel.Bind<IAgreementManager>().To<AgreementManager>();
            kernel.Bind<IUserManager>().To<UserManager>();
            kernel.Bind<ICompanyOrderManager>().To<CompanyOrderManager>();
            kernel.Bind<IUserCartManager>().To<UserCartManager>();
            kernel.Bind<ISupplierInvoiceManager>().To<SupplierInvoiceManager>();
            //kernel.Bind<IUserCartManager>().To<UserCartManager>();
        }
    }
}