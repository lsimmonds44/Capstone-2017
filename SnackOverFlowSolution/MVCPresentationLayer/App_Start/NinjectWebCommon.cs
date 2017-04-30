[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MVCPresentationLayer.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MVCPresentationLayer.App_Start.NinjectWebCommon), "Stop")]

namespace MVCPresentationLayer.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    /// <summary>
    /// Ariel Sigo
    /// Udpated:
    /// 2017/04/29
    /// Ninject Web Common
    /// </summary>
    public static class NinjectWebCommon 
    {
        /// <summary>
        /// Ariel Sigo
        /// Updated:
        /// 2017/04/29
        /// Creates a read only isntance of BootStrapper
        /// </summary>
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Ariel Sigo
        /// Updated:
        /// 2017/04/29
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Ariel Sigo
        /// Updated:
        /// 2017/04/29
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Ariel Sigo
        /// Updated:
        /// 2017/04/29
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Ariel Sigo
        /// Updated:
        /// 2017/04/29
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            System.Web.Mvc.DependencyResolver.SetResolver(new MVCPresentationLayer.Infrastructure.NinjectDependencyResolver(kernel));
        }        
    }
}
