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

    /// <summary>
    /// Ariel Sigo
    /// 
    /// Created:
    /// 2017/04/29
    /// 
    /// Ninject Dependency Resolver
    /// </summary>
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// </summary>
        /// <param name="kernelParam"></param>
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        /// <summary>
        /// Ariel Sigo
        /// 
        /// Created:
        /// 2017/04/29
        /// 
        /// Adds Binding to the interfaces.
        /// </summary>
        private void AddBindings()
        {
            kernel.Bind<IProductManager>().To<ProductManager>();
            kernel.Bind<ISupplierManager>().To<SupplierManager>();
            kernel.Bind<IAgreementManager>().To<AgreementManager>();
            kernel.Bind<IUserManager>().To<UserManager>();
            kernel.Bind<ICompanyOrderManager>().To<CompanyOrderManager>();
            kernel.Bind<IUserCartManager>().To<UserCartManager>();
            kernel.Bind<ISupplierInvoiceManager>().To<SupplierInvoiceManager>();
            kernel.Bind<ICustomerManager>().To<CustomerManager>();
            kernel.Bind<ICommercialInvoiceManager>().To<CommercialInvoiceManager>();
            kernel.Bind<ISupplierProductLotManager>().To<SupplierProductLotManager>();
            kernel.Bind<IProductOrderManager>().To<ProductOrderManager>();
            kernel.Bind<ICustomerOrderManager>().To<CustomerOrderManager>();
            //kernel.Bind<IUserCartManager>().To<UserCartManager>();
        }
    }
}