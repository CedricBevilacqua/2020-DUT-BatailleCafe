using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Sockets;


using Moteur.Initialisation;
using Moteur.Unites;

namespace Tests
{
    [TestClass]
    public class TestDecode
    {
        public int[] Createmap()
        {
            int[] Testmap = new int[100];
            Testmap[0] = 3;
            Testmap[1] = 9;
            Testmap[2] = 71;
            Testmap[3] = 69;
            Testmap[4] = 65;
            Testmap[5] = 65;
            Testmap[6] = 65;
            Testmap[7] = 65;
            Testmap[8] = 65;
            Testmap[9] = 73;
            Testmap[10] = 2;
            Testmap[11] = 8;
            Testmap[12] = 3;
            Testmap[13] = 9;
            Testmap[14] = 70;
            Testmap[15] = 68;
            Testmap[16] = 64;
            Testmap[17] = 64;
            Testmap[18] = 64;
            Testmap[19] = 72;
            Testmap[20] = 6;
            Testmap[21] = 12;
            Testmap[22] = 2;
            Testmap[23] = 8;
            Testmap[24] = 3;
            Testmap[25] = 9;
            Testmap[26] = 70;
            Testmap[27] = 68;
            Testmap[28] = 64;
            Testmap[29] = 72;
            Testmap[30] = 11;
            Testmap[31] = 11;
            Testmap[32] = 6;
            Testmap[33] = 12;
            Testmap[34] = 6;
            Testmap[35] = 12;
            Testmap[36] = 3;
            Testmap[37] = 9;
            Testmap[38] = 70;
            Testmap[39] = 76;
            Testmap[40] = 10;
            Testmap[41] = 10;
            Testmap[42] = 11;
            Testmap[43] = 11;
            Testmap[44] = 67;
            Testmap[45] = 73;
            Testmap[46] = 6;
            Testmap[47] = 12;
            Testmap[48] = 3;
            Testmap[49] = 9;
            Testmap[50] = 14;
            Testmap[51] = 14;
            Testmap[52] = 10;
            Testmap[53] = 10;
            Testmap[54] = 70;
            Testmap[55] = 76;
            Testmap[56] = 7;
            Testmap[57] = 13;
            Testmap[58] = 6;
            Testmap[59] = 12;
            Testmap[60] = 3;
            Testmap[61] = 9;
            Testmap[62] = 14;
            Testmap[63] = 14;
            Testmap[64] = 11;
            Testmap[65] = 7;
            Testmap[66] = 13;
            Testmap[67] = 3;
            Testmap[68] = 9;
            Testmap[69] = 75;
            Testmap[70] = 2;
            Testmap[71] = 8;
            Testmap[72] = 7;
            Testmap[73] = 13;
            Testmap[74] = 14;
            Testmap[75] = 3;
            Testmap[76] = 9;
            Testmap[77] = 6;
            Testmap[78] = 12;
            Testmap[79] = 78;
            Testmap[80] = 6;
            Testmap[81] = 12;
            Testmap[82] = 3;
            Testmap[83] = 1;
            Testmap[84] = 9;
            Testmap[85] = 6;
            Testmap[86] = 12;
            Testmap[87] = 35;
            Testmap[88] = 33;
            Testmap[89] = 41;
            Testmap[90] = 71;
            Testmap[91] = 77;
            Testmap[92] = 6;
            Testmap[93] = 4;
            Testmap[94] = 12;
            Testmap[95] = 39;
            Testmap[96] = 37;
            Testmap[97] = 36;
            Testmap[98] = 36;
            Testmap[99] = 44;


            return Testmap;
        }

        [TestMethod]
        public void TestByteToASCII()
        {
            Decode decode = new Decode();

            byte[] Test = { 4, 10, 5, 12 };
            String chaineconvertie = decode.GetByteToASCII(Test);
            Assert.AreEqual(chaineconvertie.GetType(), typeof(String));
            Assert.AreNotEqual(chaineconvertie, null);

        }

