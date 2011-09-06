namespace GeniusCode.Components.Factories
{
    public abstract class FactoryBase<T, TAcquireArgs> : FactoryBase<T>
        where T : class
        where TAcquireArgs : class
    {

        protected abstract bool TryGetInstanceWithStrongArgs<TResult>(out bool wasCached, TAcquireArgs args, out TResult result)
            where TResult : class, T;


        protected override sealed bool TryGetInstance<TResult>(out bool wasCached, object args, out TResult result)
        {
            var stronglyTypedArgs = args as TAcquireArgs;
            var argsAreSupported = (args == null || stronglyTypedArgs != null);

            if (!argsAreSupported)
            {
                wasCached = false;
                result = null;
                return false;
            }

            return TryGetInstanceWithStrongArgs(out wasCached, stronglyTypedArgs, out result);
        }
    }
}