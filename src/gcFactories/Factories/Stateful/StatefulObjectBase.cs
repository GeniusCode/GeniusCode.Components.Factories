namespace GeniusCode.Components.Factories.Stateful
{
    public class StatefulObjectBase<TState> : IStatefulObject<TState>
    {
        private TState _state;

        protected TState State
        {
            get { return _state; }
            private set
            {
                _state = value;
                OnStateSet();
            }
        }

        #region Impementations

        TState IStatefulObject<TState>.State
        {
            get { return State; }
        }

        bool IStatefulObject<TState>.TrySetState(TState state)
        {
            return TrySetState(state);
        }

        #endregion

        protected virtual void OnStateSet()
        {
        }

        protected virtual bool TrySetState(TState state)
        {
            State = state;
            return true;
        }
    }
}