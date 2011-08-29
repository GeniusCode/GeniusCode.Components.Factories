using System;

namespace GeniusCode.Components.Factories.DepedencyInjection
{
    public class DIAbstractFactory<TDependency, T> : AbstractFactory<T>
        where T : class, IDependant<TDependency>
    {
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