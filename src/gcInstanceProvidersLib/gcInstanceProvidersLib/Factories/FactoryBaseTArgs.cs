using System;

namespace GeniusCode.FactoryModel.Factories
{
    public abstract class FactoryBase<T, TArgs> : FactoryBase<T>, IFactory<T,TArgs>
        where T : class
        where TArgs : class
    {

        protected abstract bool TryBuildWithStrongArgs<R>(out bool wasCached, TArgs args, out R result) where R : class, T;


        protected sealed override bool TryBuild<R>(out bool wasCached, object args, out R result)
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

        public IFactoryOutput<T, R> GetInstance<R>(TArgs args) where R : class, T
        {
            return GetResult<R>(args);
        }
    }
}
