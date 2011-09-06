using System.Collections.Generic;

namespace GeniusCode.Components
{
    public class BindableAbstractFactory<T> : BindableAbstractFactory<T, object>
        where T : class
    {
        public BindableAbstractFactory(params IFactory<T>[] providers)
            : base(providers)
        {
        }
    }

    public class BindableAbstractFactory<T, TAcquireArgs> : AbstractFactory<T>
        where T : class
        where TAcquireArgs : class, new()
    {
        public BindableAbstractFactory(params IFactory<T>[] providers)
            : base(providers)
        {
        }

        public T Source
        {
            get { return ReturnValueForSourceRequest(GetArgs()); }
        }

        protected virtual TAcquireArgs GetArgs()
        {
            return new TAcquireArgs();
        }

        protected virtual T ReturnValueForSourceRequest(TAcquireArgs args)
        {
            return GetInstance<T>(args);
        }
    }
}