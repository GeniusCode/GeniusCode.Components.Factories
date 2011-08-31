using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using GeniusCode.Components.Factories.DepedencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeniusCode.Components.Factories.Tests
{
    [TestClass]
    public class DIFactoryTests
    {
        public class Train
        {

            public Train()
            {
                Id = Guid.NewGuid();
            }

            public Guid Id { get; private set; }
        }


        public class Person : IDependant<Train>
        {
            #region Implementation of IDependant<Train>

            public Train Dependency { get; private set; }

            public bool TrySetDependency(Train args)
            {
                Dependency = args;
                return true;
            }

            #endregion
        }
        [TestMethod]
        public void TestMethod1()
        {
            var train = new Train();

            var factories = new List<IFactory<Person>>();
            factories.AddNewDefaultConstructorFactory();

            var trainStation = factories.ToDIAbstractFactory<Person, Train>();

            var p1 = trainStation.GetInstance(train);
            var p2 = trainStation.GetInstance(train);
            var p3 = trainStation.GetInstance(train); 
            var p4 = trainStation.GetInstance(train);

            Assert.AreNotSame(p1,p2);
            Assert.AreNotSame(p2, p3);
            Assert.AreNotSame(p3, p4);

            Assert.AreSame(p1.Dependency, p2.Dependency);
            Assert.AreSame(p2.Dependency, p3.Dependency);
            Assert.AreSame(p3.Dependency, p4.Dependency);



        }
    }
}
