using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moteur.Unites;
using Moteur;

namespace Tests
{
    [TestClass]
    public class TestMap
    {
        [TestMethod]
        public void TestAddUniteToMap()
        {
            Terre uniteterre = new Terre(4, 5, true, false, false, true);
            Mer unitemer = new Mer(3, 4, true, false, false, true);
            Foret uniteforet = new Foret(1, 2, true, false, false, true);
            Moteur.Map.AddUniteToMap(uniteterre);
            Moteur.Map.AddUniteToMap(unitemer);
            Moteur.Map.AddUniteToMap(uniteforet);
            Moteur.Map.PutGraine(1,2,1);
            Assert.AreEqual(uniteterre, Moteur.Map.GetUnite(4,5));
            Assert.AreEqual(unitemer, Moteur.Map.GetUnite(3, 4));
            Assert.AreEqual(uniteforet, Moteur.Map.GetUnite(1, 2));
            Assert.AreEqual(1, Moteur.Map.GetUnite(1, 2).GetStatus());
            Assert.AreNotEqual(uniteterre, Moteur.Map.GetUnite(2, 2));
            Assert.AreNotEqual(unitemer, Moteur.Map.GetUnite(3, 3));
            Assert.AreNotEqual(uniteforet, Moteur.Map.GetUnite(2, 1));
            Assert.AreNotEqual(0, Moteur.Map.GetUnite(1, 2).GetStatus());
        }
    }
}
