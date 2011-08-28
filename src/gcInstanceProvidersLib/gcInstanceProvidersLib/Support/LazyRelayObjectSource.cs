using System;

namespace GeniusCode.FactoryModel
{




    /// <summary>
    /// Class that duplicates the behavior of Lazy in .net 4
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LazyRelayObjectSource<T> : RelayObjectSource<T>
    {

        public static implicit operator T(LazyRelayObjectSource<T> input)
        {
            return input.Value;
        }

        bool hasValue;

        public LazyRelayObjectSource(Func<T> factory)
            : base(factory)
        {
        }

        private T _Value;
        protected override T GetReturnValueForSource()
        {
            if (!hasValue)
            {
                _Value = _Factory();
                hasValue = true;
            }
            return _Value;
        }
    }


    /// <summary>
    /// Class that operates similar to Lazy in .net, with metadata
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TMetadata">The type of the metadata.</typeparam>
    public class LazyRelayObjectSource<T, TMetadata> : LazyRelayObjectSource<T>
        where TMetadata : class
    {


        public LazyRelayObjectSource(Func<T> factory, TMetadata metadata)
            : base(factory)
        {
            Metadata = metadata;
        }

        public TMetadata Metadata { get; private set; }

    }

}
