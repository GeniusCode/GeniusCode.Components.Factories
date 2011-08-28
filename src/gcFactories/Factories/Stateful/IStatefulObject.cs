namespace GeniusCode.Components.Factories.Stateful
{
    public interface IStatefulObject<TState>
    {
        TState State { get; }
        bool TrySetState(TState args);
    }
}