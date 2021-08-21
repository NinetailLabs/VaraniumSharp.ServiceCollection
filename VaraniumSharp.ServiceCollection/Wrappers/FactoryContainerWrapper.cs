using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using VaraniumSharp.Attributes;
using VaraniumSharp.Interfaces.DependencyInjection;

namespace VaraniumSharp.ServiceCollection.Wrappers
{
    /// <summary>
    /// Wrapper to help level 2 libraries access the container in an abstract way.
    /// </summary>
    [AutomaticContainerRegistration(typeof(IContainerFactoryWrapper))]
    public class FactoryContainerWrapper : IContainerFactoryWrapper
    {
        #region Constructor

        /// <summary>
        /// DI Constructor
        /// </summary>
        public FactoryContainerWrapper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public TService Resolve<TService>()
        {
            return _serviceProvider.GetService<TService>();
        }

        /// <inheritdoc />
        public IEnumerable<TService> ResolveMany<TService>()
        {
            return _serviceProvider.GetServices<TService>();
        }

        #endregion

        #region Variables

        /// <summary>
        /// ServiceCollection instance
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        #endregion
    }
}