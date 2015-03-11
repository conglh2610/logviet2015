[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Oas.LV2015.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Oas.LV2015.App_Start.NinjectWebCommon), "Stop")]

namespace Oas.LV2015.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Oas.Infrastructure.Services;
    using System.Web.Http;
    using Ninject.Web.WebApi;
    using Oas.Infrastructure;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
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

                // Set Web API Resolver
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
            #region core services
            kernel.Bind<IBusinessService>().To<BusinessService>();
            kernel.Bind<IImageService>().To<ImageService>();
            #endregion

            #region CarRenting services
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            kernel.Bind<ICarService>().To<CarService>();
            kernel.Bind<ICarModelService>().To<CarModelService>();
            kernel.Bind<ICarCategoryService>().To<CarCategoryService>();
            kernel.Bind<ICarItemService>().To<CarItemService>();
            kernel.Bind<IDriverService>().To<DriverService>();
            #endregion

            #region ECenter Services
            kernel.Bind<IStudentService>().To<StudentService>();
            kernel.Bind<IAreaService>().To<AreaService>();
            kernel.Bind<IRoomService>().To<RoomService>();
            #endregion
        }
    }
}
