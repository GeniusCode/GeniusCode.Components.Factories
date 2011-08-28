using System.Collections.Generic;
using System;
//GeniusCode.Composition.ProviderModel
namespace GeniusCode.FactoryModel
{

    public class BindableAbstractFactory<T> : BindableAbstractFactory<T,object>
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

        protected virtual TArgs GetArgs()
        {
            return new TArgs();
        }

        public T Source
        {
            get
            {
                return ReturnValueForSourceRequest(GetArgs());
            }
        }

        protected virtual T ReturnValueForSourceRequest(TArgs args)
        {
            return GetInstance<T>(args);
        }
    }
}
