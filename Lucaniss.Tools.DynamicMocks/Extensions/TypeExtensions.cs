using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace Lucaniss.Tools.DynamicMocks.Extensions
{
    internal static class TypeExtensions
    {
        public static IEnumerable<MethodInfo> GetMethodInfosForMock(this Type type)
        {
            var methodInfos = new List<MethodInfo>();

            var methodInfosFromCurrentInterfaceType = type.GetMethods();
            var methodInfosFromBaseInterfacesTypes = type.GetInterfaces().SelectMany(e => e.GetMethods());

            methodInfos.AddRange(methodInfosFromCurrentInterfaceType);
            methodInfos.AddRange(methodInfosFromBaseInterfacesTypes);

            return methodInfos;
        }

        public static Type SafeGetType(this Type type)
        {
            return type.IsByRef ? type.GetElementType() : type;
        }

        public static String SafeGetTypeName(this Type type)
        {
            return SafeGetType(type).IsGenericParameter ? type.Name : type.FullName;
        }

        public static Boolean IsValueOrPrimitiveType(this Type parameterInfo)
        {
            var type = parameterInfo.SafeGetType();
            return type.IsValueType || type.IsPrimitive;
        }
    }
}