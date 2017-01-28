using System;


namespace Lucaniss.Tools.DynamicMocks
{
    public interface IMockSetupForSetter<out TValue>
    {
        void Callback(Action<TValue> mockFunction);
    }
}