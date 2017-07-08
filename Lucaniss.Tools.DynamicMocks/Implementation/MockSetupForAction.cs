using System;
using System.Collections.Generic;
using System.Reflection;
using Lucaniss.Tools.DynamicMocks.Exceptions;


namespace Lucaniss.Tools.DynamicMocks.Implementation
{
    internal sealed class MockSetupForAction : MockSetup, IMockSetupForAction
    {
        private Action<Object[]> ActionDelegate { get; set; }


        public MockSetupForAction(MethodInfo methodInfo, IReadOnlyList<Type> parameterTypes, IReadOnlyList<Object> parameterValues)
            : base(methodInfo, parameterTypes, parameterValues)
        {
        }


        public void Callback(Action mockAction)
        {
            ActionDelegate = argumentValues => mockAction.DynamicInvoke(argumentValues);
        }

        public void Callback<T1>(Action<T1> mockAction)
        {
            ActionDelegate = argumentValues => mockAction.DynamicInvoke(argumentValues);
        }

        public void Callback<T1, T2>(Action<T1, T2> mockAction)
        {
            ActionDelegate = argumentValues => mockAction.DynamicInvoke(argumentValues);
        }

        public void Callback<T1, T2, T3>(Action<T1, T2, T3> mockAction)
        {
            ActionDelegate = argumentValues => mockAction.DynamicInvoke(argumentValues);
        }

        public void Callback<T1, T2, T3, T4>(Action<T1, T2, T3, T4> mockAction)
        {
            ActionDelegate = argumentValues => mockAction.DynamicInvoke(argumentValues);
        }

        public void Callback<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> mockAction)
        {
            ActionDelegate = argumentValues => mockAction.DynamicInvoke(argumentValues);
        }

        public void Callback<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> mockAction)
        {
            ActionDelegate = argumentValues => mockAction.DynamicInvoke(argumentValues);
        }

        public void Callback<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> mockAction)
        {
            ActionDelegate = argumentValues => mockAction.DynamicInvoke(argumentValues);
        }

        public void Callback<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> mockAction)
        {
            ActionDelegate = argumentValues => mockAction.DynamicInvoke(argumentValues);
        }

        public void Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> mockAction)
        {
            ActionDelegate = argumentValues => mockAction.DynamicInvoke(argumentValues);
        }

        public void Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> mockAction)
        {
            ActionDelegate = argumentValues => mockAction.DynamicInvoke(argumentValues);
        }

        public void CallbackDynamic(Action<Object[]> mockAction)
        {
            ActionDelegate = mockAction;
        }

        public override Object InvokeDelegateCallback(Object[] argumentValues)
        {
            if (ActionDelegate == null)
            {
                throw MockExceptionHelper.MethodHasNoInvocationCallback(MethodInfo.Name);
            }

            ActionDelegate(argumentValues);
            return null;
        }
    }
}