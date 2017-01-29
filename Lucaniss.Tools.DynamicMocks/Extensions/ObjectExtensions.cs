using System;


namespace Lucaniss.Tools.DynamicMocks.Extensions
{
    internal static class ObjectExtensions
    {
        public static Type SafeGetType(this Object instance)
        {
            return instance.GetType().SafeGetType();
        }
    }
}