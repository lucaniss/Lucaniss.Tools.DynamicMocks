using System;


namespace Lucaniss.Tools.Mocks.Tests.Data.Interfaces.Inheritance
{
    public interface IComplexInterface : ISimpleInterface
    {
        String EchoSecond();
    }
}