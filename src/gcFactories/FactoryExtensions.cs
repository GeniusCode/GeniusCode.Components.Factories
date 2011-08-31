using System.Collections.Generic;
using GeniusCode.Components.Factories;
using GeniusCode.Components.Factories.DepedencyInjection;

namespace GeniusCode.Components
{
    public static class FactoryExtensions
    {
        public static void AddNewDefaultConstructorFactory<T>(this IList<IFactory<T>> input) where T : class, new()
        {
            input.Add(new DefaultConstructorFactory<T>());
        }

        public  static IAbstractFactory<T> ToAbstractFactory<T>(this IEnumerable<IFactory<T>> input) where T: class
        {
            return new AbstractFactory<T>(input);
        }

        public static DIAbstractFactory<TDependency,T> ToDIAbstractFactory<T, TDependency>(this IEnumerable<IFactory<T>> input) 
            where T : class, IDependant<TDependency>
            where TDependency : class
        {
            return new DIAbstractFactory<TDependency,T>(input);   
        }

    }
}
