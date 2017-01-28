namespace Lucaniss.Tools.DynamicMocks.Tests.Data.Interfaces
{
    public interface ITestInterfaceWithProperties
    {
        SimpleValueType PropertyForValueType { get; set; }
        SimpleReferenceType PropertyForReferenceType { get; set; }
    }
}