using System;
using Lucaniss.Tools.DynamicMocks.Resources;


namespace Lucaniss.Tools.DynamicMocks.Exceptions
{
    internal static class MockExceptionHelper
    {
        public static Exception TypeIsNotInterface(Type interfaceType)
        {
            var message = String.Format(ExceptionResources.TypeIsNotInterface, interfaceType);
            return new MockException(MockExceptionErrors.TypeIsNotInterface, message);
        }

        public static Exception MethodHasNoSetup(String methodName)
        {
            var message = String.Format(ExceptionResources.MethodHasNoSetup, methodName);
            return new MockException(MockExceptionErrors.MethodHasNoSetup, message);
        }

        public static Exception MethodHasNoInvocationCallback(String methodName)
        {
            var message = String.Format(ExceptionResources.MethodHasNoInvocationCallback, methodName);
            return new MockException(MockExceptionErrors.MethodHasNoInvocationCallback, message);
        }

        public static Exception MethodSetupConflict(String methodName)
        {
            var message = String.Format(ExceptionResources.MethodSetupConflict, methodName);
            return new MockException(MockExceptionErrors.MethodSetupConflict, message);
        }

        public static Exception ExpressionIsNotMethodCallExpression()
        {
            return new MockException(MockExceptionErrors.ExpressionIsNotMethodCallExpression, ExceptionResources.ExpressionIsNotMethodCallExpression);
        }

        public static Exception ExpressionIsNotMemberExpression()
        {
            return new MockException(MockExceptionErrors.ExpressionIsNotMemberExpression, ExceptionResources.ExpressionIsNotMemberExpression);
        }
    }
}