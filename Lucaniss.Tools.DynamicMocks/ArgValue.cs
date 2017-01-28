namespace Lucaniss.Tools.DynamicMocks
{
    public sealed class ArgValue<TValue>
    {
        private readonly TValue _value;

        public ArgValue(TValue value)
        {
            _value = value;
        }

        public static implicit operator TValue(ArgValue<TValue> item)
        {
            return item._value;
        }
    }
}