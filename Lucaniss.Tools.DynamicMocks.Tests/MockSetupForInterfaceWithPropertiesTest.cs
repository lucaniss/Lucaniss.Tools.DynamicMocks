using System;
using Lucaniss.Tools.DynamicMocks.Exceptions;
using Lucaniss.Tools.DynamicMocks.Tests.Data;
using Lucaniss.Tools.DynamicMocks.Tests.Data.Interfaces;
using Lucaniss.Tools.DynamicMocks.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Lucaniss.Tools.DynamicMocks.Tests
{
    [TestClass]
    public class MockSetupForInterfaceWithPropertiesTest
    {
        [TestMethod]
        public void SetupGetter_WhenExpressionIsNotMemberExpression_ThenThrowException()
        {
            var mock = Mock.Create<ITestInterfaceWithProperties>();

            // Act
            Action action = () =>
            {
                mock.SetupGetter(e => e.ToString());
            };

            // Assert
            AssertException.Throws<MockException>(action, e => e.Error == MockExceptionErrors.ExpressionIsNotMemberExpression);
        }

        [TestMethod]
        public void SetupSetter_WhenExpressionIsNotMemberExpression_ThenThrowException()
        {
            var mock = Mock.Create<ITestInterfaceWithProperties>();

            // Act
            Action action = () =>
            {
                mock.SetupSetter(e => e.ToString(), null);
            };

            // Assert
            AssertException.Throws<MockException>(action, e => e.Error == MockExceptionErrors.ExpressionIsNotMemberExpression);
        }


        [TestMethod]
        public void SetupGetter_ForValueType_WhenInvokeMethod_ThenInvokeCallback()
        {
            // Arrange
            var mock = Mock.Create<ITestInterfaceWithProperties>();
            var value = new SimpleValueType(Guid.NewGuid());

            mock.SetupGetter(e => e.PropertyForValueType).Callback(() =>
            {
                return value;
            });

            // Act
            var returnValue = mock.Instance.PropertyForValueType;

            // Assert
            Assert.AreEqual(value, returnValue);
        }

        [TestMethod]
        public void SetupGetter_ForReferenceType_WhenInvokeMethod_ThenInvokeCallback()
        {
            // Arrange
            var mock = Mock.Create<ITestInterfaceWithProperties>();
            var value = new SimpleReferenceType(Guid.NewGuid());

            mock.SetupGetter(e => e.PropertyForReferenceType).Callback(() =>
            {
                return value;
            });

            // Act
            var returnValue = mock.Instance.PropertyForReferenceType;

            // Assert
            Assert.AreEqual(value, returnValue);
        }


        [TestMethod]
        public void SetupSetter_ForValueType_WhenSetupMatch_ThenInvokeCallback()
        {
            // Arrange
            var mock = Mock.Create<ITestInterfaceWithProperties>();

            var parameterValue = new SimpleValueType(Guid.NewGuid());
            var invocationHandler = new InvocationHandler();

            mock.SetupSetter(e => e.PropertyForValueType, parameterValue).Callback(argument =>
            {
                invocationHandler.IsInvoked = true;
                invocationHandler.SimpleValueTypeCheck = argument;
            });

            // Act
            mock.Instance.PropertyForValueType = parameterValue;

            // Assert
            Assert.IsTrue(invocationHandler.IsInvoked);
            Assert.AreEqual(parameterValue, invocationHandler.SimpleValueTypeCheck);
            Assert.AreEqual(parameterValue.Id, invocationHandler.SimpleValueTypeCheck.Id);
        }

        [TestMethod]
        public void SetupSetter_ForReferenceType_WhenSetupMatch_ThenInvokeCallback()
        {
            // Arrange
            var mock = Mock.Create<ITestInterfaceWithProperties>();

            var parameterValue = new SimpleReferenceType(Guid.NewGuid());
            var invocationHandler = new InvocationHandler();

            mock.SetupSetter(e => e.PropertyForReferenceType, parameterValue).Callback(argument =>
            {
                invocationHandler.IsInvoked = true;
                invocationHandler.SimpleReferenceTypeCheck = argument;
            });

            // Act
            mock.Instance.PropertyForReferenceType = parameterValue;

            // Assert
            Assert.IsTrue(invocationHandler.IsInvoked);
            Assert.AreEqual(parameterValue, invocationHandler.SimpleReferenceTypeCheck);
            Assert.AreEqual(parameterValue.Id, invocationHandler.SimpleReferenceTypeCheck.Id);
        }


        [TestMethod]
        public void SetupSetter_ForValueType_WhenSetupNoMatch_ThenThrowException()
        {
            // Arrange
            var mock = Mock.Create<ITestInterfaceWithProperties>();

            var parameterValue1 = new SimpleValueType(Guid.NewGuid());
            var parameterValue2 = new SimpleValueType(Guid.NewGuid());

            mock.SetupSetter(e => e.PropertyForValueType, parameterValue1).Callback(argument =>
            {
            });

            // Act
            Action action = () =>
            {
                mock.Instance.PropertyForValueType = parameterValue2;
            };

            // Assert
            AssertException.Throws<MockException>(action, e => e.Error == MockExceptionErrors.MethodHasNoSetup);
        }

        [TestMethod]
        public void SetupSetter_ForReferenceType_WhenSetupNoMatch_ThenThrowException()
        {
            // Arrange
            var mock = Mock.Create<ITestInterfaceWithProperties>();

            var parameterValue1 = new SimpleReferenceType(Guid.NewGuid());
            var parameterValue2 = new SimpleReferenceType(Guid.NewGuid());

            mock.SetupSetter(e => e.PropertyForReferenceType, parameterValue1).Callback(argument =>
            {
            });

            // Act
            Action action = () =>
            {
                mock.Instance.PropertyForReferenceType = parameterValue2;
            };

            // Assert
            AssertException.Throws<MockException>(action, e => e.Error == MockExceptionErrors.MethodHasNoSetup);
        }
    }
}