namespace GeniusCode.Components
{
    

    public interface IAbstractFactory<T>
        where T : class
    {
        T GetInstance(object args);

        TResult GetInstance<TResult>(object args)
            where TResult : class, T;

        TResult GetInstanceTypedByArgs<TResult, TArgs>(TArgs args) 
            where TResult: class,T 
            where TArgs : IAcquiredArgs<TResult>;
    }
}