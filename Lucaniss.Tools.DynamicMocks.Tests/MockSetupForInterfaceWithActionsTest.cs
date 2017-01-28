using System;
using Lucaniss.Tools.DynamicMocks.Tests.Data;
using Lucaniss.Tools.DynamicMocks.Tests.Data.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;


// ReSharper disable AccessToModifiedClosure
// ReSharper disable RedundantAssignment

namespace Lucaniss.Tools.DynamicMocks.Tests
{
    [TestClass]
    public class MockSetupForInterfaceWithActionsTest
    {
        [TestMethod]
        public void SetupAction_WithNoArguments_WhenInvokeMethod_InvokeCallback()
        {
            // Arrange
            var mock = Mock.Create<ITestInterfaceWithActions>();
            var invocationHandler = new InvocationHandler();

            mock.SetupMethod(m => m.Action()).Callback(() =>
            {
                invocationHandler.IsInvoked = true;
            });

            // Act
            mock.Instance.Action();

            // Assert
            Assert.IsTrue(invocationHandler.IsInvoked);
        }


        [TestMethod]
        public void SetupAction_WithArgumentOfValueType_WhenSetupMatch_InvokeCallbackAndVariableIsNotModified()
        {
            // Arrange            
            var mock = Mock.Create<ITestInterfaceWithActions>();

            var invocationHandler = new InvocationHandler();
            var valueBeforeCall = new SimpleValueType(Guid.NewGuid());
            var valueAfterCall = new SimpleValueType(Guid.NewGuid());

            mock.SetupMethod(m => m.Action(valueBeforeCall)).Callback<SimpleValueType>(argument =>
            {
                invocationHandler.IsInvoked = true;
                argument = valueAfterCall;
            });

            // Act
            mock.Instance.Action(valueBeforeCall);

            // Assert        
            Assert.IsTrue(invocationHandler.IsInvoked);

            Assert.AreNotEqual(valueBeforeCall, valueAfterCall);
            Assert.AreNotEqual(valueBeforeCall.Id, valueAfterCall.Id);
        }

        [TestMethod]
        public void SetupAction_WithArgumentOfValueTypeByOut_WhenSetupMatch_InvokeCallbackAndVariableIsModified()
        {
            // Arrange            
            var mock = Mock.Create<ITestInterfaceWithActions>();

            var invocationHandler = new InvocationHandler();
            var valueBeforeCall = new SimpleValueType(Guid.NewGuid());
            var valueAfterCall = new SimpleValueType(Guid.NewGuid());

            mock.SetupMethod(m => m.ActionWithOutParam(out valueBeforeCall)).CallbackDynamic(argument =>
            {
                invocationHandler.IsInvoked = true;
                argument[0] = valueAfterCall;
            });

            // Act
            mock.Instance.ActionWithOutParam(out valueBeforeCall);

            // Assert
            Assert.IsTrue(invocationHandler.IsInvoked);

            Assert.AreEqual(valueBeforeCall, valueAfterCall);
            Assert.AreEqual(valueBeforeCall.Id, valueAfterCall.Id);
        }

        [TestMethod]
        public void SetupAction_WithArgumentOfValueTypeByRef_WhenSetupMatch_InvokeCallbackAndVariableIsModified()
        {
            // Arrange            
            var mock = Mock.Create<ITestInterfaceWithActions>();

            var invocationHandler = new InvocationHandler();
            var valueBeforeCall = new SimpleValueType(Guid.NewGuid());
            var valueAfterCall = new SimpleValueType(Guid.NewGuid());

            mock.SetupMethod(m => m.ActionWithRefParam(ref valueBeforeCall)).CallbackDynamic(argument =>
            {
                invocationHandler.IsInvoked = true;
                argument[0] = valueAfterCall;
            });

            // Act
            mock.Instance.ActionWithRefParam(ref valueBeforeCall);

            // Assert
            Assert.IsTrue(invocationHandler.IsInvoked);

            Assert.AreEqual(valueBeforeCall, valueAfterCall);
            Assert.AreEqual(valueBeforeCall.Id, valueAfterCall.Id);
        }

        [TestMethod]
        public void SetupAction_WithArgumentOfValueTypeByGeneric_WhenSetupMatch_InvokeCallbackAndVariableIsNotModified()
        {
            // Arrange            
            var mock = Mock.Create<ITestInterfaceWithActions>();

            var invocationHandler = new InvocationHandler();
            var valueBeforeCall = new SimpleValueType(Guid.NewGuid());
            var valueAfterCall = new SimpleValueType(Guid.NewGuid());

            mock.SetupMethod(m => m.ActionWithGenericValueParameter(valueBeforeCall)).Callback<SimpleValueType>(argument =>
            {
                invocationHandler.IsInvoked = true;
                argument = valueAfterCall;
            });

            // Act
            mock.Instance.ActionWithGenericValueParameter(valueBeforeCall);

            // Assert
            Assert.IsTrue(invocationHandler.IsInvoked);

            Assert.AreNotEqual(valueBeforeCall, valueAfterCall);
            Assert.AreNotEqual(valueBeforeCall.Id, valueAfterCall.Id);
        }


