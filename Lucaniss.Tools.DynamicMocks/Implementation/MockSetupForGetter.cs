using System;
using System.Collections.Generic;
using System.Reflection;
using Lucaniss.Tools.DynamicMocks.Exceptions;


namespace Lucaniss.Tools.DynamicMocks.Implementation
{
    internal sealed class MockSetupForGetter<TResult> : MockSetup, IMockSetupForGetter<TResult>
    {
        private Func<TResult> FunctionDelegate { get; set; }


        public MockSetupForGetter(MethodInfo methodInfo, IReadOnlyList<Type> parameterTypes, IReadOnlyList<Object> parameterValues)
            : base(methodInfo, parameterTypes, parameterValues)
        {
        }


        public void Callback(TResult value)
        {
            FunctionDelegate = () => value;
        }

        public void Callback(Func<TResult> mockFunction)
        {
            FunctionDelegate = mockFunction;
        }

        public override Object InvokeDelegateCallback(Object[] argumentValues)
        {
            if (FunctionDelegate == null)
            {
                throw MockExceptionHelper.MethodHasNoInvocationCallback(MethodInfo.Name);
            }

            return FunctionDelegate();
        }
    }
}