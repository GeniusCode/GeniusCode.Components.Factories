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
    }
}
