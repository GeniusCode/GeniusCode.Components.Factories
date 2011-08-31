using System;
using System.Collections.Generic;

namespace GeniusCode.Components.Factories.DepedencyInjection
{
    public class DIAbstractFactory<TDependency, T> : AbstractFactory<T>, IDIAbstractFactory<TDependency, T> where T : class, IDependant<TDependency>
        where TDependency : class
    {
        public DIAbstractFactory(IEnumerable<IFactory<T>> providers) : base(providers)
        {
        }

        public TResult GetInstance<TResult>(IDependant<TDependency> dependant, object args = null)
            where TResult : class, T
        {
            return GetInstance<TResult>(dependant.Dependency, args);
        }

        public T GetInstance(TDependency dependency, object args = null)
        {
            return GetInstance<T>(dependency, args);
        }

        public TResult GetInstance<TResult>(TDependency dependency, object args = null)
            where TResult : class, T
        {
            var output = GetInstance<TResult>(args);
            var wasSuccessful = output.TrySetDependency(dependency);

            TrySetPeerChain(output);

            if (!wasSuccessful)
                throw new Exception("Depedency was not set as expected");

            return output;
        }

        private void TrySetPeerChain<TResult>(TResult output) where TResult : class, T
        {
            var asPeerChain = output as IPeerChainDependant<T, TDependency>;
            if ((asPeerChain) != null)
                asPeerChain.Factory = this;
        }
    }
}