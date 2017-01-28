namespace Lucaniss.Tools.DynamicMocks.Exceptions
{
    public enum MockExceptionErrors
    {
        TypeIsNotInterface,
        MethodHasNoSetup,
        MethodHasNoInvocationCallback,
        MethodSetupConflict,
        ExpressionIsNotMethodCallExpression,
        ExpressionIsNotMemberExpression
    }
}