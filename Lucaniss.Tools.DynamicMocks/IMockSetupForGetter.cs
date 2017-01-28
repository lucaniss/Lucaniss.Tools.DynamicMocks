using System;


namespace Lucaniss.Tools.DynamicMocks
{
    public interface IMockSetupForGetter<in TResult>
    {
        void Callback(TResult value);
        void Callback(Func<TResult> mockFunction);
    }
}