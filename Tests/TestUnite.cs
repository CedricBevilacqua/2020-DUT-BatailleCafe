using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moteur.Unites;

namespace Tests
{
    [TestClass]
    public class TestUnite
    {
        [TestMethod]
        public void TestTerre()
        {
            Terre unite = new Terre(4, 5, true, false, false, true);
            Assert.AreEqual(unite.GetposX(), 4);
            Assert.AreNotEqual(unite.GetposX(), 6);
            Assert.AreEqual(unite.GetposY(), 5);
            Assert.AreNotEqual(unite.GetposY(), 12);
            Assert.AreEqual(unite.GetborderUp(), true);
            Assert.AreNotEqual(unite.GetborderUp(), false);
            Assert.AreEqual(unite.GetborderDown(), false);
            Assert.AreNotEqual(unite.GetborderDown(), true);
            Assert.AreEqual(unite.GetborderRight(), false);
            Assert.AreNotEqual(unite.GetborderRight(), true);
            Assert.AreEqual(unite.GetborderLeft(), true);
            Assert.AreNotEqual(unite.GetborderLeft(), false);
            unite.SetparcelleLetter('t');
            Assert.AreEqual(unite.Getparcelleletter(), 't');
            unite.SetborderUp(false);
            unite.SetborderDown(true);
            unite.SetborderRight(true);
            unite.SetborderLeft(false);
            Assert.AreEqual(unite.GetborderUp(), false);
            Assert.AreNotEqual(unite.GetborderUp(), true);
            Assert.AreEqual(unite.GetborderDown(), true);
            Assert.AreNotEqual(unite.GetborderDown(), false);
            Assert.AreEqual(unite.GetborderRight(), true);
            Assert.AreNotEqual(unite.GetborderRight(), false);
            Assert.AreEqual(unite.GetborderLeft(), false);
            Assert.AreNotEqual(unite.GetborderLeft(), true);
            Assert.AreEqual(unite.GetStatus(), 0);
            unite.SetStatus(1);
            Assert.AreNotEqual(unite.GetStatus(), 0);
            Assert.AreEqual(unite.GetStatus(), 1);

        }

        [TestMethod]
        public void TestMer()
        {
            Mer unite = new Mer(4, 5, true, false, false, true);
            Assert.AreEqual(unite.GetposX(), 4);
            Assert.AreNotEqual(unite.GetposX(), 6);
            Assert.AreEqual(unite.GetposY(), 5);
            Assert.AreNotEqual(unite.GetposY(), 12);
            Assert.AreEqual(unite.GetborderUp(), true);
            Assert.AreNotEqual(unite.GetborderUp(), false);
            Assert.AreEqual(unite.GetborderDown(), false);
            Assert.AreNotEqual(unite.GetborderDown(), true);
            Assert.AreEqual(unite.GetborderRight(), false);
            Assert.AreNotEqual(unite.GetborderRight(), true);
            Assert.AreEqual(unite.GetborderLeft(), true);
            Assert.AreNotEqual(unite.GetborderLeft(), false);
            unite.SetparcelleLetter('m');
            Assert.AreEqual(unite.Getparcelleletter(), 'm');
            unite.SetborderUp(false);
            unite.SetborderDown(true);
            unite.SetborderRight(true);
            unite.SetborderLeft(false);
            Assert.AreEqual(unite.GetborderUp(), false);
            Assert.AreNotEqual(unite.GetborderUp(), true);
            Assert.AreEqual(unite.GetborderDown(), true);
            Assert.AreNotEqual(unite.GetborderDown(), false);
            Assert.AreEqual(unite.GetborderRight(), true);
            Assert.AreNotEqual(unite.GetborderRight(), false);
            Assert.AreEqual(unite.GetborderLeft(), false);
            Assert.AreNotEqual(unite.GetborderLeft(), true);
            Assert.AreEqual(unite.GetStatus(), 0);
            unite.SetStatus(1);
            Assert.AreNotEqual(unite.GetStatus(), 0);
            Assert.AreEqual(unite.GetStatus(), 1);
        }

        [TestMethod]
        public void TestForet()
        {
            Foret unite = new Foret(4, 5, true, false, false, true);
            Assert.AreEqual(unite.GetposX(), 4);
            Assert.AreNotEqual(unite.GetposX(), 6);
            Assert.AreEqual(unite.GetposY(), 5);
            Assert.AreNotEqual(unite.GetposY(), 12);
            Assert.AreEqual(unite.GetborderUp(), true);
            Assert.AreNotEqual(unite.GetborderUp(), false);
            Assert.AreEqual(unite.GetborderDown(), false);
            Assert.AreNotEqual(unite.GetborderDown(), true);
            Assert.AreEqual(unite.GetborderRight(), false);
            Assert.AreNotEqual(unite.GetborderRight(), true);
            Assert.AreEqual(unite.GetborderLeft(), true);
            Assert.AreNotEqual(unite.GetborderLeft(), false);
            unite.SetparcelleLetter('f');
            Assert.AreEqual(unite.Getparcelleletter(), 'f');
            unite.SetborderUp(false);
            unite.SetborderDown(true);
            unite.SetborderRight(true);
            unite.SetborderLeft(false);
            Assert.AreEqual(unite.GetborderUp(), false);
            Assert.AreNotEqual(unite.GetborderUp(), true);
            Assert.AreEqual(unite.GetborderDown(), true);
            Assert.AreNotEqual(unite.GetborderDown(), false);
            Assert.AreEqual(unite.GetborderRight(), true);
            Assert.AreNotEqual(unite.GetborderRight(), false);
            Assert.AreEqual(unite.GetborderLeft(), false);
            Assert.AreNotEqual(unite.GetborderLeft(), true);
            Assert.AreEqual(unite.GetStatus(), 0);
            unite.SetStatus(1);
            Assert.AreNotEqual(unite.GetStatus(), 0);
            Assert.AreEqual(unite.GetStatus(), 1);

        }
    }
}
