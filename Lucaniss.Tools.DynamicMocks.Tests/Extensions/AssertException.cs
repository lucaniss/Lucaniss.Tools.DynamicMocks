using System;


namespace Lucaniss.Tools.DynamicMocks.Tests.Extensions
{
    public static class AssertException
    {
        public static void Throws<TException>(Action action, Predicate<TException> exceptionPredicate = null)
            where TException : Exception
        {
            try
            {
                action.Invoke();
            }
            catch (TException e)
            {
                if (exceptionPredicate != null && !exceptionPredicate(e))
                {
                    throw new Exception("Exception predicate is not valid.");
                }

                Console.WriteLine(e.Message);
                return;
            }
            catch (Exception e)
            {
                throw new Exception($"Exception is of type '{e.GetType().FullName}' not of the type: '{typeof (TException).FullName}'.");
            }

            throw new Exception($"Exception ({typeof (TException).FullName}) wasn't thrown.");
        }
    }
}