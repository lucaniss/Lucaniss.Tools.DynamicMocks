using System;


namespace Lucaniss.Tools.DynamicMocks.Implementation
{
    internal sealed class ArgValueMock
    {
        public Type Type { get; }

        public ArgValueMock(Type type)
        {
            Type = type;
        }
    }
}