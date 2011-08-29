using System.Collections.Generic;
using GeniusCode.Components.Factories;

namespace GeniusCode.Components
{
    public static class FactoryExtensions
    {
        public static void AddNewDefaultConstructorFactory<T>(this IList<IFactory<T>> input) where T : class, new()
        {
            input.Add(new DefaultConstructorFactory<T>());
        }

        public  static AbstractFactory<T> ToAbstractFactory<T>(this IList<IFactory<T>> input) where T: class
        {
            return new AbstractFactory<T>(input);
        }

    }
}
