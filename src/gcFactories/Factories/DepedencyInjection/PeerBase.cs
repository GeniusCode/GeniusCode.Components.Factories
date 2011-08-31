namespace GeniusCode.Components.Factories.DepedencyInjection
{
    public abstract class PeerBase<T, TDependency> : IPeerChainDependant<T, TDependency>
        where TDependency : class
        where T : class, IDependant<TDependency>
    {
        #region Implementation of IDependant<TDependency>

        public bool TrySetDependency(TDependency args)
        {

            //TODO: Write OnDependency Changing/Changed
            _dependency = args;
            return true;
        }

        private TDependency _dependency;
        TDependency IDependant<TDependency>.Dependency
        {
            get { return _dependency; }
        }

        #endregion

        #region Implementation of IPeerChainDependant<T,TDependency>

        protected IDIAbstractFactory<TDependency, T> Factory { get; private set; }

        IDIAbstractFactory<TDependency, T> IPeerChainDependant<T, TDependency>.Factory
        {
            get { return Factory; }
            set { Factory = value; }
        }

        #endregion
    }
}