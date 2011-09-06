using GeniusCode.Components.Factories.Support;

namespace GeniusCode.Components.Factories
{
    public abstract class FactoryBase<T> : IFactory<T>
        where T : class
    {
        #region IFactory<T> Members

        public IFactoryOutput<T, TResult> GetInstance<TResult>(object args = null) where TResult : class, T
        {
            return GetResult<TResult>(args);
        }

        #endregion

        protected abstract bool TryGetInstance<TResult>(out bool wasCached, object args, out TResult result)
            where TResult : class, T;

        internal IFactoryOutput<T, TResult> GetResult<TResult>(object args = null) where TResult : class, T
        {
            bool wasCached;
            TResult output;

            var success = TryGetInstance(out wasCached, args, out output);

            if (success)
                return FactoryOutput<T, TResult>.NewSuccessfulInstance(output, args, wasCached, this);
            
            return FactoryOutput<T, TResult>.NewFailureInstance(output,args, this);
        }
    }
}