namespace GeniusCode.Components
{
    public interface IFactory<T>
        where T : class
    {
        IFactoryOutput<T, TResult> GetInstance<TResult>(object args = null) where TResult : class, T;
    }

}