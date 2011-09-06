namespace GeniusCode.Components
{
    
    public static class AbstractFactoryExtensions
    {
        public static void GetInstanceUsingAcquiredArgs<T, TResult, TAcquireArgs>(this IAbstractFactory<T> input, TAcquireArgs args, out TResult output)
            where T: class
            where TResult : class,T
            where TAcquireArgs : IAcquiredArgs<TResult>
        {
            output = input.GetInstance<TResult>(args);
        }
    }
}