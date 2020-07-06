[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Web.App_Start.NinjectWebCommon), "Stop")]

namespace Web.App_Start
{
    using System;
    using System.Web;
    using System.Web.Http;
    using Common;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Ninject.Web.WebApi;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
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

                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IAuthorisation>().To<Authorisation>().InRequestScope();
            kernel.Bind<IAuthorisationLegacyProvider>().To<AuthorisationLegacyProvider>().InRequestScope();
            kernel.Bind<IContextProvider>().To<ContextProvider>().InRequestScope();
            kernel.Bind<ICachedContextProvider>().To<CachedContextProvider>().InRequestScope();
            kernel.Bind<IAuthorisationProviderFactory>().ToMethod(ctx => new AuthorisationProviderFactory(ctx.Kernel)).InRequestScope();

            // option 1
            // kernel.Bind<IAuthorisationComparisionProvider>().To<AuthorisationComparisionProvider>().InRequestScope();
            
            // option 2
            kernel.Bind<IAuthorisationComparisionProvider>().ToMethod(ctx=>
            {
                var legacyProvider = ctx.Kernel.Get<IAuthorisationLegacyProvider>();
                var cachedContextProvider = ctx.Kernel.Get<ICachedContextProvider>();
                return new AuthorisationComparisionProvider(cachedContextProvider, legacyProvider);
            }).InRequestScope();
        }
    }
}