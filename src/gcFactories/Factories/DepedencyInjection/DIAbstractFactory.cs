using System;
using System.Collections.Generic;

namespace GeniusCode.Components.Factories.DepedencyInjection
{
    public class DIAbstractFactory<TDependency, T> : AbstractFactory<T>
        where T : class, IDependant<TDependency>
    {
        public DIAbstractFactory(IEnumerable<IFactory<T>> providers) : base(providers)
        {
        }

        protected TResult GetInstance<TResult>(object args, TDependency dependency)
            where TResult : class, T
        {
            var output = GetInstance<TResult>(args);
            var wasSuccessful = output.TrySetDependency(dependency);

            if (!wasSuccessful)
                throw new Exception("Depedency was not set as expected");

            return output;
        }
    }
}