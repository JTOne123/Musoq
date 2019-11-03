namespace Musoq.Plugins
{
    public class DeferredValue<T>
    {
        public T Value { get; set; }
    }

    public class DeferredInt : DeferredValue<int>
    {
        public static implicit operator int(DeferredInt defferedInt)
        {
            return defferedInt.Value;
        }
    }

    public class DeferredDouble : DeferredValue<double>
    {
        public static implicit operator double(DeferredDouble deferredDouble)
        {
            return deferredDouble.Value;
        }
    }

    public class DeferredDecimal : DeferredValue<decimal>
    {
        public static implicit operator decimal(DeferredDecimal deferredDouble)
        {
            return deferredDouble.Value;
        }
    }

    public class DeferredLong : DeferredValue<long>
    {
        public static implicit operator decimal(DeferredLong deferredLong)
        {
            return deferredLong.Value;
        }
    }
}