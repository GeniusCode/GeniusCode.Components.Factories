using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;

namespace GeniusCode.Components
{
    public class MefFactoryExtensions
    {

        public static void AddMefLocatorProvider<T, TMefContract, TMetadata>(this IList<IFactory<T>> input,
            CompositionContainer container,
            Func<object, IEnumerable<Lazy<TMefContract, TMetadata>>, Lazy<TMefContract, TMetadata>> selector,
            Func<object, Lazy<TMefContract, TMetadata>, bool> predicate, bool shareInstances)
            where T : class, TMefContract
            where TMefContract : class
            where TMetadata : class
        {
            var mp = new MefInstanceProvider<T, TMefContract, TMetadata>(container, shareInstances) { Predicate = predicate, Selector = selector };
            input.Add(mp);
        }


        public static void AddMefLocatorProvider<T, TMefContract, TMetadata, TArgs>(this IList<IFactory<T>> input,
        CompositionContainer container,
        Func<TArgs, IEnumerable<Lazy<TMefContract, TMetadata>>, Lazy<TMefContract, TMetadata>> selector,
        Func<TArgs, Lazy<TMefContract, TMetadata>, bool> predicate, bool shareInstances)
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
