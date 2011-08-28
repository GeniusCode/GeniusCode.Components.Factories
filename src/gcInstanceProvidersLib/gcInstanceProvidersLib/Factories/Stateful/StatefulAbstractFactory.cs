using System;

namespace GeniusCode.Components.Factories.Stateful
{
    public class StatefulAbstractFactory<TState, T> : AbstractFactory<T>
        where T : class, IStatefulObject<TState>
    {
        protected TResult GetInstance<TResult>(object args, TState state)
            where TResult : class, T
        {
            var output = GetInstance<TResult>(args);
            bool wasSuccessful = output.TrySetState(state);

            if (!wasSuccessful)
                throw new Exception("State was not set as expected");

            return output;
        }
    }
}