using System;
using System.Collections.Generic;


namespace Lucaniss.Tools.DynamicMocks
{
    public interface IMockObject
    {
        ICollection<IMockSetup> Setups { get; }
        Object InvokeInterceptorMethod(String methodName, String[] argumentTypes, Object[] argumentValues);
    }
}