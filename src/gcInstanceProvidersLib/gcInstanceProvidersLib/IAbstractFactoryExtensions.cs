namespace GeniusCode.Components
{
    public static class AbstractFactoryExtensions
    {
        public static TResult GetInstance<TResult, T>(this IAbstractFactory<T> input)
            where T : class
            where TResult : class, T
        {
            return input.GetInstance<TResult>(null);
        }

        public static T GetInstance<T>(this IAbstractFactory<T> input)
            where T : class
        {
            return input.GetInstance(null);
        }
    }
}