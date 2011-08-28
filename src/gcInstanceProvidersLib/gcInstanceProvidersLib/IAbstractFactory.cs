using System;

namespace GeniusCode.FactoryModel
{
    public interface IAbstractFactory<T>
                where T : class
    {
        T GetInstance(object args);
        R GetInstance<R>(object args)
                where R : class, T;
    }
}
