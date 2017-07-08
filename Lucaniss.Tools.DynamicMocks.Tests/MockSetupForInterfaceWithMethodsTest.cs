using System;
using Lucaniss.Tools.DynamicMocks.Exceptions;
using Lucaniss.Tools.DynamicMocks.Tests.Data;
using Lucaniss.Tools.DynamicMocks.Tests.Data.Interfaces;
using Lucaniss.Tools.DynamicMocks.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Lucaniss.Tools.DynamicMocks.Tests
{
    [TestClass]
    public class MockSetupForInterfaceWithMethodsTest
    {
        [TestMethod]
        public void SetupMethod_WhenExpressionIsNotMethodCallExpression_ThenThrowException()
        {
            // Arrange
            var mock = Mock.Create<ITestInterfaceWithProperties>();

            // Act
            Action action = () =>
            {
                mock.SetupMethod(m => m.PropertyForValueType);
            };

            // Assert
            AssertException.Throws<MockException>(action, e => e.Error == MockExceptionErrors.ExpressionIsNotMethodCallExpression);
        }

        [TestMethod]
        public void SetupMethod_WhenMethodSetupMatchAreMoreThanOnce_ThenThrowException()
        {
            // Arrange
            var mock = Mock.Create<ITestInterfaceWithActions>();

            var parameterValue = new SimpleValueType(Guid.NewGuid());

            mock.SetupMethod(m => m.Action(parameterValue));
            mock.SetupMethod(m => m.Action(Arg.Any<SimpleValueType>()));

            // Act
            Action action = () =>
            {
                mock.Instance.Action(parameterValue);
            };

            // Assert
            AssertException.Throws<MockException>(action, e => e.Error == MockExceptionErrors.MethodSetupConflict);
        }

        [TestMethod]
        public void SetupMethod_WhenThereIsMoreDifferentSetupThanOne_ThenInvokeCallback()
        {
            // Arrange
            var mock = Mock.Create<ITestInterfaceWithActions>();

            var invocationHandler1 = new InvocationHandler();
            var invocationHandler2 = new InvocationHandler();

            var parameterValue1 = new SimpleValueType(Guid.NewGuid());
            var parameterValue2 = new SimpleValueType(Guid.NewGuid());

            mock.SetupMethod(m => m.Action(parameterValue1)).Callback<SimpleValueType>(argument =>
            {
                invocationHandler1.IsInvoked = true;
                invocationHandler1.SimpleValueTypeCheck = argument;
            });

            mock.SetupMethod(m => m.Action(parameterValue2)).Callback<SimpleValueType>(argument =>
            {
                invocationHandler2.IsInvoked = true;
                invocationHandler2.SimpleValueTypeCheck = argument;
            });

            // Act
            mock.Instance.Action(parameterValue1);

            // Assert
            Assert.IsTrue(invocationHandler1.IsInvoked);
            Assert.IsFalse(invocationHandler2.IsInvoked);

            Assert.AreEqual(parameterValue1, invocationHandler1.SimpleValueTypeCheck);
        }


        [TestMethod]
        public void SetupMethod_WithArgumentAsWildcard_WhenInvokeMethodGroup_ThenInvokeCallback()
        {
            // Arrange
            var mock = Mock.Create<ITestInterfaceWithActions>();

            var invocationHandler1 = new InvocationHandler();
            var invocationHandler2 = new InvocationHandler();

            var parameterValue = new SimpleValueType(Guid.NewGuid());

            mock.SetupMethod(m => m.Action(Arg.Any<SimpleValueType>())).Callback<SimpleValueType>(argument =>
            {
                invocationHandler1.IsInvoked = true;
                invocationHandler1.SimpleValueTypeCheck = argument;
            });

            mock.SetupMethod(m => m.Action(Arg.Any<SimpleReferenceType>())).Callback<SimpleReferenceType>(argument =>
            {
                invocationHandler2.IsInvoked = true;
                invocationHandler2.SimpleReferenceTypeCheck = argument;
            });

            // Act           
            mock.Instance.Action(parameterValue);

            // Assert
            Assert.IsTrue(invocationHandler1.IsInvoked);
            Assert.IsFalse(invocationHandler2.IsInvoked);

            Assert.AreEqual(parameterValue, invocationHandler1.SimpleValueTypeCheck);
        }


        [TestMethod]
        public void SetupMethod_WithArgumentOfValueType_WhenInvokeMethodGroupWithConflict_ThenThrowException()
        {
            // Arrange
            var mock = Mock.Create<ITestInterfaceWithActions>();

            var invocationHandler1 = new InvocationHandler();
            var invocationHandler2 = new InvocationHandler();

            var parameter = new SimpleValueType(Guid.NewGuid());

            mock.SetupMethod(m => m.ActionWithSignatureConflict1(parameter)).Callback<SimpleValueType>(argument =>
            {
                invocationHandler1.IsInvoked = true;
            });

            mock.SetupMethod(m => m.ActionWithSignatureConflict1(ref parameter)).Callback<SimpleValueType>(argument =>
            {
                invocationHandler2.IsInvoked = true;
            });

            // Act
            Action action = () =>
            {
                mock.Instance.ActionWithSignatureConflict1(parameter);
            };

            // Assert
            AssertException.Throws<MockException>(action, e => e.Error == MockExceptionErrors.MethodSetupConflict);

            // TODO: Jest to znane zachowanie (Wywo³uj¹c interceptor tracimy informacjê o tym czy argument zosta³ przekazany przez referencjê czy nie).
            // TODO: Wymaga zmiany w wewnêtrznej implementacji mocka (MSIL).
        }

        [TestMethod]
        public void SetupMethod_WithArgumentOfReferenceType_WhenInvokeMethodGroupWithConflict_ThenThrowException()
        {
            // Arrange
            var mock = Mock.Create<ITestInterfaceWithActions>();

            var invocationHandler1 = new InvocationHandler();
            var invocationHandler2 = new InvocationHandler();

            var parameter = new SimpleReferenceType(Guid.NewGuid());

            mock.SetupMethod(m => m.ActionWithSignatureConflict2(parameter)).Callback<SimpleReferenceType>(argument =>
            {
                invocationHandler1.IsInvoked = true;
            });

            mock.SetupMethod(m => m.ActionWithSignatureConflict2(ref parameter)).Callback<SimpleReferenceType>(argument =>
            {
                invocationHandler2.IsInvoked = true;
            });

            // Act
            Action action = () =>
            {
                mock.Instance.ActionWithSignatureConflict2(parameter);
            };

            // Assert
            AssertException.Throws<MockException>(action, e => e.Error == MockExceptionErrors.MethodSetupConflict);

            // TODO: Jest to znane zachowanie (Wywo³uj¹c interceptor tracimy informacjê o tym czy argument zosta³ przekazany przez referencjê czy nie).
            // TODO: Wymaga zmiany w wewnêtrznej (MSIL) implementacji mocka.
        }


        [TestMethod]
        public void SetupMethod_WithArgumentOfReferenceType_WhenArgumentIsNull_ThenInvokeCallback()
        {
            // Arrange
            var mock = Mock.Create<ITestInterfaceWithActions>();

            var invocationHandler = new InvocationHandler();

            mock.SetupMethod(m => m.Action(Arg.Any<SimpleReferenceType>())).Callback<SimpleReferenceType>(argument =>
            {
                invocationHandler.IsInvoked = true;
                invocationHandler.SimpleReferenceTypeCheck = argument;
            });

            // Act
            mock.Instance.Action(null);

            // Assert
            Assert.IsTrue(invocationHandler.IsInvoked);
            Assert.AreEqual(null, invocationHandler.SimpleReferenceTypeCheck);
        }
    }
}