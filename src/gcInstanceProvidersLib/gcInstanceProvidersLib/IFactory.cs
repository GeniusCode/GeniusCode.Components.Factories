namespace GeniusCode.Components
{
    public interface IFactory<T>
        where T : class
    {
        IFactoryOutput<T, TResult> GetInstance<TResult>(object args = null) where TResult : class, T;
    }

    public interface IFactory<T, in TArgs> : IFactory<T>
        where T : class
        where TArgs : class
    {
        IFactoryOutput<T, TResult> GetInstance<TResult>(TArgs args) where TResult : class, T;
    }
}