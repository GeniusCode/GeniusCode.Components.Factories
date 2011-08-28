using System;

namespace GeniusCode.FactoryModel.ProviderModel.Stateful
{
    public class StatefulBase<TState> : IStateful<TState>
    {
        protected virtual void OnStateSet()
        { }

        private TState _State;
        protected TState State
        {
            get
            {
                return _State;
            }
            private set
            {
                _State = value;
                OnStateSet();
            }
        }

        #region Impementations

        TState IStateful<TState>.State
        {
            get
            {
                return this.State;
            }
        }
        bool IStateful<TState>.TrySetState(TState state)
        {
            return TrySetState(state);
        }
        #endregion

        protected virtual bool TrySetState(TState state)
        {
            this.State = state;
            return true;
        }
    }
}
