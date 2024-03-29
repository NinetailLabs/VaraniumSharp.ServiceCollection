﻿using System;
using VaraniumSharp.Attributes;
using VaraniumSharp.Enumerations;

// ReSharper disable UnusedAutoPropertyAccessor.Global - Test fixtures - Required for testing even when not used
// ReSharper disable MemberCanBePrivate.Global - Test fixtures - Access required so they function correctly
// ReSharper disable UnusedMember.Global - Test fixtures used via AutomaticContainerRegistration

namespace VaraniumSharp.ServiceCollection.Tests.Fixtures
{
    [AutomaticConcretionContainerRegistration]
    public interface IDisposableConcretionBase : IDisposable
    {}

    public class DisposableConcretionImplementation : IDisposableConcretionBase
    {
        #region Public Methods

        public void Dispose()
        {
        }

        #endregion
    }

    public class SecondaryDisposableConcretionImplementation : IDisposableConcretionBase
    {
        #region Public Methods

        public void Dispose()
        {
        }

        #endregion
    }

    [AutomaticContainerRegistration(typeof(DisposableDummy))]
    public class DisposableDummy : IDisposable
    {
        #region Public Methods

        public void Dispose()
        {
        }

        #endregion
    }

    [AutomaticContainerRegistration(typeof(AutoRegistrationDummy))]
    public class AutoRegistrationDummy
    {}

    [AutomaticContainerRegistration(typeof(SingletonDummy), ServiceReuse.Singleton)]
    public class SingletonDummy
    {}

    [AutomaticConcretionContainerRegistration]
    public abstract class BaseClassDummy
    {}

    public class InheritorClassDummy : BaseClassDummy
    {}

    [AutomaticConcretionContainerRegistration(ServiceReuse.Singleton)]
    public interface ITestInterfaceDummy
    {}

    public class ImplementationClassDummy : ITestInterfaceDummy
    {}

    public class ImplementationClassTooDummy : ITestInterfaceDummy
    {}

    [AutomaticContainerRegistration(typeof(MultiConstructorClass), ServiceReuse.Default, true)]
    public class MultiConstructorClass
    {
        #region Constructor

        public MultiConstructorClass()
        {
        }

        public MultiConstructorClass(AutoRegistrationDummy autoRegistrationDummy)
        {
            AutoRegistrationDummy = autoRegistrationDummy;
        }

        #endregion

        #region Properties

        public AutoRegistrationDummy AutoRegistrationDummy { get; }

        #endregion
    }

    [AutomaticConcretionContainerRegistration(ServiceReuse.Default, true)]
    public abstract class MultiConstructorConcretionClassDummy
    {}

    public class MultiConstructorConcretionInheritor : MultiConstructorConcretionClassDummy
    {
        #region Constructor

        public MultiConstructorConcretionInheritor()
        {
        }

        public MultiConstructorConcretionInheritor(AutoRegistrationDummy autoRegistrationDummy)
        {
            AutoRegistrationDummy = autoRegistrationDummy;
        }

        #endregion

        #region Properties

        public AutoRegistrationDummy AutoRegistrationDummy { get; }

        #endregion
    }

    [AutomaticContainerRegistration(typeof(AutoResolve), AutoResolveAtStartup = true)]
    public class AutoResolve
    {
        #region Constructor

        public AutoResolve()
        {
            TimesResolved++;
        }

        #endregion

        #region Properties

        public static int TimesResolved { get; private set; }

        #endregion
    }

    [AutomaticConcretionContainerRegistration(ServiceReuse.Singleton, AutoResolveAtStartup = true)]
    public abstract class AutoResolveBase
    {
        #region Properties

        public int TimesResolved { get; protected init; }

        #endregion
    }

    public class AutoResolve1 : AutoResolveBase
    {
        #region Constructor

        public AutoResolve1()
        {
            TimesResolved++;
        }

        #endregion
    }

    public class AutoResolve2 : AutoResolveBase
    {
        #region Constructor

        public AutoResolve2()
        {
            TimesResolved++;
        }

        #endregion
    }
}