using System;
using System.Reflection;

namespace GeniusCode.Components.Factories
{
    public class DefaultConstructorFactory<T> : FactoryBase<T> where T : class
    {
        protected override bool TryGetInstance<TResult>(out bool wasCached, object args, out TResult result)
        {
            result = null;
            wasCached = false;

            if (TypeHasDefaultConstructor(typeof (TResult)))
            {
                result = Activator.CreateInstance<TResult>();
                return true;
            }
            return false;
        }

        private static bool TypeHasDefaultConstructor(Type type)
        {
            if (type.IsValueType)
                return true;

            ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);

            if (constructor == null)
                return false;

            return true;
        }
    }
}