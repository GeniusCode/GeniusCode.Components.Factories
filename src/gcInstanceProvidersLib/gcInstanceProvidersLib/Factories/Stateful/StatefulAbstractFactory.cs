using System;
using GeniusCode.FactoryModel;
using GeniusCode.FactoryModel.ProviderModel.Stateful;

namespace GeniusCode.Factory.ProviderModel.Stateful
{
    public class StatefulAbstractFactory<TState, T> : AbstractFactory<T>
                where T : class, IStateful<TState>
    {

        
        protected TResult GetInstance<TResult>(object args, TState state)
            where TResult : class, T
        {
            var output = base.GetInstance<TResult>(args);
            var wasSuccessful = output.TrySetState(state);

            if (!wasSuccessful)
                throw new Exception("State was not set as expected");

            return output;
        }

    }
}
