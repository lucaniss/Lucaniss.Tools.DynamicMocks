namespace Lucaniss.Tools.DynamicMocks.Tests.Data.Interfaces
{
    public interface ITestInterfaceWithActions
    {
        void Action();

        void Action(SimpleValueType value);
        void Action(SimpleReferenceType value);

        void ActionWithOutParam(out SimpleValueType value);
        void ActionWithOutParam(out SimpleReferenceType value);

        void ActionWithRefParam(ref SimpleValueType value);
        void ActionWithRefParam(ref SimpleReferenceType value);

        void ActionWithGenericValueParameter<TValue>(TValue value) where TValue : struct;
        void ActionWithGenericReferenceParameter<TValue>(TValue value) where TValue : class;

        void ActionWithSignatureConflict1(SimpleValueType value);
        void ActionWithSignatureConflict1(ref SimpleValueType value);

        void ActionWithSignatureConflict2(SimpleReferenceType value);
        void ActionWithSignatureConflict2(ref SimpleReferenceType value);
    }
}