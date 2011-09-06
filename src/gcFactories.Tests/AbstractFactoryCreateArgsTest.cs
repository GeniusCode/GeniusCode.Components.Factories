using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeniusCode.Components.Factories.Tests
{
    [TestClass]
    public class AbstractFactoryCreateArgsTest
    {

        public class EntityBase
        {
            
        }

        public class Person : EntityBase
        {
            public string Name { get; set; }   
        }



        public class AcquiredPersonArgs : IAcquiredArgs<Person>
        {
            public string Name { get; set; }

            public AcquiredPersonArgs(string name)
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
        public void Should_fire_delegate_contained_on_args_class()
        {
            var args = new AcquiredPersonArgs("John");
            var factory = GetEntityBaseFactory();
            Person output = factory.GetInstanceTypedByArgs<Person,AcquiredPersonArgs>(args);
            Assert.AreEqual("John", output.Name);
        }

        private IAbstractFactory<EntityBase> GetEntityBaseFactory()
        {
            var factories = new List<IFactory<EntityBase>>();
            factories.AddNewDefaultConstructorFactory();
            return factories.ToAbstractFactory();
        }
    }


}
