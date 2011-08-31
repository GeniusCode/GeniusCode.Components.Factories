namespace GeniusCode.Components.Factories.DepedencyInjection
{
    public interface IPeerChainDependant<T, TDependency> : IDependant<TDependency> 
        where TDependency : class
        where T: class, IDependant<TDependency>
    {
        IDIAbstractFactory<TDependency, T> Factory { get; set; }
    }
}