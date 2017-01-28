using System;
using System.Reflection;


namespace Lucaniss.Tools.DynamicMocks.Extensions
{
    public static class ParameterInfoExtensions
    {
        public static Type SafeGetType(this ParameterInfo parameterInfo)
        {
            return parameterInfo.ParameterType.IsByRef ?
                parameterInfo.ParameterType.GetElementType() :
                parameterInfo.ParameterType;
        }

        public static Boolean IsValueOrPrimitiveType(this ParameterInfo parameterInfo)
        {
            var type = parameterInfo.SafeGetType();
            return type.IsValueType || type.IsPrimitive;
        }
    }
}