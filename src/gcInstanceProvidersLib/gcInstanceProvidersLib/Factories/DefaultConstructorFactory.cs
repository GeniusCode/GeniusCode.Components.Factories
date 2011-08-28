using System;

namespace GeniusCode.FactoryModel.Factories
{

    public class DefaultConstructorFactory<T> : FactoryBase<T> where T : class
    {

        protected override bool TryBuild<R>(out bool wasCached, object args, out R result)
        {
            result = null;
            wasCached = false;

            if (TypeHasDefaultConstructor(typeof(R)))
            {
                result = Activator.CreateInstance<R>();
                return true;
            }
            return false;
        }

        private static bool TypeHasDefaultConstructor(Type type)
        {
            if (type.IsValueType)
                return true;

            var constructor = type.GetConstructor(Type.EmptyTypes);

            if (constructor == null)
                return false;

            return true;
        }

    }
}
