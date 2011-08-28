using System.Collections.Generic;

namespace GeniusCode.Components
{
    public class BindableAbstractFactory<T> : BindableAbstractFactory<T, object>
        where T : class
    {
        public BindableAbstractFactory(IEnumerable<IFactory<T>> providers = null)
            : base(providers)
        {
        }
    }

    public class BindableAbstractFactory<T, TArgs> : AbstractFactory<T>
        where T : class
        where TArgs : class, new()
    {
        public BindableAbstractFactory(IEnumerable<IFactory<T>> providers = null)
            : base(providers)
        {
        }

        public T Source
        {
            get { return ReturnValueForSourceRequest(GetArgs()); }
        }

        protected virtual TArgs GetArgs()
        {
            return new TArgs();
        }

        protected virtual T ReturnValueForSourceRequest(TArgs args)
        {
            return GetInstance<T>(args);
        }
    }
}