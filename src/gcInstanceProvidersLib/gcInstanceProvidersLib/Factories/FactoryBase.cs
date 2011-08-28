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

        internal IFactoryOutput<T, R> GetResult<R>(object args = null) where R : class, T
        {
            bool wasCached;
            R output;

            bool success = TryBuild(out wasCached, args, out output);

            if (success)
                return FactoryOutput<T, R>.NewSuccessfulInstance(output, wasCached, this);
            else
                return FactoryOutput<T, R>.NewFailureInstance(output, this);
        }
    }
}