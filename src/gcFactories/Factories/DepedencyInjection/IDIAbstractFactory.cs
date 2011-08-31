namespace GeniusCode.Components.Factories.DepedencyInjection
{
    public interface IDIAbstractFactory<TDependency, in T> where TDependency : class where T : class, IDependant<TDependency>
    {
        TResult GetInstance<TResult>(IDependant<TDependency> dependant, object args = null)
            where TResult : class, T;
    }
}