using System;


namespace Lucaniss.Tools.DynamicMocks.Tests.Data
{
    public struct SimpleValueType
    {
        public Guid Id { get; }

        public SimpleValueType(Guid id)
        {
            Id = id;
        }

        public override String ToString()
        {
            return $"Simple value: {Id}";
        }
    }
}