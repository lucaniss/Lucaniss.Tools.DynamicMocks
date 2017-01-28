using System;
using System.Collections.Generic;
using System.Reflection;


namespace Lucaniss.Tools.DynamicMocks.Implementation
{
    internal abstract class MockSetup : IMockSetup
    {
        public MethodInfo MethodInfo { get; }

        public IReadOnlyList<Type> ParameterTypes { get; }
        public IReadOnlyList<Object> ParameterValues { get; }


        protected MockSetup(MethodInfo methodInfo, IReadOnlyList<Type> parameterTypes, IReadOnlyList<Object> parameterValues)
        {
            MethodInfo = methodInfo;

            ParameterTypes = parameterTypes;
            ParameterValues = parameterValues;
        }


        public abstract Object InvokeDelegateCallback(Object[] argumentValues);
    }
}