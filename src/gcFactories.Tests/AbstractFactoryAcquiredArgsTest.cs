using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeniusCode.Components.Factories.Tests
{
    [TestClass]
    public class AbstractFactoryAcquiredArgsTest
    {

        public class EntityBase
        {
            
        }

        public class Person : EntityBase
        {
            public string Name { get; set; }   
        }



        public class AcquiredPersonArgsToSetName : IAcquiredArgs<Person>
        {
            public string Name { get; set; }

            public AcquiredPersonArgsToSetName(string name)
            {
                Name = name;
            }

            #region Implementation of ICreateArgs<Person>

            public Action<Person> GetOnAcquiredAction()
            {
                return p => p.Name = Name;
            }

            #endregion
        }

        [TestMethod]
        public void Should_fire_delegate_contained_on_acquired_args_class()
        {
            var args = new AcquiredPersonArgsToSetName("John");
            var factory = GetEntityBaseFactory();
            Person output;
            factory.GetInstanceUsingAcquiredArgs(args, out output);
            Assert.AreEqual("John", output.Name);
        }

        private static IAbstractFactory<EntityBase> GetEntityBaseFactory()
        {
            var factories = new List<IFactory<EntityBase>>();
            factories.AddNewDefaultConstructorFactory();
            return factories.ToAbstractFactory();
        }
    }


}
