using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moteur.Jeu;

namespace Tests
{
    [TestClass]
    public class TestPlay
    {
        [TestMethod]
        public void TestConvertToByteArray()
        {
            Play playing = new Play();
            byte[] messageBase = playing.GetConvertToByteArray("A:35");
            Assert.AreNotEqual("A:35", messageBase);
        }

        [TestMethod]
        public void TestConvertToString()
        {
            Play playing = new Play();
            byte[] messageBase = playing.GetConvertToByteArray("A:35");
            string reconverted = playing.GetConvertToString(messageBase);
            Assert.AreEqual("A:35", reconverted);
        }
    }
}
