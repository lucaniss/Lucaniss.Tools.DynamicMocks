using System;


namespace Lucaniss.Tools.DynamicMocks.Tests.Data
{
    internal class InvocationHandler
    {
        public Boolean IsInvoked { get; set; }
        public SimpleValueType SimpleValueTypeCheck { get; set; }
        public SimpleReferenceType SimpleReferenceTypeCheck { get; set; }
    }
}