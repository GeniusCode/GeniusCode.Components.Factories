using GeniusCode.Components.Factories.DepedencyInjection;

namespace GeniusCode.Components
{
    public static class DIAbstractFactoryExtensions
    {
        public static void GetInstanceUsingAcquiredArgs<TDependency, T, TResult, TAcquireArgs>(this IDIAbstractFactory<TDependency, T> input, TDependency dependency, TAcquireArgs args, out TResult output)
            where T : class, IDependant<TDependency>
            where TResult : class,T
            where TAcquireArgs : IAcquiredArgs<TResult>
            where TDependency : class
        {
            output = input.GetInstance<TResult>(dependency, args);
        }
    }
}