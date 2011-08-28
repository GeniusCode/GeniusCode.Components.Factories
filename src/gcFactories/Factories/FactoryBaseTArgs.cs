namespace GeniusCode.Components.Factories
{
    public abstract class FactoryBase<T, TArgs> : FactoryBase<T>, IFactory<T, TArgs>
        where T : class
        where TArgs : class
    {
        #region IFactory<T,TArgs> Members

        public IFactoryOutput<T, R> GetInstance<R>(TArgs args) where R : class, T
        {
            return GetResult<R>(args);
        }

        #endregion

        protected abstract bool TryBuildWithStrongArgs<TResult>(out bool wasCached, TArgs args, out TResult result)
            where TResult : class, T;


        protected override sealed bool TryBuild<TResult>(out bool wasCached, object args, out TResult result)
        {
            var stronglyTypedArgs = args as TArgs;
            bool argsAreSupported = (args == null || stronglyTypedArgs != null);

            if (!argsAreSupported)
            {
                wasCached = false;
                result = null;
                return false;
            }

            return TryBuildWithStrongArgs(out wasCached, stronglyTypedArgs, out result);
        }
    }
}