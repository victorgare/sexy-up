using SexyUp.ApplicationCore.Interfaces.Repository;
using SexyUp.Infrastructure.Repository;
using SexyUp.Web.Controllers;
using System;
using SexyUp.ApplicationCore.Interfaces.Service;
using Unity;
using Unity.Injection;
using SexyUp.ApplicationCore.Services;

namespace SexyUp.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            container.RegisterType<AccountController>(new InjectionConstructor());

            #region Services

            container.RegisterType<IProductService, ProductService>();

            #endregion

            #region Repositories

            container.RegisterType<IProductRepository, ProductRepository>();

            #endregion
        }
    }
}