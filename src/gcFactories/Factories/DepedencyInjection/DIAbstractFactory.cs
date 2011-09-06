using System;
using System.Collections.Generic;

namespace GeniusCode.Components.Factories.DepedencyInjection
{
    public class DIAbstractFactory<TDependency, T> : IDIAbstractFactory<TDependency, T> where T : class, IDependant<TDependency>
        where TDependency : class
    {

        private readonly IAbstractFactory<T> _factory;

        public DIAbstractFactory(IAbstractFactory<T> factory)
        {
            _factory = factory;
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
            var output = _factory.GetInstance<TResult>(args);
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