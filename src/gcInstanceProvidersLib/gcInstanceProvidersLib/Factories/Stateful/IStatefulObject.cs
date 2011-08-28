using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GeniusCode.FactoryModel.ProviderModel.Stateful
{
    public interface IStateful<TState>
    {
        TState State { get; }
        bool TrySetState(TState args);
    }

}
