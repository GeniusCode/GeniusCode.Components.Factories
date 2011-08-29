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
            var f = new DefaultConstructorFactory<Person>();


            var af = new AbstractFactory<Person>(f);

            var person = af.GetInstance();

            Assert.IsNotNull(person);
        }
    }
}