using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Lucaniss.Tools.DynamicMocks.Exceptions;


namespace Lucaniss.Tools.DynamicMocks.Implementation
{
    internal sealed class MockContainer<TInterface> : IMockContainer<TInterface>
        where TInterface : class
    {
        private readonly IMockObject _mockObject;
        public TInterface Instance => (TInterface) _mockObject;


        public MockContainer(IMockObject mockObject)
        {
            _mockObject = mockObject;
        }


        public IMockSetupForAction SetupMethod(Expression<Action<TInterface>> methodExpression)
        {
            return CreateMockSetupForMethod(methodExpression, (methodInfo, parameterTypes, parameterValues) =>
            {
                return new MockSetupForAction(methodInfo, parameterTypes, parameterValues);
            });
        }

        public IMockSetupForFunction<TResult> SetupMethod<TResult>(Expression<Func<TInterface, TResult>> methodExpression)
        {
            return CreateMockSetupForMethod(methodExpression, (methodInfo, parameterTypes, parameterValues) =>
            {
                return new MockSetupForFunction<TResult>(methodInfo, parameterTypes, parameterValues);
            });
        }

        public IMockSetupForGetter<TValue> SetupGetter<TValue>(Expression<Func<TInterface, TValue>> getterExpression)
        {
            var memberExpression = getterExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw MockExceptionHelper.ExpressionIsNotMemberExpression();
            }

            var methodInfo = typeof (TInterface).GetProperty(memberExpression.Member.Name).GetMethod;

            var mockSetup = new MockSetupForGetter<TValue>(methodInfo, new Type[] { }, new Object[] { });
            _mockObject.Setups.Add(mockSetup);

            return mockSetup;
        }

        public IMockSetupForSetter<TValue> SetupSetter<TValue>(Expression<Func<TInterface, TValue>> setterExpression, TValue value)
        {
            var memberExpression = setterExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw MockExceptionHelper.ExpressionIsNotMemberExpression();
            }

            var methodInfo = typeof (TInterface).GetProperty(memberExpression.Member.Name).SetMethod;

            var mockSetup = new MockSetupForSetter<TValue>(methodInfo, new[] { typeof (TValue) }, new Object[] { value });
            _mockObject.Setups.Add(mockSetup);

            return mockSetup;
        }


        private TSetup CreateMockSetupForMethod<TSetup>(LambdaExpression expression, Func<MethodInfo, IReadOnlyList<Type>, IReadOnlyList<Object>, TSetup> activatorDelegate)
            where TSetup : IMockSetup
        {
            var methodCallExpression = expression.Body as MethodCallExpression;
            if (methodCallExpression == null)
            {
                throw MockExceptionHelper.ExpressionIsNotMethodCallExpression();
            }

            var methodInfo = methodCallExpression.Method;

            var parameterTypes = GetParameterTypes(methodCallExpression).ToList();
            var parameterValues = GetParameterValues(methodCallExpression).ToList();

            var mockSetup = activatorDelegate(methodInfo, parameterTypes, parameterValues);
            _mockObject.Setups.Add(mockSetup);

            return mockSetup;
        }

        private static IEnumerable<Type> GetParameterTypes(MethodCallExpression expression)
        {
            return expression.Method.GetParameters().Select(e => e.ParameterType);
        }

        private static IEnumerable<Object> GetParameterValues(MethodCallExpression expression)
        {
            foreach (var argumentExpresion in expression.Arguments)
            {
                var argumentExpressionAsWildcard = (argumentExpresion as MethodCallExpression);
                if (argumentExpressionAsWildcard?.Method.DeclaringType?.Name == nameof(Arg))
                {
                    yield return new ArgValueMock(argumentExpressionAsWildcard.Method.ReturnType);
                }
                else
                {
                    var lambda = Expression.Lambda<Func<Object>>(Expression.Convert(argumentExpresion, typeof (Object)), null);
                    yield return lambda.Compile().Invoke();
                }
            }
        }
    }
}