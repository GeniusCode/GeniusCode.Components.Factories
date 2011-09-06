namespace GeniusCode.Components
{
    public interface IAcquireResult<out TResult>
        where TResult : class
    {
        TResult Result { get; }
        bool ConsiderResultCached { get; }
        bool ResultSuccessful { get; }
     
    }

    public interface IFactoryOutput<T, out TResult> : IAcquireResult<TResult>
        where T : class
        where TResult : class, T
    {
        IFactory<T> FactoryUsed { get; }
        object ArgsSupplied { get; }
    }
}