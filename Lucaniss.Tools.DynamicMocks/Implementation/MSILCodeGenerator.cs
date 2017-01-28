using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Lucaniss.Tools.DynamicMocks.Extensions;


namespace Lucaniss.Tools.DynamicMocks.Implementation
{
    internal static class MSILCodeGenerator
    {
        public static LocalBuilder DeclareLocalVariable(ILGenerator msil, Type type)
        {
            // INFO: Zadeklarowanie zmiennej lokalnej o podanym typie.
            return msil.DeclareLocal(type);
        }

        public static void CreateArrayForArgumentTypes(ILGenerator msil, MethodInfo methodInfo, LocalBuilder arrayVariable)
        {
            // INFO: Stworzenie tablicy przechowuj¹cej nazwy typów argumentów metody oraz przypisanie jej do zmiennej lokalnej.
            //       1. Od³o¿enie na stosie liczby okreœlaj¹cej rozmiar tablicy.
            //       2. Utworzenie na stosie tablicy o rozmiarze wskazanym przez wartoœæ na stosie.
            msil.Emit(OpCodes.Ldc_I4, methodInfo.GetParameters().Count());
            msil.Emit(OpCodes.Newarr, typeof (String));

            // INFO: Zdjêcie ze stosu nowo utworzonej tablicy i przypisanie jej do zmiennej lokalnej.
            msil.Emit(OpCodes.Stloc, arrayVariable);
        }

        public static void CraeteArrayForArgumentValues(ILGenerator msil, MethodInfo methodInfo, LocalBuilder arrayVariable)
        {
            // INFO: Stworzenie tablicy przechowuj¹cej wartoœci argumentów metody oraz przypisanie jej do zmiennej lokalnej.
            //       1. Od³o¿enie na stosie liczby okreœlaj¹cej rozmiar tablicy.
            //       2. Utworzenie na stosie tablicy o rozmiarze wskazanym przez wartoœæ na stosie.
            msil.Emit(OpCodes.Ldc_I4, methodInfo.GetParameters().Count());
            msil.Emit(OpCodes.Newarr, typeof (Object));

            // INFO: Zdjêcie ze stosu nowo utworzonej tablicy i przypisanie jej do zmiennej lokalnej.
            msil.Emit(OpCodes.Stloc, arrayVariable);
        }

        public static void StoreArgumentTypeNameInArray(ILGenerator msil, ParameterInfo parameter, Int32 index, LocalBuilder arrayVariable)
        {
            // INFO: Od³o¿enie na stosie elementów potrzebnych do skopiowania wartoœci parametru metody do tablicy obiektów.
            msil.Emit(OpCodes.Ldloc, arrayVariable); // Od³o¿enie na stosie zmiennej lokalnej reprezentuj¹cej tablicê obiektów.
            msil.Emit(OpCodes.Ldc_I4, index); // Od³o¿enie na stosie liczby reprezentuj¹cej indeks elementu tablicy do którego ma zostaæ skopiowana wartoœæ.
            msil.Emit(OpCodes.Ldstr, parameter.ParameterType.SafeGetTypeName()); // Od³o¿enie na stosie nazwy typu argumentu metody.

            // INFO: Skopiowanie nazwy typu argumentu metody do tablicy obiektów.
            msil.Emit(OpCodes.Stelem_Ref);
        }

        public static void StoreArgumentValueInArray(ILGenerator msil, ParameterInfo parameter, Int32 index, LocalBuilder arrayVariable)
        {
            // INFO: Od³o¿enie na stosie elementów potrzebnych do skopiowania wartoœci parametru metody do tablicy obiektów.
            msil.Emit(OpCodes.Ldloc, arrayVariable); // Od³o¿enie na stosie zmiennej lokalnej reprezentuj¹cej tablicê obiektów.
            msil.Emit(OpCodes.Ldc_I4, index); // Od³o¿enie na stosie liczby reprezentuj¹cej indeks elementu tablicy do którego ma zostaæ skopiowana wartoœæ.          

            // INFO: Od³o¿enie na stosie wartoœci argumentu metody o podanym indeksie.
            //       W przypadku parametrów referencyjnych (ref, out) odk³adany jest adres.
            msil.Emit(OpCodes.Ldarg, index + 1);

            // INFO: Jeœli parametr jest przekazywany przez referencjê (ref, out).
            if (parameter.ParameterType.IsByRef)
            {
                if (parameter.IsValueOrPrimitiveType())
                {
                    // INFO: Od³o¿enie na stosie obiektu wskazywanego przez adres argumentu metody o podanym indeksie (dla typów wartoœciowych).
                    msil.Emit(OpCodes.Ldobj, parameter.SafeGetType());
                }
                else
                {
                    // INFO: Od³o¿enie na stosie referencji argumentu metody o podanym indeksie (dla typów referencyjnych).
                    msil.Emit(OpCodes.Ldind_Ref);
                }
            }

            // INFO: Jeœli parametr jest typem prostym wtedy wymagana jest operacja 'Box'.
            if (parameter.IsValueOrPrimitiveType())
            {
                // INFO: Wykonanie operacji 'Box' na aktualnym elemencie stosu (wartoœæ/referencja argumentu metody).
                msil.Emit(OpCodes.Box, parameter.SafeGetType());
            }

            // INFO: Skopiowanie wartoœci argumentu metody do tablicy obiektów.
            msil.Emit(OpCodes.Stelem_Ref);
        }

