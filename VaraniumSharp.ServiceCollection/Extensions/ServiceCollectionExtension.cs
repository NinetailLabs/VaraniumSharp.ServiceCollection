using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace VaraniumSharp.ServiceCollection.Extensions
{

    /// <summary>
    /// Extension methods for <seealso cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionExtension
    {
        #region Public Methods

        /// <summary>
        /// Adds a singleton that has already been registered as part of a base type and register it again as itself
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="type">The registered type</param>
        /// <param name="baseType">The type that T is derived from, can be the base class or base interface.</param>
        /// <returns>the IServiceCollection used to register the interface with.</returns>
        public static IServiceCollection ReUseSingleton(this IServiceCollection services, Type type, Type baseType)
        {
            services.AddSingleton(type, a =>
            {
                var entries = a.GetServices(baseType);
                return entries.First(x => x.GetType() == type);
            });
            return services;
        }

        #endregion
    }
}