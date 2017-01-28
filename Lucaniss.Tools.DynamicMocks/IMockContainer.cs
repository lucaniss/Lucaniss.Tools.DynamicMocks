using System;
using System.Linq.Expressions;


namespace Lucaniss.Tools.DynamicMocks
{
    public interface IMockContainer<TInterface>
    {
        TInterface Instance { get; }

        IMockSetupForAction SetupMethod(Expression<Action<TInterface>> methodExpression);
        IMockSetupForFunction<TResult> SetupMethod<TResult>(Expression<Func<TInterface, TResult>> methodExpression);

        IMockSetupForGetter<TValue> SetupGetter<TValue>(Expression<Func<TInterface, TValue>> getterExpression);
        IMockSetupForSetter<TValue> SetupSetter<TValue>(Expression<Func<TInterface, TValue>> setterExpression, TValue value);
    }
}