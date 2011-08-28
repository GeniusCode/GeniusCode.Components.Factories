using System;

namespace GeniusCode.FactoryModel
{

    public interface IAcquireResult<R>
        where R : class
    {
        R Result { get; }
        bool ConsiderResultCached { get; }
        bool ResultSuccessful { get; }
    }

    public interface IFactoryOutput<T, R> : IAcquireResult<R>
        where T : class
        where R : class, T
    {
        IFactory<T> FactoryUsed { get; }
    }




}
