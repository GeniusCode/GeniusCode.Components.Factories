﻿using System;
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


        public class Person : IPeerChainDependant<Person,Train>
        {
            #region Implementation of IDependant<Train>

            public Train Dependency { get; private set; }

            public bool TrySetDependency(Train args)
            {
                Dependency = args;
                return true;
            }

            #endregion

            #region Implementation of IPeerChainDependant<Person,Train>

            IDIAbstractFactory<Train, Person> IPeerChainDependant<Person,Train>.Factory { get; set; }

            #endregion

            public Person GetAnotherPerson()
            {
                return (this as IPeerChainDependant<Person, Train>).Factory.GetInstance<Person>(this);
            }
        }
        [TestMethod]
        public void Should_reuse_dependency_in_result_instances()
        {
            var train = new Train();

            var factories = new List<IFactory<Person>>();
            factories.AddNewDefaultConstructorFactory();

            var trainStation = factories.ToDIAbstractFactory<Person, Train>();

            var p1 = trainStation.GetInstance(train);
            var p2 = trainStation.GetInstance(train);
            var p3 = trainStation.GetInstance(train); 
            var p4 = trainStation.GetInstance(train);

            AssertDependencyInjectionOccurred(p1, p2, p3, p4);
        }

        private static void AssertDependencyInjectionOccurred(Person p1, Person p2, Person p3, Person p4)
        {
            Assert.AreNotSame(p1, p2);
            Assert.AreNotSame(p2, p3);
            Assert.AreNotSame(p3, p4);

            Assert.AreSame(p1.Dependency, p2.Dependency);
            Assert.AreSame(p2.Dependency, p3.Dependency);
            Assert.AreSame(p3.Dependency, p4.Dependency);
        }

        [TestMethod]
        public void Should_propagate_factory_reference_in_peer_chain()
        {
            var train = new Train();

            var factories = new List<IFactory<Person>>();
            factories.AddNewDefaultConstructorFactory();

            var trainStation = factories.ToDIAbstractFactory<Person, Train>();

            var p1 = trainStation.GetInstance(train);

            var p2 = p1.GetAnotherPerson();
            var p3 = p2.GetAnotherPerson();
            var p4 = p3.GetAnotherPerson();

            AssertDependencyInjectionOccurred(p1,p2,p3,p4);

        }
    }
}
