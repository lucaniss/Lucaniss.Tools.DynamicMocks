using System;
using Lucaniss.Tools.DynamicMocks.Exceptions;
using Lucaniss.Tools.DynamicMocks.Tests.Data;
using Lucaniss.Tools.DynamicMocks.Tests.Data.Interfaces;
using Lucaniss.Tools.DynamicMocks.Tests.Data.Interfaces.Inheritance;
using Lucaniss.Tools.DynamicMocks.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;


// ReSharper disable UnusedVariable

namespace Lucaniss.Tools.DynamicMocks.Tests
{
    [TestClass]
    public class MockCreateTest
    {
        [TestMethod]
        public void CreateMock_WhenParameterTypeIsClass_ThenThrowException()
        {
            // Act
            Action action = () =>
            {
                var mock = Mock.Create<TestClass>();
            };

            // Assert
            AssertException.Throws<MockException>(action, ex => ex.Error == MockExceptionErrors.TypeIsNotInterface);
        }


        [TestMethod]
        public void CreateMock_WhenParameterTypeIsInterface_ThenMockIsCreated()
        {
            // Act
            var mock = Mock.Create<ITestInterface>();

            // Assert
            Assert.IsNotNull(mock.Instance);
            Assert.IsInstanceOfType(mock.Instance, typeof (ITestInterface));
        }

        [TestMethod]
        public void CreateMock_WhenParameterTypeIsGenericInterface_ThenMockIsCreated()
        {
            // Act
            var mock = Mock.Create<ITestGenericInterface<SimpleReferenceType, SimpleValueType>>();

            // Assert
            Assert.IsNotNull(mock.Instance);
            Assert.IsInstanceOfType(mock.Instance, typeof (ITestGenericInterface<SimpleReferenceType, SimpleValueType>));
        }

        [TestMethod]
        public void CreateMock_WhenParameterTypeIsBaseInterface_ThenMockIsCreated()
        {
            // Arrange
            var mock = Mock.Create<IBaseInterface>();

            const String value = "Base";
            mock.SetupMethod(m => m.EchoBase()).Callback(value);

            // Act
            var result = mock.Instance.EchoBase();

            // Assert
            Assert.AreEqual(value, result);
        }

        [TestMethod]
        public void CreateMock_WhenParameterTypeIsDerivedInterface_ThenMockIsCreated()
        {
            // Arrange
            var mock = Mock.Create<IDerivedInterface>();

            const String valueBase = "Base";
            const String valueDerived = "Derived";

            mock.SetupMethod(m => m.EchoBase()).Callback(valueBase);
            mock.SetupMethod(m => m.EchoDerived()).Callback(valueDerived);

            // Act
            var resultBase = mock.Instance.EchoBase();
            var resultDerived = mock.Instance.EchoDerived();

            // Assert
            Assert.AreEqual(valueBase, resultBase);
            Assert.AreEqual(valueDerived, resultDerived);
        }
    }
}