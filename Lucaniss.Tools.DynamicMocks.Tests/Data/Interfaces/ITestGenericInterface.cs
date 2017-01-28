namespace Lucaniss.Tools.DynamicMocks.Tests.Data.Interfaces
{
    public interface ITestGenericInterface<in TReferenceValue, in TSimpleValue>
        where TReferenceValue : class
        where TSimpleValue : struct
    {
    }
}