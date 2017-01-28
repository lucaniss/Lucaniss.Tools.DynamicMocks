using System;


namespace Lucaniss.Tools.DynamicMocks.Tests.Data.Interfaces.Inheritance
{
    public interface IDerivedInterface : IBaseInterface
    {
        String EchoDerived();
    }
}