using System;
using Lucaniss.Tools.DynamicMocks.Exceptions;
using Lucaniss.Tools.DynamicMocks.Tests.Data;
using Lucaniss.Tools.DynamicMocks.Tests.Data.Interfaces;
using Lucaniss.Tools.DynamicMocks.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;


// ReSharper disable RedundantAssignment

namespace Lucaniss.Tools.DynamicMocks.Tests
{
    [TestClass]
    public class MockSetupForInterfaceWithFunctionsTest
    {
        [TestMethod]
        public void SetupFunction_WithNoArguments_WhenInvokeMethod_InvokeCallbackAndReturnValue()
        {
            // Arrange
            var mock = Mock.Create<ITestInterfaceWithFunctions>();

            var callbackValue = new Object();

            mock.SetupMethod(m => m.Function()).Callback(() =>
            {
                return callbackValue;
            });

            // Act
            var returnValue = mock.Instance.Function();

            // Assert
            Assert.AreEqual(callbackValue, returnValue);
        }


        [TestMethod]
        public void SetupFunction_WithArgumentOfValueType_WhenSetupNoMatch_ThrowException()
        {
            // Arrange
            var mock = Mock.Create<ITestInterfaceWithFunctions>();

            var parameterValue1 = new SimpleValueType(Guid.NewGuid());
            var parameterValue2 = new SimpleValueType(Guid.NewGuid());

            mock.SetupMethod(m => m.Function(parameterValue1)).Callback<SimpleValueType>(argument =>
            {
                return argument;
            });

            // Act
            Action action = () =>
            {
                mock.Instance.Function(parameterValue2);
            };

            // Assert
            AssertException.Throws<MockException>(action, e => e.Error == MockExceptionErrors.MethodHasNoSetup);
        }

        [TestMethod]
        public void SetupFunction_WithArgumentOfValueType_WhenSetupMatch_InvokeCallbackAndForwardValue()
        {
            // Arrange            
            var mock = Mock.Create<ITestInterfaceWithFunctions>();
            var parameterValue = new SimpleValueType(Guid.NewGuid());

            mock.SetupMethod(m => m.Function(parameterValue)).Callback<SimpleValueType>(argument =>
            {
                return argument;
            });

            // Act
            var returnValue = mock.Instance.Function(parameterValue);

            // Assert
            Assert.AreEqual(parameterValue, returnValue);
            Assert.AreEqual(parameterValue.Id, parameterValue.Id);
        }


        [TestMethod]
        public void SetupFunction_WithArgumentOfReferenceType_WhenSetupNoMatch_ThrowException()
        {
            // Arrange
            var mock = Mock.Create<ITestInterfaceWithFunctions>();

            var parameterValue1 = new SimpleReferenceType(Guid.NewGuid());
            var parameterValue2 = new SimpleReferenceType(Guid.NewGuid());

            mock.SetupMethod(m => m.Function(parameterValue1)).Callback<SimpleReferenceType>(argument =>
            {
                return argument;
            });

            // Act
            Action action = () =>
            {
                mock.Instance.Function(parameterValue2);
            };

            // Assert
            AssertException.Throws<MockException>(action, e => e.Error == MockExceptionErrors.MethodHasNoSetup);
        }

        [TestMethod]
        public void SetupFunction_WithArgumentOfReferenceType_WhenSetupMatch_InvokeCallbackAndForwardValue()
        {
            // Arrange            
            var mock = Mock.Create<ITestInterfaceWithFunctions>();
            var parameterValue = new SimpleReferenceType(Guid.NewGuid());

            mock.SetupMethod(m => m.Function(parameterValue)).Callback<SimpleReferenceType>(argument =>
            {
                return argument;
            });

            // Act
            var returnValue = mock.Instance.Function(parameterValue);

            // Assert
            Assert.AreEqual(parameterValue, returnValue);
            Assert.AreEqual(parameterValue.Id, parameterValue.Id);
        }
    }
}