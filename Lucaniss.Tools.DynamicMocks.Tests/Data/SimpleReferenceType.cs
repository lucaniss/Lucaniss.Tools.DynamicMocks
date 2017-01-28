using System;


namespace Lucaniss.Tools.DynamicMocks.Tests.Data
{
    public class SimpleReferenceType
    {
        public Guid Id { get; }

        public SimpleReferenceType(Guid id)
        {
            Id = id;
        }

        public override String ToString()
        {
            return $"Simple reference: {Id}";
        }
    }
}