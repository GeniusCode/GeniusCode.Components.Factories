using System;

namespace GeniusCode.FactoryModel.Factories
{
    public abstract class FactoryBase<T> : IFactory<T>
            where T : class
    {

        protected abstract bool TryBuild<R>(out bool wasCached, object args, out R result) where R : class, T;

        internal IFactoryOutput<T, R> GetResult<R>(object args = null) where R : class, T
        {
            bool wasCached;
            R output;

            var success = TryBuild<R>(out wasCached,args,  out output);

            if (success)
                return FactoryOutput<T, R>.NewSuccessfulInstance(output, wasCached, this);
            else
                return FactoryOutput<T, R>.NewFailureInstance(output, this);

        }

        public IFactoryOutput<T, R> GetInstance<R>(object args = null) where R : class, T
        {
            return GetResult<R>(args);
        }
    }
}
