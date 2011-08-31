using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeniusCode.Components.Factories.Tests
{
    [TestClass]
    public class FactoryTests
    {
        public class Person
        {
        }

        [TestMethod]
        public void Should_create_instance_with_default_counstructor_factory()
        {
            var factories = new List<IFactory<Person>>();
            factories.AddNewDefaultConstructorFactory();

            var af = factories.ToAbstractFactory();

            var person = af.GetInstance(null);

            Assert.IsNotNull(person);
        }
    }
}