        [TestMethod]
        public void TestSeparateChaine()
        {
            Decode decode = new Decode();
            string Testtrame = "3:9:71:69:65:65:65:65:65:73|2:8:3:9:70:68:64:64:64:72|" +
                "6:12:2:8:3:9:70:68:64:72|11:11:6:12:6:12:3:9:70:76|10:10:11:11:67:73:6:12:3:9|" +
                "14:14:10:10:70:76:7:13:6:12|3:9:14:14:11:7:13:3:9:75|2:8:7:13:14:3:9:6:12:78" +
                "|6:12:3:1:9:6:12:35:33:41|71:77:6:4:12:39:37:36:36:44|"+"3:9:71:69:65:65:65:65:65:73|2:8:3:9:70:68:64:64:64:72|" +
                "6:12:2:8:3:9:70:68:64:72|11:11:6:12:6:12:3:9:70:76|10:10:11:11:67:73:6:12:3:9|" +
                "14:14:10:10:70:76:7:13:6:12|3:9:14:14:11:7:13:3:9:75|2:8:7:13:14:3:9:6:12:78" +
                "|6:12:3:1:9:6:12:35:33:41|71:77:6:4:12:39:37:36:36:44|";

            int[] Testmap = Createmap();
            int[] Typetab = decode.GetSeparateChaine(Testtrame);
            for (int index = 0; index < 100; index++)
                Assert.AreEqual(Testmap[index], Typetab[index]);
            
        }

        [TestMethod]
        public void TestExtractType()
        {
            Decode decode = new Decode();
            int[] Testmap = Createmap();
            string[] Typetab = decode.GetExtractType(Testmap);
            Assert.AreEqual(Typetab[0], "Terre");
            Assert.AreEqual(Typetab[99], "Forêt");
            Assert.AreEqual(Typetab[2], "Mer");


        }
        [TestMethod]
        public void TestExtractBorders()
        {
            Decode decode = new Decode();
            int[] Testmap = Createmap();
            Boolean[,] Booltab = decode.GetExtractBorders(Testmap);
            Assert.AreEqual(Booltab[0,3], true);
            Assert.AreEqual(Booltab[99,2], true);
            Assert.AreEqual(Booltab[2,1], true);
        }

        [TestMethod]
        public void TestObjectCreatorUnite()
        {
            Decode decode = new Decode();
            int[] Testmap = Createmap();
            string[] Typetab = decode.GetExtractType(Testmap);
            Boolean[,] Booltab = decode.GetExtractBorders(Testmap);
            Moteur.Unites.Unite[,] unitetab = decode.GetObjectCreatorUnite(Typetab, Booltab);
            Assert.AreEqual(unitetab[0,0].GetType(), typeof(Terre));
            Assert.AreEqual(unitetab[9,9].GetType(), typeof(Foret));
            Assert.AreEqual(unitetab[2,0].GetType(), typeof(Mer));
        }

        [TestMethod]
        public void TestDetectParcelle()
        {
            Decode decode = new Decode();
            int[] Testmap = Createmap();
            string[] Typetab = decode.GetExtractType(Testmap);
            Boolean[,] Booltab = decode.GetExtractBorders(Testmap);
            Moteur.Unites.Unite[,] unitetab = decode.GetObjectCreatorUnite(Typetab, Booltab);
            Char[,] Chartab = decode.GetDetectParcelle(unitetab);
            Assert.AreEqual(Chartab[0,0], 'A');
            Assert.AreEqual(Chartab[9,9], '\0');
            Assert.AreEqual(Chartab[2,2], 'E');

        }

        [TestMethod]
        public void TestPutUnitesInMap()
        {
            Decode decode = new Decode();
            int[] Testmap = Createmap();
            string[] Typetab = decode.GetExtractType(Testmap);
            Boolean[,] Booltab = decode.GetExtractBorders(Testmap);
            Moteur.Unites.Unite[,] unitetab = decode.GetObjectCreatorUnite(Typetab, Booltab);
            Char[,] Chartab = decode.GetDetectParcelle(unitetab);
            decode.GetPutUnitesInMap(unitetab, Chartab);
            Assert.AreEqual('A', Moteur.Map.GetUnite(0, 0).Getparcelleletter());
            Assert.AreEqual('\0', Moteur.Map.GetUnite(9, 9).Getparcelleletter());
            Assert.AreEqual('E', Moteur.Map.GetUnite(2, 2).Getparcelleletter());


        }
    }
}
