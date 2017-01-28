using Lucaniss.Tools.DynamicMocks.Exceptions;
using Lucaniss.Tools.DynamicMocks.Implementation;


namespace Lucaniss.Tools.DynamicMocks
{
    public static class Mock
    {
        public static IMockContainer<TInterface> Create<TInterface>() where TInterface : class
        {
            if (!typeof (TInterface).IsInterface)
            {
                throw MockExceptionHelper.TypeIsNotInterface(typeof (TInterface));
            }

            return new MockContainer<TInterface>(new MockBuilder<TInterface>().Build());
        }
    }
}