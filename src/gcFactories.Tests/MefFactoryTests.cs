using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeniusCode.Components.Factories.Tests
{
    [TestClass]
    public class MefFactoryTests
    {

        public interface IPerson
        {

        }

        public interface ICountryMetadata
        {
            string Country { get; }
            int Rank { get; }
        }

        [Export(typeof(IPerson))]
        [ExportMetadata("Country", "USA")]
        [ExportMetadata("Rank", 1)]
        public class American : IPerson
        {

        }

        [Export(typeof(IPerson))]
        [ExportMetadata("Country", "Canada")]
        [ExportMetadata("Rank", 2)]
        public class Canadian : IPerson
        {

        }

        [Export(typeof(IPerson))]
        [ExportMetadata("Country", "Mexico")]
        [ExportMetadata("Rank", 3)]
        public class Mexican : IPerson
        {

        }

        [TestMethod]
        public void ShouldGetSameInstance()
        {

            IAbstractFactory<American> af = GetSimpleMefAbstractFactory("USA", true);

            var a1 = af.GetInstance(null);
            var a2 = af.GetInstance(null);

            Assert.AreSame(a1, a2);
        }

        [TestMethod]
        public void ShouldGetUniqueInstance()
        {

            var af = GetSimpleMefAbstractFactory("USA", false);

            var a1 = af.GetInstance(null);
            var a2 = af.GetInstance(null);

            Assert.AreNotSame(a1, a2);
        }



        private IAbstractFactory<American> GetSimpleMefAbstractFactory(string countryName, bool reuseInstances)
        {
            var assemblyCatalog = new AssemblyCatalog(GetType().Assembly);
            var container = new CompositionContainer(assemblyCatalog);

            var list = new List<IFactory<American>>();

            list.AddMefLocatorProvider<American, IPerson, ICountryMetadata>(container, reuseInstances, null, (a, m) => m.Metadata.Country == countryName);
            var af = list.ToAbstractFactory();
            return af;
        }


        [TestMethod]
        public void Should_use_selector_to_pick_from_multiple_exports()
        {
            var hi = GetMefAbstractFactoryWithSelector();

            var a1 = hi.GetInstance(null);

            Assert.IsNotNull(a1);
            Assert.AreEqual(typeof(American), a1.GetType());
        }

        private IAbstractFactory<IPerson> GetMefAbstractFactoryWithSelector()
        {
            var assemblyCatalog = new AssemblyCatalog(GetType().Assembly);
            var container = new CompositionContainer(assemblyCatalog);

            Assert.AreEqual(3, container.GetExports<IPerson>().Count());

            var list = new List<IFactory<IPerson>>();

            list.AddMefLocatorProvider<IPerson, IPerson, ICountryMetadata>(container, false,
                                                                            (a, lazies) =>
                                                                            lazies.OrderBy(l => l.Metadata.Rank).First());
            var af = list.ToAbstractFactory();
            return af;
        }
    }
}
