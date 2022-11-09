using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moteur.Unites;
using Moteur;

namespace Tests
{
    [TestClass]
    public class TestParcelle
    {
        [TestMethod]
        public void TestAddUnite()
        {
            Terre uniteterre = new Terre(4, 5, true, false, false, true);
            Mer unitemer = new Mer(4, 5, true, false, false, true);
            Foret uniteforet = new Foret(4, 5, true, false, false, true);
            Parcelle parcelle = new Parcelle();
            Assert.AreEqual(0, parcelle.GetAssignedUnites().Count);
            parcelle.addUnite(uniteterre);
            parcelle.addUnite(unitemer);
            parcelle.addUnite(uniteforet);
            Assert.AreEqual(3, parcelle.GetAssignedUnites().Count);
            Assert.AreEqual(uniteterre, parcelle.GetAssignedUnites()[0]);
            Assert.AreEqual(unitemer, parcelle.GetAssignedUnites()[1]);
            Assert.AreEqual(uniteforet, parcelle.GetAssignedUnites()[2]);
        }
    }
}
