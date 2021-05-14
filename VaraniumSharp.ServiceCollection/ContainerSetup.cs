using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using VaraniumSharp.Attributes;
using VaraniumSharp.DependencyInjection;
using VaraniumSharp.Enumerations;
using VaraniumSharp.ServiceCollection.Extensions;

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
            throw new System.NotSupportedException(ResolveErrorMessage);
        }

        /// <inheritdoc />
        public override IEnumerable<TService> ResolveMany<TService>()
        {
            throw new System.NotSupportedException(ResolveErrorMessage);
        }

        #endregion

        #region Private Methods

        /// <inheritdoc />
        protected override void RegisterClasses()
        {
            foreach (var @class in ClassesToRegister)
            {
                var registrationAttribute =
                    (AutomaticContainerRegistrationAttribute)
                    @class.GetCustomAttribute(typeof(AutomaticContainerRegistrationAttribute));

                if (registrationAttribute.Reuse == ServiceReuse.Singleton)
                {
                    _services.AddSingleton(registrationAttribute.ServiceType, @class);
                }
                else if (registrationAttribute.Reuse == ServiceReuse.Default)
                {
                    _services.AddTransient(registrationAttribute.ServiceType, @class);
                }
                else
                {
                    throw new NotImplementedException($"Reuse type {registrationAttribute.Reuse} is not implemented for this ContainerSetup");
                }
            }
        }

        /// <inheritdoc />
        protected override void RegisterConcretionClasses()
        {
            foreach (var @class in ConcretionClassesToRegister)
            {
                var registrationAttribute =
                    (AutomaticConcretionContainerRegistrationAttribute)
                    @class.Key.GetCustomAttribute(typeof(AutomaticConcretionContainerRegistrationAttribute));

                @class.Value.ForEach(x =>
                {
                    if (registrationAttribute.Reuse == ServiceReuse.Singleton)
                    {
                        _services
                            .AddSingleton(@class.Key, x)
                            .ReUseSingleton(x, @class.Key);
                    }
                    else if (registrationAttribute.Reuse == ServiceReuse.Default)
                    {
                        _services.AddTransient(@class.Key, x);
                        _services.AddTransient(x, x);
                    }
                    else
                    {
                        throw new NotImplementedException($"Reuse type {registrationAttribute.Reuse} is not implemented for this ContainerSetup");
                    }
                });

            }
        }

        #endregion

        #region Variables

        /// <summary>
        /// Error message to show when the user tries to resolve an entry from the container
        /// </summary>
        private const string ResolveErrorMessage = "IServiceCollection does not support resolving during setup time. To resolve services from the container get the Services instance from the host after configuration is completed";

        /// <summary>
        /// ServiceCollection instance
        /// </summary>
        private readonly IServiceCollection _services;

        #endregion
    }
}