using GeniusCode.Components.Factories.Support;
using GeniusCode.Factory.ProviderModel.Support;

namespace GeniusCode.Components.Factories
{
    public abstract class FactoryBase<T> : IFactory<T>
        where T : class
    {
        #region IFactory<T> Members

        public IFactoryOutput<T, R> GetInstance<R>(object args = null) where R : class, T
        {
            return GetResult<R>(args);
        }

        #endregion

        protected abstract bool TryBuild<TResult>(out bool wasCached, object args, out TResult result)
            where TResult : class, T;

        internal IFactoryOutput<T, TResult> GetResult<TResult>(object args = null) where TResult : class, T
        {
            bool wasCached;
            TResult output;

            var success = TryBuild(out wasCached, args, out output);

            if (success)
                return FactoryOutput<T, TResult>.NewSuccessfulInstance(output, wasCached, this);
            
            return FactoryOutput<T, TResult>.NewFailureInstance(output, this);
        }
    }
}