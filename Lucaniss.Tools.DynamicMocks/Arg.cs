namespace Lucaniss.Tools.DynamicMocks
{
    public static class Arg
    {
        public static TValue Any<TValue>()
        {
            return new ArgValue<TValue>(default(TValue));
        }
    }
}