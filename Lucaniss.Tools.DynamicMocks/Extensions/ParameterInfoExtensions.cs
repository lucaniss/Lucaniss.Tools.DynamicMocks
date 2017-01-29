using System;
using System.Reflection;


namespace Lucaniss.Tools.DynamicMocks.Extensions
{
    public static class ParameterInfoExtensions
    {
        public static Type SafeGetType(this ParameterInfo parameterInfo)
        {
            return parameterInfo.ParameterType.SafeGetType();
        }

        public static String SafeGetTypeName(this ParameterInfo parameterInfo)
        {
            return parameterInfo.ParameterType.SafeGetTypeName();
        }

        public static Boolean IsValueOrPrimitiveType(this ParameterInfo parameterInfo)
        {
            var type = parameterInfo.SafeGetType();
            return type.IsValueType || type.IsPrimitive;
        }
    }
}