using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;

namespace GeniusCode.Components.Support
{
    internal class Logic<TMefContract, TMetadata>
        where TMetadata : class
    {

        private IMefExportPlaceHolder<TMefContract, TMetadata> GetSharedOrNonSharedPlaceholder()
        {
            IMefExportPlaceHolder<TMefContract, TMetadata> placeholder;
            if (_shareInstances)
                placeholder = new MySharedPlaceHolder();
            else
                placeholder = new MyNonSharedPlaceHolder();
            return placeholder;
        }

        private readonly CompositionContainer _container;
        private readonly bool _shareInstances;

        private IEnumerable<Lazy<TMefContract, TMetadata>> GetExports()
        {
            var placeholder = GetSharedOrNonSharedPlaceholder();

            _container.SatisfyImportsOnce(placeholder);
            return placeholder.ResolvedExports;
        }

        public TResult GetExportedValue<TResult,TArgs>(TArgs args,
                                                       Func<TArgs, Lazy<TMefContract, TMetadata>, bool> predicate, 
                                                       Func<TArgs, IEnumerable<Lazy<TMefContract, TMetadata>>, Lazy<TMefContract, TMetadata>> selector)
            where TResult: class, TMefContract
            where TArgs :class
        {

            var lazies = GetExports().ToList();

            if (predicate != null)
                lazies = lazies.Where(a => predicate(args, a)).ToList();

            var myLazy = lazies.Count() > 1 ? selector(args, lazies) : lazies.SingleOrDefault();

            if (myLazy == null)
                return null;

            return (TResult)myLazy.Value;
            
        }



        public Logic(CompositionContainer container, bool shareInstances)
        {
            _container = container;
            _shareInstances = shareInstances;
        }


        internal class MySharedPlaceHolder : IMefExportPlaceHolder<TMefContract, TMetadata>
        {
            [ImportMany(RequiredCreationPolicy = CreationPolicy.Shared)]
            public List<Lazy<TMefContract, TMetadata>> ResolvedExports { get; set; }
        }


        internal class MyNonSharedPlaceHolder : IMefExportPlaceHolder<TMefContract, TMetadata>
        {
            [ImportMany(RequiredCreationPolicy = CreationPolicy.NonShared)]
            public List<Lazy<TMefContract, TMetadata>> ResolvedExports { get; set; }
        }

    }
}