using Mentor.Models;
using Mentor.Models.Repositories;
using Mentor.Models.Repositories.AbstractInterfaces;
using Mentor.Models.Repositories.ConcreteImplementation;
using Mentor.Models.Repositories.Interfaces;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Mentor.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Mentor.App_Start.NinjectWebCommon), "Stop")]

namespace Mentor.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

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
                //unit of work /// constructor
                kernel.Bind<ApplicationDbContext>().ToSelf().InRequestScope();
                kernel.Bind<AbstractIRepository<Program>>().To<ProgramRepository>().WithConstructorArgument("context", context => kernel.Get<ApplicationDbContext>());
                kernel.Bind<AbstractIRepository<User>>().To<UserRepository>().WithConstructorArgument("context", context => kernel.Get<ApplicationDbContext>());
                kernel.Bind<AbstractIRepository<Interest>>().To<InterestRepository>().WithConstructorArgument("context", context => kernel.Get<ApplicationDbContext>());

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
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
        }        
    }
}
