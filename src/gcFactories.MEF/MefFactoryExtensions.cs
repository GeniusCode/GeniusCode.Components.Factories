using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;

namespace GeniusCode.Components
{
    public static class MefFactoryExtensions
    {

        public static void AddMefLocatorProvider<T, TMefContract, TMetadata>(this IList<IFactory<T>> input,
            CompositionContainer container, bool shareInstances,
            Func<object, IEnumerable<Lazy<TMefContract, TMetadata>>, Lazy<TMefContract, TMetadata>> selector = null,
            Func<object, Lazy<TMefContract, TMetadata>, bool> predicate = null)
            where T : class, TMefContract
            where TMefContract : class
            where TMetadata : class
        {
            var mp = new MefInstanceProvider<T, TMefContract, TMetadata>(container, shareInstances) { Predicate = predicate, Selector = selector };
            input.Add(mp);
        }


        public static void AddMefLocatorProvider<T, TMefContract, TMetadata, TArgs>(this IList<IFactory<T>> input,
        CompositionContainer container, bool shareInstances,
        Func<TArgs, IEnumerable<Lazy<TMefContract, TMetadata>>, Lazy<TMefContract, TMetadata>> selector = null,
        Func<TArgs, Lazy<TMefContract, TMetadata>, bool> predicate = null )
            where T : class, TMefContract
            where TMefContract : class
            where TMetadata : class
            where TArgs : class
        {
            var mp = new MefInstanceProvider<T, TMefContract, TMetadata, TArgs>(container, shareInstances) { Predicate = predicate, Selector = selector };
            input.Add(mp);
        }

    }
}
