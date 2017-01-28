using System;
using System.Collections.Generic;
using System.Reflection;


namespace Lucaniss.Tools.DynamicMocks
{
    public interface IMockSetup
    {
        MethodInfo MethodInfo { get; }

        IReadOnlyList<Type> ParameterTypes { get; }
        IReadOnlyList<Object> ParameterValues { get; }

        Object InvokeDelegateCallback(Object[] argumentValues);
    }
}