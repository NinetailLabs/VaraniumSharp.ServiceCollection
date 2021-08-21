using System.Collections.Generic;
using FluentAssertions;
using Moq;
using VaraniumSharp.ServiceCollection.Tests.Fixtures;
using VaraniumSharp.ServiceCollection.Wrappers;
using Xunit;

namespace VaraniumSharp.ServiceCollection.Tests.Wrappers
{
    public class FactoryContainerWrapperTests
    {
        [Fact]
        public void ResolvingAServiceWorksCorrectly()
        {
            // arrange
            var container = new ServiceProviderFixture();
            var dummyObject = new Mock<ITestHelper>();
            container.EntriesToReturns.Add(dummyObject.Object);
            var sut = new FactoryContainerWrapper(container);

            // act
            var _ = sut.Resolve<ITestHelper>();

            // assert
            container.GetServiceRequests.Count.Should().Be(1);
            container.GetServiceRequests[0].Should().Be(typeof(ITestHelper));
        }

        [Fact]
        public void ResolvingAServiceByTypeWorksCorrectly()
        {
            // arrange
            var container = new ServiceProviderFixture();
            var dummyObject = new Mock<ITestHelper>();
            container.EntriesToReturns.Add(dummyObject.Object);
            var sut = new FactoryContainerWrapper(container);

            // act
            var _ = sut.Resolve(typeof(ITestHelper));

            // assert
            container.GetServiceRequests.Count.Should().Be(1);
            container.GetServiceRequests[0].Should().Be(typeof(ITestHelper));
        }

        [Fact]
        public void ResolvingManyServicesWorksCorrectly()
        {
            // arrange
            var container = new ServiceProviderFixture();
            var dummyObject = new Mock<ITestHelper>();
            container.EntriesToReturns.Add(new List<ITestHelper>{ dummyObject.Object });
            var sut = new FactoryContainerWrapper(container);

            // act
            var _ = sut.ResolveMany<ITestHelper>();

            // assert
            container.GetServiceRequests.Count.Should().Be(1);
            container.GetServiceRequests[0].Should().Be(typeof(IEnumerable<ITestHelper>));
        }
    }
}