namespace GeniusCode.Components.Factories.DepedencyInjection
{
    public interface IDIAbstractFactory<in TDependency, in T> where TDependency : class where T : class, IDependant<TDependency>
    {
        TResult GetInstance<TResult>(TDependency dependancy, object args = null)
            where TResult : class, T;
    }
}