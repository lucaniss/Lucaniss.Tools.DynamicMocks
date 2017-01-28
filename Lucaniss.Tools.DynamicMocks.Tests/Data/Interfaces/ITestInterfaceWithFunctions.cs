using System;


namespace Lucaniss.Tools.DynamicMocks.Tests.Data.Interfaces
{
    public interface ITestInterfaceWithFunctions
    {
        Object Function();

        SimpleValueType Function(SimpleValueType value);
        SimpleReferenceType Function(SimpleReferenceType value);
    }
}