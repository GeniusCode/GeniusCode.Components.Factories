namespace GeniusCode.Components.Factories.DepedencyInjection
{

    public interface IDependant<TDependency>
    {
        TDependency Dependency { get; }
        bool TrySetDependency(TDependency args);
    }
}