using System;
using System.Collections.Generic;
using System.Linq;
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
        public void ClassWithMultipleConstructorsIsRegisteredCorrectly()
        {
            // arrange
            var serviceDummy = new Mock<IServiceCollection>();
            var sut = new ContainerSetup(serviceDummy.Object);
            var act = new Action(() => sut.RetrieveClassesRequiringRegistration(true));

            // act
            // assert
            act.Should().NotThrow<Exception>();
        }

        [Fact]
        public void ConcretionClassesAreResolvedCorrectly()
        {
            // arrange
            var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            var sut = new ContainerSetup(serviceCollection);

            // act
            sut.RetrieveConcretionClassesRequiringRegistration(true);

            // assert
            var services = serviceCollection.BuildServiceProvider();
            var resolvedClass = ((IEnumerable<BaseClassDummy>)services.GetService(typeof(IEnumerable<BaseClassDummy>))).ToList();
            resolvedClass.Count.Should().Be(1);
            resolvedClass.First().GetType().Should().Be(typeof(InheritorClassDummy));
        }

        [Fact]
        public void ConcretionClassesCorrectlyApplyReuse()
        {
            // arrange
            var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            var sut = new ContainerSetup(serviceCollection);

            // act
            sut.RetrieveConcretionClassesRequiringRegistration(true);

            // assert
            var services = serviceCollection.BuildServiceProvider();
            var resolvedClasses = ((IEnumerable<ITestInterfaceDummy>) services.GetService(typeof(IEnumerable<ITestInterfaceDummy>))).ToList();
            var secondResolve = ((IEnumerable<ITestInterfaceDummy>)services.GetService(typeof(IEnumerable<ITestInterfaceDummy>))).ToList();
            resolvedClasses.Should().BeEquivalentTo(secondResolve);
        }

        [Fact]
        public void ConcretionClassesFromInterfaceAreCorrectlyResolved()
        {
            // arrange
            var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            var sut = new ContainerSetup(serviceCollection);

            // act
            sut.RetrieveConcretionClassesRequiringRegistration(true);

            // assert
            var services = serviceCollection.BuildServiceProvider();
            var resolvedClasses = ((IEnumerable<ITestInterfaceDummy>)services.GetService(typeof(IEnumerable<ITestInterfaceDummy>))).ToList();
            resolvedClasses.Count.Should().Be(2);
            resolvedClasses.Should().Contain(x => x.GetType() == typeof(ImplementationClassDummy));
            resolvedClasses.Should().Contain(x => x.GetType() == typeof(ImplementationClassTooDummy));
        }

        [Fact]
        public void ConcretionClassWithMultipleConstructorsIsRegisteredCorrectly()
        {
            // arrange
            var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            var sut = new ContainerSetup(serviceCollection);
            var act = new Action(() => sut.RetrieveConcretionClassesRequiringRegistration(true));

            // act
            // assert
            act.Should().NotThrow<Exception>();
        }

        [Fact]
        public void MultiTypeRegistrationSingletonsWorkCorrectly()
        {
            // arrange
            var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            var sut = new ContainerSetup(serviceCollection);

            // act
            sut.RetrieveConcretionClassesRequiringRegistration(true);

            // assert
            var services = serviceCollection.BuildServiceProvider();
            var resolvedClasses = ((IEnumerable<ITestInterfaceDummy>)services.GetService(typeof(IEnumerable<ITestInterfaceDummy>))).ToList();
            var interfaceResolvedClass = resolvedClasses.FirstOrDefault(t => t.GetType() == typeof(ImplementationClassDummy));
            var directlyResolvedClass = services.GetService(typeof(ImplementationClassDummy));

            interfaceResolvedClass.Should().Be(directlyResolvedClass);
        }

        [Fact]
        public void SingletonRegistrationsAreResolvedCorrectly()
        {
            // arrange
            var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            var sut = new ContainerSetup(serviceCollection);

            // act
            sut.RetrieveClassesRequiringRegistration(true);

            // assert
            var services = serviceCollection.BuildServiceProvider();
            var resolvedClass = services.GetService<SingletonDummy>();
            var secondResolve = services.GetService<SingletonDummy>();
            resolvedClass.Should().Be(secondResolve);
        }

        [Fact]
        public void TransientRegistrationsAreResolvedCorrectly()
        {
            // arrange
            var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            var sut = new ContainerSetup(serviceCollection);

            // act
            sut.RetrieveClassesRequiringRegistration(true);

            // assert
            var services = serviceCollection.BuildServiceProvider();
            var resolvedClass = services.GetService<AutoRegistrationDummy>();
            var secondResolve = services.GetService<AutoRegistrationDummy>();
            resolvedClass.Should().NotBe(secondResolve);
        }

        [Fact]
        public void TryingToResolveAServiceThrowsANotSupportedException()
        {
            // arrange
            var serviceCollectionDummy = new Mock<IServiceCollection>();

            var sut = new ContainerSetup(serviceCollectionDummy.Object);

            var act = new Action(() => sut.Resolve<DisposableDummy>());

            // act
            // assert
            act.Should().Throw<NotSupportedException>();
        }

        [Fact]
        public void TryingToResolveMultipleServicesThrowsANotSupportedException()
        {
            // arrange
            var serviceCollectionDummy = new Mock<IServiceCollection>();

            var sut = new ContainerSetup(serviceCollectionDummy.Object);

            var act = new Action(() => sut.ResolveMany<DisposableDummy>());

            // act
            // assert
            act.Should().Throw<NotSupportedException>();
        }
    }
}
