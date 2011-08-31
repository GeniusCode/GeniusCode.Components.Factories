namespace GeniusCode.Components.Factories.DepedencyInjection
{

    public interface IPeerChainDependant<TRoot, TDependency> : IDependant<TDependency>
        where TRoot : class, IDependant<TDependency>
        where TDependency : class
    {
        IDIAbstractFactory<TDependency, TRoot> Factory { get; set; }
    }
}