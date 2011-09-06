using System.Collections.Generic;
using GeniusCode.Components.Factories;
using GeniusCode.Components.Factories.DepedencyInjection;

namespace GeniusCode.Components
{
    public static class FactoryExtensions
    {

        /// <summary>
        /// Adds the new default constructor factory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">The input.</param>
        /// <remarks>This does not require a parameterless constructor on type T</remarks>
        public static void AddNewDefaultConstructorFactory<T>(this IList<IFactory<T>> input) where T : class //,new() <-- NO!
        {
            input.Add(new DefaultConstructorFactory<T>());
        }

        public static IAbstractFactory<T> ToAbstractFactory<T>(this IEnumerable<IFactory<T>> input) where T : class
        {
            return new AbstractFactory<T>(input);
        }

        public static DIAbstractFactory<TDependency, T> ToDIAbstractFactory<T, TDependency>(this IEnumerable<IFactory<T>> input)
            where T : class, IDependant<TDependency>
            where TDependency : class
        {
            var factory = input.ToAbstractFactory();
            return new DIAbstractFactory<TDependency, T>(factory);
        }

    }
}
