using System;


namespace Lucaniss.Tools.DynamicMocks
{
    public interface IMockSetupForFunction<in TResult>
    {
        void Callback(TResult value);

        void Callback(Func<TResult> mockFunction);
        void Callback<T1>(Func<T1, TResult> mockFunction);
        void Callback<T1, T2>(Func<T1, T2, TResult> mockFunction);
        void Callback<T1, T2, T3>(Func<T1, T2, T3, TResult> mockFunction);
        void Callback<T1, T2, T3, T4>(Func<T1, T2, T3, T4, TResult> mockFunction);
        void Callback<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5, TResult> mockFunction);
        void Callback<T1, T2, T3, T4, T5, T6>(Func<T1, T2, T3, T4, T5, T6, TResult> mockFunction);
        void Callback<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> mockFunction);
        void Callback<T1, T2, T3, T4, T5, T6, T7, T8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> mockFunction);
        void Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> mockFunction);
        void Callback<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> mockFunction);

        void CallbackDynamic(Func<Object[], Object> mockAction);
    }
}