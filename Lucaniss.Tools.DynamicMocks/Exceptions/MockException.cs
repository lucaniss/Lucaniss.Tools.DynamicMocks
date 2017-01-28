using System;


namespace Lucaniss.Tools.DynamicMocks.Exceptions
{
    public class MockException : Exception
    {
        public MockExceptionErrors Error { get; }


        public MockException(MockExceptionErrors error, String message)
            : base(message)
        {
            Error = error;
        }
    }
}