using System;
using System.Collections.Generic;
using System.Reflection;
using Lucaniss.Tools.DynamicMocks.Exceptions;


namespace Lucaniss.Tools.DynamicMocks.Implementation
{
    internal sealed class MockSetupForFunction<TResult> : MockSetup, IMockSetupForFunction<TResult>
    {
        private Func<Object[], Object> FunctionDelegate { get; set; }


        public MockSetupForFunction(MethodInfo methodInfo, IReadOnlyList<Type> parameterTypes, IReadOnlyList<Object> parameterValues)
            : base(methodInfo, parameterTypes, parameterValues)
        {
        }


        public void Callback(TResult value)
        {
            FunctionDelegate = (argumentValues) => value;
        }

        public void Callback(Func<TResult> mockFunction)
        {
            FunctionDelegate = mockFunction.DynamicInvoke;
        }

        public void Callback<T1>(Func<T1, TResult> mockFunction)
        {
            FunctionDelegate = mockFunction.DynamicInvoke;
        }

        public void Callback<T1, T2>(Func<T1, T2, TResult> mockFunction)
        {
            FunctionDelegate = mockFunction.DynamicInvoke;
        }

        public void Callback<T1, T2, T3>(Func<T1, T2, T3, TResult> mockFunction)
        {
            FunctionDelegate = mockFunction.DynamicInvoke;
        }

        public void Callback<T1, T2, T3, T4>(Func<T1, T2, T3, T4, TResult> mockFunction)
        {
            FunctionDelegate = mockFunction.DynamicInvoke;
        }

        public void Callback<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, TResult> mockFunction)
        {
            FunctionDelegate = mockFunction.DynamicInvoke;
        }

        public void Callback<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, TResult> mockFunction)
        {
            FunctionDelegate = mockFunction.DynamicInvoke;
        }

        public void Callback<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> mockFunction)
        {
            FunctionDelegate = mockFunction.DynamicInvoke;
        }

        public void Callback<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> mockFunction)
        {
            FunctionDelegate = mockFunction.DynamicInvoke;
        }

        public void Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> mockFunction)
        {
            FunctionDelegate = mockFunction.DynamicInvoke;
        }

        public void Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> mockFunction)
        {
            FunctionDelegate = mockFunction.DynamicInvoke;
        }

        public void CallbackDynamic(Func<Object[], Object> mockFunction)
        {
            FunctionDelegate = mockFunction;
        }

        public override Object InvokeDelegateCallback(Object[] argumentValues)
        {
            if (FunctionDelegate == null)
            {
                throw MockExceptionHelper.MethodHasNoInvocationCallback(MethodInfo.Name);
            }

            return FunctionDelegate(argumentValues);
        }
    }
}