namespace GeniusCode.Components.Factories.DepedencyInjection
{
    public class DependantBase<TState> : IDependant<TState>
    {
        private TState _depedency;

        protected TState Depedency
        {
            get { return _depedency; }
            private set
            {
                _depedency = value;
                OnStateSet();
            }
        }

        #region Impementations

        TState IDependant<TState>.Dependency
        {
            get { return Depedency; }
        }

        bool IDependant<TState>.TrySetDependency(TState state)
        {
            return TrySetState(state);
        }

        #endregion

        protected virtual void OnStateSet()
        {
        }

        protected virtual bool TrySetState(TState state)
        {
            Depedency = state;
            return true;
        }
    }
}