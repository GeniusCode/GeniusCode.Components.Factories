using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using GeniusCode.Components.Factories;
using GeniusCode.Components.Support;

namespace GeniusCode.Components
{


    public class MefInstanceProvider<T, TMefContract, TMetadata, TArgs> : FactoryBase<T, TArgs>
        where T : class, TMefContract
        where TMetadata : class
        where TArgs : class
    {

        private readonly Logic<TMefContract, TMetadata> _logic;

        public MefInstanceProvider(CompositionContainer container, bool shareInstances)
        {
            _logic = new Logic<TMefContract, TMetadata>(container, shareInstances);
        }

        #region Public Members
        public Func<TArgs, Lazy<TMefContract, TMetadata>, bool> Predicate { get; set; }
        public Func<TArgs, IEnumerable<Lazy<TMefContract, TMetadata>>, Lazy<TMefContract, TMetadata>> Selector { get; set; }
        #endregion

        #region Overrides of FactoryBase<T,TArgs>

        protected override bool TryBuildWithStrongArgs<TResult>(out bool wasCached, TArgs args, out TResult result)
        {
            wasCached = false;
            result = _logic.GetExportedValue<TResult, TArgs>(args, Predicate, Selector);
            return result != null;
        }

        #endregion
    }


    public class MefInstanceProvider<T, TMefContract, TMetadata> : FactoryBase<T>
        where T : class, TMefContract
        where TMetadata : class
    {

        public Func<object, Lazy<TMefContract, TMetadata>, bool> Predicate { get; set; }
        public Func<object, IEnumerable<Lazy<TMefContract, TMetadata>>, Lazy<TMefContract, TMetadata>> Selector { get; set; }

        private readonly Logic<TMefContract, TMetadata> _logic;

        public MefInstanceProvider(CompositionContainer container, bool shareInstances)
        {
            _logic = new Logic<TMefContract, TMetadata>(container, shareInstances);
        }

        #region Overrides of FactoryBase<T>

        protected override bool TryBuild<TResult>(out bool wasCached, object args, out TResult result)
        {
            wasCached = false;
            result = _logic.GetExportedValue<TResult, object>(args, Predicate, Selector);
            return result != null;
        }

        #endregion
    }



}
