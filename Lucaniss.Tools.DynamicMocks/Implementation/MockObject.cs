using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Lucaniss.Tools.DynamicMocks.Exceptions;
using Lucaniss.Tools.DynamicMocks.Extensions;


// ReSharper disable PossibleIntendedRethrow

namespace Lucaniss.Tools.DynamicMocks.Implementation
{
    public class MockObject : IMockObject
    {
        public ICollection<IMockSetup> Setups { get; }


        public MockObject()
        {
            Setups = new Collection<IMockSetup>();
        }


        public static MethodInfo GetInterceptorMethodInfo()
        {
            MockObject instance;
            return typeof (MockObject).GetMethod(nameof(instance.InvokeInterceptorMethod));
        }

        public Object InvokeInterceptorMethod(String methodName, String[] argumentTypes, Object[] argumentValues)
        {
            var setup = FindSetups(methodName, argumentTypes, argumentValues).ToList();

            if (!setup.Any())
            {
                throw MockExceptionHelper.MethodHasNoSetup(methodName);
            }

            if (setup.Count != 1)
            {
                throw MockExceptionHelper.MethodSetupConflict(methodName);
            }

            try
            {
                return setup.Single().InvokeDelegateCallback(argumentValues);
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException ?? e;
            }
        }


        private IEnumerable<IMockSetup> FindSetups(String methodName, IReadOnlyList<String> argumentTypes, IReadOnlyList<Object> argumentValues)
        {
            return Setups.Where(s => IsMethodNameMatch(s, methodName)
                && IsParametersAndArgumentsTypesMatch(s.ParameterTypes, argumentTypes, argumentValues)
                && IsParametersAndArgumentsValuesMatch(s.ParameterValues, argumentValues));
        }

        private static Boolean IsMethodNameMatch(IMockSetup setup, String methodName)
        {
            return setup.MethodInfo.Name.Equals(methodName);
        }

        private static Boolean IsParametersAndArgumentsTypesMatch(IReadOnlyList<Type> parameterTypes, IReadOnlyList<String> argumentTypes, IReadOnlyList<Object> argumentValues)
        {
            if (parameterTypes.Count != argumentTypes.Count)
            {
                return false;
            }

            for (var i = 0; i < parameterTypes.Count; i++)
            {
                if (!parameterTypes[i].SafeGetType().IsAssignableFrom(argumentValues[i]?.SafeGetType()))
                {
                    if (parameterTypes[i].SafeGetTypeName() != argumentTypes[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static Boolean IsParametersAndArgumentsValuesMatch(IReadOnlyList<Object> parameterValues, IReadOnlyList<Object> argumentValues)
        {
            if (parameterValues.Count != argumentValues.Count)
            {
                return false;
            }

            for (var i = 0; i < parameterValues.Count; i++)
            {
                if (argumentValues[i] == null)
                {
                    continue;
                }

                var argumentValueMock = parameterValues[i] as ArgValueMock;
                if (argumentValueMock != null && argumentValueMock.Type.IsAssignableFrom(argumentValues[i].SafeGetType()))
                {
                    continue;
                }

                if (!parameterValues[i].Equals(argumentValues[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}