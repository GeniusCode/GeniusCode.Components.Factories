using System;

namespace GeniusCode.FactoryModel
{
    public static class IAbstractFactoryExtensions
    {
        public static R GetInstance<R, T>(this IAbstractFactory<T> input)
            where T : class
            where R : class, T
        {
            return input.GetInstance<R>(null);
        }

        public static T GetInstance<T>(this IAbstractFactory<T> input)
            where T : class
        {
            return input.GetInstance(null);
        }
    }
}
