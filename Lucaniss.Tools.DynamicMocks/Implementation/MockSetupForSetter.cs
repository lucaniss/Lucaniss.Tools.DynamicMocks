using System;
using System.Collections.Generic;
using System.Reflection;
using Lucaniss.Tools.DynamicMocks.Exceptions;


namespace Lucaniss.Tools.DynamicMocks.Implementation
{
    internal sealed class MockSetupForSetter<TValue> : MockSetup, IMockSetupForSetter<TValue>
    {
        private Action<TValue> ActionDelegate { get; set; }


        public MockSetupForSetter(MethodInfo methodInfo, IReadOnlyList<Type> parameterTypes, IReadOnlyList<Object> parameterValues)
            : base(methodInfo, parameterTypes, parameterValues)
        {
        }


        public void Callback(Action<TValue> mockFunction)
        {
            ActionDelegate = mockFunction;
        }

        public override Object InvokeDelegateCallback(Object[] argumentValues)
        {
            if (ActionDelegate == null)
            {
                throw MockExceptionHelper.MethodHasNoInvocationCallback(MethodInfo.Name);
            }

            ActionDelegate.DynamicInvoke(argumentValues);
            return null;
        }
    }
}