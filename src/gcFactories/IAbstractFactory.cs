namespace GeniusCode.Components
{
    public interface IAbstractFactory<T>
        where T : class
    {
        T GetInstance(object args);

        TResult GetInstance<TResult>(object args)
            where TResult : class, T;
    }
}