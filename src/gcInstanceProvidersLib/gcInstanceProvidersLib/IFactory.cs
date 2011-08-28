using System;

namespace GeniusCode.FactoryModel
{
    public interface IFactory<T>
        where T : class
    {
        IFactoryOutput<T, R> GetInstance<R>(object args = null) where R : class, T;       
    }

    public interface IFactory<T, TArgs> : IFactory<T>
        where T : class
        where TArgs : class
    {
        IFactoryOutput<T, R> GetInstance<R>(TArgs args) where R : class, T;
    }



}
