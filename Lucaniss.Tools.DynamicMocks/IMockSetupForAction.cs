using System;


namespace Lucaniss.Tools.DynamicMocks
{
    public interface IMockSetupForAction
    {
        void Callback(Action mockAction);
        void Callback<T1>(Action<T1> mockAction);
        void Callback<T1, T2>(Action<T1, T2> mockAction);
        void Callback<T1, T2, T3>(Action<T1, T2, T3> mockAction);
        void Callback<T1, T2, T3, T4>(Action<T1, T2, T3, T4> mockAction);
        void Callback<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> mockAction);
        void Callback<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> mockAction);
        void Callback<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> mockAction);
        void Callback<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> mockAction);
        void Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> mockAction);
        void Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> mockAction);

        void CallbackDynamic(Action<Object[]> mockAction);
    }
}