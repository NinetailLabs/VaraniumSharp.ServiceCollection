using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using VaraniumSharp.ServiceCollection.Tests.Fixtures;
using Xunit;

namespace VaraniumSharp.ServiceCollection.Tests
{
    public class ContainerSetupTests
    {
        [Fact]
        public void TryingToResolveEntryThrowsAnException()
        {
            // arrange
            var serviceCollectionDummy = new Mock<IServiceCollection>();

            var sut = new ContainerSetup(serviceCollectionDummy.Object);

            var act = new Action(() => sut.Resolve<DisposableDummy>());

            // act
            // assert
            act.Should().Throw<NotImplementedException>();
        }
    }
}
