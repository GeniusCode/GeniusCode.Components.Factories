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

        public class CountryMetadata : ICountryMetadata
        {
            #region Implementation of ICountryMetadata

            public string Country { get; set; }

            public int Rank { get; set; }

            #endregion
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

            var af = GetSimpleMefAbstractFactory(true);

            var md = new CountryMetadata { Country = "USA" };

            var a1 = af.GetInstance(md);
            var a2 = af.GetInstance(md);

            Assert.AreSame(a1, a2);
        }

        [TestMethod]
        public void ShouldGetUniqueInstance()
        {

            var af = GetSimpleMefAbstractFactory(false);

            var md = new CountryMetadata {Country = "USA"};

            var a1 = af.GetInstance(md);
            var a2 = af.GetInstance(md);

            Assert.AreNotSame(a1, a2);
        }



        private IAbstractFactory<IPerson> GetSimpleMefAbstractFactory(bool reuseInstances)
        {
            var assemblyCatalog = new AssemblyCatalog(GetType().Assembly);
            var container = new CompositionContainer(assemblyCatalog);

            var list = new List<IFactory<IPerson>>();

            list.AddMefLocatorProvider<IPerson, IPerson, ICountryMetadata, ICountryMetadata>(container, reuseInstances, null,
                (a, m) => m.Metadata.Country == a.Country);
            var af = list.ToAbstractFactory();
            return af;
        }


        [TestMethod]
        public void Should_use_selector_to_pick_from_multiple_exports()
        {
            var hi = GetMefAbstractFactoryWithSelector();

            var a1 = hi.GetInstance(null);

            Assert.IsNotNull(a1);
            Assert.AreEqual(typeof(Mexican), a1.GetType());
        }

        private IAbstractFactory<IPerson> GetMefAbstractFactoryWithSelector()
        {
            var assemblyCatalog = new AssemblyCatalog(GetType().Assembly);
            var container = new CompositionContainer(assemblyCatalog);

            Assert.AreEqual(3, container.GetExports<IPerson>().Count());

            var list = new List<IFactory<IPerson>>();

            list.AddMefLocatorProvider<IPerson, IPerson, ICountryMetadata>(container, false,
                                                                            (a, lazies) =>
                                                                            lazies.OrderBy(l => l.Metadata.Rank).Last());
            var af = list.ToAbstractFactory();
            return af;
        }
    }
}
