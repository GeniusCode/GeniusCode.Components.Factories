using System;
using GeniusCode.Factory.ProviderModel.Support;

namespace GeniusCode.Components.Factories.Support
{
    /// <summary>
    /// Class that duplicates the behavior of Lazy in .net 4
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class LazyRelayObjectSource<T> : RelayObjectSource<T>
    {
        private bool _hasValue;

        private T _value;

        public LazyRelayObjectSource(Func<T> factory)
            : base(factory)
        {
        }

        public static implicit operator T(LazyRelayObjectSource<T> input)
        {
            return input.Value;
        }

        protected override T GetReturnValueForSource()
        {
            if (!_hasValue)
            {
                _value = Factory();
                _hasValue = true;
            }
            return _value;
        }
    }


    /// <summary>
    /// Class that operates similar to Lazy in .net, with metadata
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TMetadata">The type of the metadata.</typeparam>
    internal class LazyRelayObjectSource<T, TMetadata> : LazyRelayObjectSource<T>
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