        [TestMethod]
        public void SetupAction_WithArgumentOfReferenceType_WhenSetupMatch_InvokeCallbackAndVariableIsNotModified()
        {
            // Arrange            
            var mock = Mock.Create<ITestInterfaceWithActions>();

            var invocationHandler = new InvocationHandler();
            var valueBeforeCall = new SimpleReferenceType(Guid.NewGuid());
            var valueAfterCall = new SimpleReferenceType(Guid.NewGuid());

            mock.SetupMethod(m => m.Action(valueBeforeCall)).Callback<SimpleReferenceType>(argument =>
            {
                invocationHandler.IsInvoked = true;
                argument = valueAfterCall;
            });

            // Act
            mock.Instance.Action(valueBeforeCall);

            // Assert
            Assert.AreNotEqual(valueBeforeCall, valueAfterCall);
            Assert.AreNotEqual(valueBeforeCall.Id, valueAfterCall.Id);
        }

        [TestMethod]
        public void SetupAction_WithArgumentOfReferenceTypeByOut_WhenSetupMatch_InvokeCallbackAndVariableIsModified()
        {
            // Arrange            
            var mock = Mock.Create<ITestInterfaceWithActions>();

            var invocationHandler = new InvocationHandler();
            var valueBeforeCall = new SimpleReferenceType(Guid.NewGuid());
            var valueAfterCall = new SimpleReferenceType(Guid.NewGuid());

            mock.SetupMethod(m => m.ActionWithOutParam(out valueBeforeCall)).CallbackDynamic(argument =>
            {
                invocationHandler.IsInvoked = true;
                argument[0] = valueAfterCall;
            });

            // Act
            mock.Instance.ActionWithOutParam(out valueBeforeCall);

            // Assert
            Assert.IsTrue(invocationHandler.IsInvoked);

            Assert.AreEqual(valueBeforeCall, valueAfterCall);
            Assert.AreEqual(valueBeforeCall.Id, valueAfterCall.Id);
        }

        [TestMethod]
        public void SetupAction_WithArgumentOfReferenceTypeByRef_WhenSetupMatch_InvokeCallbackAndVariableIsModified()
        {
            // Arrange            
            var mock = Mock.Create<ITestInterfaceWithActions>();

            var invocationHandler = new InvocationHandler();
            var valueBeforeCall = new SimpleReferenceType(Guid.NewGuid());
            var valueAfterCall = new SimpleReferenceType(Guid.NewGuid());

            mock.SetupMethod(m => m.ActionWithRefParam(ref valueBeforeCall)).CallbackDynamic(argument =>
            {
                invocationHandler.IsInvoked = true;
                argument[0] = valueAfterCall;
            });

            // Act
            mock.Instance.ActionWithRefParam(ref valueBeforeCall);

            // Assert
            Assert.IsTrue(invocationHandler.IsInvoked);

            Assert.AreEqual(valueBeforeCall, valueAfterCall);
            Assert.AreEqual(valueBeforeCall.Id, valueAfterCall.Id);
        }

        [TestMethod]
        public void SetupAction_WithArgumentOfReferenceTypeByGeneric_WhenSetupMatch_InvokeCallbackAndVariableIsNotModified()
        {
            // Arrange            
            var mock = Mock.Create<ITestInterfaceWithActions>();

            var invocationHandler = new InvocationHandler();
            var valueBeforeCall = new SimpleReferenceType(Guid.NewGuid());
            var valueAfterCall = new SimpleReferenceType(Guid.NewGuid());

            mock.SetupMethod(m => m.ActionWithGenericReferenceParameter(valueBeforeCall)).Callback<SimpleReferenceType>(argument =>
            {
                invocationHandler.IsInvoked = true;
                argument = valueAfterCall;
            });

            // Act
            mock.Instance.ActionWithGenericReferenceParameter(valueBeforeCall);

            // Assert
            Assert.IsTrue(invocationHandler.IsInvoked);

            Assert.AreNotEqual(valueBeforeCall, valueAfterCall);
            Assert.AreNotEqual(valueBeforeCall.Id, valueAfterCall.Id);
        }
    }
}