        public static void InvokeMockInterceptorMethod(ILGenerator msil, MethodInfo methodInfo, LocalBuilder arrayForArgumentTypesVariable, LocalBuilder arrayForArgumentValuesVariable)
        {
            // INFO: Od³o¿enie na stosie wskaŸnika 'this'.
            msil.Emit(OpCodes.Ldarg_0);

            // INFO: Od³o¿enie na stosie nazwy metody.
            msil.Emit(OpCodes.Ldstr, methodInfo.Name);

            // INFO: Od³o¿enie na stosie tablicy obiektów (Nazwy typów argumentów metody).
            msil.Emit(OpCodes.Ldloc, arrayForArgumentTypesVariable);

            // INFO: Od³o¿enie na stosie tablicy obiektów (Wartoœci argumentów metody).
            msil.Emit(OpCodes.Ldloc, arrayForArgumentValuesVariable);

            // INFO: Wywo³anie metody interceptora. Jeœli metoda zwraca wartoœæ to ta wartoœæ jest odk³adana na stos.
            msil.Emit(OpCodes.Call, MockObject.GetInterceptorMethodInfo());
        }

        public static void HandleMockInterceptorMethodReturnValue(ILGenerator msil, MethodInfo methodInfo)
        {
            // INFO: Jeœli zwracana wartoœæ jest typu wartoœciowego wtedy wymagana jest operacja 'Unbox'.
            if (methodInfo.ReturnType != typeof (void) && (methodInfo.ReturnType.IsValueOrPrimitiveType()))
            {
                // INFO: Wykonanie operacji 'Unbox' (na wskazany typ) na aktualnym elemencie stosu.
                msil.Emit(OpCodes.Unbox_Any, methodInfo.ReturnType);
            }
        }

        public static void AssignReferenceArgumentValues(ILGenerator msil, ParameterInfo[] parameters, LocalBuilder arrayForParameterValues)
        {
            for (var index = 0; index < parameters.Length; index++)
            {
                if (parameters[index].ParameterType.IsByRef)
                {
                    // INFO: Od³o¿enie na stosie wartoœci argumentu metody (w tym wypadku adres).
                    msil.Emit(OpCodes.Ldarg, index + 1);

                    // INFO: Od³o¿enie na stosie referencji ze zmiennej lokalnej tablicowej o podanym indeksie.
                    msil.Emit(OpCodes.Ldloc, arrayForParameterValues);
                    msil.Emit(OpCodes.Ldc_I4, index);
                    msil.Emit(OpCodes.Ldelem_Ref);

                    if (parameters[index].IsValueOrPrimitiveType())
                    {
                        // INFO: Skopiowanie obiektu o typie wartoœciowym pod wskazany adres argumentu o podanym indeksie (wymaga operacji 'Unbox').
                        msil.Emit(OpCodes.Unbox_Any, parameters[index].ParameterType.GetElementType());
                        msil.Emit(OpCodes.Stobj, parameters[index].ParameterType.GetElementType());
                    }
                    else
                    {
                        // INFO: Skopiowanie obiektu o typie referencyjnym pod wskazany adres argumentu o podanym indeksie.
                        msil.Emit(OpCodes.Stind_Ref);
                    }
                }
            }
        }

        public static void ReturnFromMethod(ILGenerator msil, MethodInfo methodInfo)
        {
            // INFO: Poniewa¿ metoda interceptora zwraca zawsze wartoœæ (obiekt o typie referencyjnyn) 
            //       to dla metod które nie zwracaj¹ wartoœci (void) nale¿y zdj¹æ wartoœæ ze stosu.
            if (methodInfo.ReturnType == typeof (void))
            {
                msil.Emit(OpCodes.Pop);
            }

            // INFO: Powrót z metody.
            msil.Emit(OpCodes.Ret);
        }
    }
}