using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using VaraniumSharp.DependencyInjection;

namespace VaraniumSharp.ServiceCollection
{
    /// <summary>
    /// Assist with setting up the <see cref="IServiceCollection"/> and register all classes that implement the AutomaticContainerRegistrationAttribute
    /// </summary>
    public class ContainerSetup : AutomaticContainerRegistration
    {
        #region Constructor

        /// <summary>
        /// Create the container setup and set the <see cref="IServiceCollection"/> to use for registration
        /// </summary>
        /// <param name="services">ServiceCollection to use for registration</param>
        public ContainerSetup(IServiceCollection services)
        {
            _services = services;
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public override TService Resolve<TService>()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public override IEnumerable<TService> ResolveMany<TService>()
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Private Methods

        /// <inheritdoc />
        protected override void RegisterClasses()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        protected override void RegisterConcretionClasses()
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Variables

        /// <summary>
        /// ServiceCollection instance
        /// </summary>
        private readonly IServiceCollection _services;

        #endregion
    }
}