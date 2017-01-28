using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using Lucaniss.Tools.DynamicMocks.Consts;
using Lucaniss.Tools.DynamicMocks.Extensions;


namespace Lucaniss.Tools.DynamicMocks.Implementation
{
    internal sealed class MockBuilder<TInterface> : IMockBuilder
    {
        private TypeBuilder _typeBuilder;


        public IMockObject Build()
        {
            CreateClass();
            CreateMethods();

            return (IMockObject) Activator.CreateInstance(_typeBuilder.CreateType());
        }


        private void CreateClass()
        {
            var typeName = String.Format(MockConsts.TypeName, typeof (TInterface).FullName);

            var assemblyBuilder = Thread.GetDomain().DefineDynamicAssembly(new AssemblyName(MockConsts.AssemblyName), AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule(MockConsts.ModuleName);

            _typeBuilder = moduleBuilder.DefineType(typeName, TypeAttributes.Class | TypeAttributes.Public, typeof (MockObject), new[] { typeof (TInterface) });
        }

        private void CreateMethods()
        {
            foreach (var methodInfo in typeof (TInterface).GetMethodInfosForMock())
            {
                CreateMethod(methodInfo);
            }
        }

        private void CreateMethod(MethodInfo methodInfo)
        {
            var methodBuilder = _typeBuilder.DefineMethod(
                methodInfo.Name,
                MethodAttributes.Public | MethodAttributes.Virtual,
                methodInfo.CallingConvention,
                methodInfo.ReturnType,
                methodInfo.GetParameters().Select(p => p.ParameterType).ToArray());

            CreateGenericParameters(methodInfo, methodBuilder);
            CreateMethodBody(methodInfo, methodBuilder);
        }

        private static void CreateGenericParameters(MethodInfo methodInfo, MethodBuilder methodBuilder)
        {
            var genericParameters = methodInfo.GetGenericArguments().Select(p => p.Name).ToArray();
            if (genericParameters.Any())
            {
                // INFO: Definicja parametrów generycznych metody (na podstawie sygnatury metody interfejsu).
                methodBuilder.DefineGenericParameters(genericParameters);
            }
        }

        private static void CreateMethodBody(MethodInfo methodInfo, MethodBuilder methodBuilder)
        {
            // INFO: Pobranie generatora kodu IL dla tworzonej metody.
            var msil = methodBuilder.GetILGenerator();

            // INFO: Zadeklarowanie zmiennych lokalnych tablicowych, które będą przechowywały nazwy typów oraz wartości argumentów metody.
            var arrayForArgumentTypesVariable = MSILCodeGenerator.DeclareLocalVariable(msil, typeof (String[]));
            var arrayForArgumentValuesVariable = MSILCodeGenerator.DeclareLocalVariable(msil, typeof (Object[]));

            // INFO: Zainicjalizowanie zmiennych lokalnych tablicowych.
            MSILCodeGenerator.CreateArrayForArgumentTypes(msil, methodInfo, arrayForArgumentTypesVariable);
            MSILCodeGenerator.CraeteArrayForArgumentValues(msil, methodInfo, arrayForArgumentValuesVariable);

            // INFO: Pobranie parametrów metody.
            var parameters = methodInfo.GetParameters();

            // INFO: Skopiowanie nazw typów oraz wartości argumentów metody do zmiennych lokalnych tablicowych.
            for (var index = 0; index < parameters.Length; index++)
            {
                var parameter = parameters[index];

                MSILCodeGenerator.StoreArgumentTypeNameInArray(msil, parameter, index, arrayForArgumentTypesVariable);
                MSILCodeGenerator.StoreArgumentValueInArray(msil, parameter, index, arrayForArgumentValuesVariable);
            }

            // INFO: Wywołanie metody interceptora (do której przekazujemy stworzone wcześniej zmienne lokalne tablicowe).
            MSILCodeGenerator.InvokeMockInterceptorMethod(msil, methodInfo, arrayForArgumentTypesVariable, arrayForArgumentValuesVariable);

            // INFO: Obsłużenie zwracanej przez interceptor wartości.
            MSILCodeGenerator.HandleMockInterceptorMethodReturnValue(msil, methodInfo);

            // INFO: Przypisanie wartości argumentów przekazywanych przez referencję (ref, out).
            MSILCodeGenerator.AssignReferenceArgumentValues(msil, parameters, arrayForArgumentValuesVariable);

            // INFO: Zakończenie metody.
            MSILCodeGenerator.ReturnFromMethod(msil, methodInfo);
        }
    }
}