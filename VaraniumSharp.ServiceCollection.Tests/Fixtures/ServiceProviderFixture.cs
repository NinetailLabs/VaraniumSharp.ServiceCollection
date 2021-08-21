using System;
using System.Collections.Generic;
using System.Linq;

namespace VaraniumSharp.ServiceCollection.Tests.Fixtures
{
    public class ServiceProviderFixture : IServiceProvider
    {
        #region Properties

        public List<object> EntriesToReturns { get; } = new();

        public List<Type> GetServiceRequests { get; }= new();

        #endregion

        #region Public Methods

        public object GetService(Type serviceType)
        {
            GetServiceRequests.Add(serviceType);
            var entryToReturn = EntriesToReturns.First();
            EntriesToReturns.Remove(entryToReturn);
            return entryToReturn;
        }

        #endregion
    }
}