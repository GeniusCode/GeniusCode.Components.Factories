namespace GeniusCode.Components.Factories.DepedencyInjection
{
    public interface IPeerChainDependant<TRoot, TDependency> : IDependant<TDependency>
        where TDependency : class
        where TRoot : class, IDependant<TDependency>
    {
        IDIAbstractFactory<TDependency, TRoot> Factory { get; set; }
    }
}