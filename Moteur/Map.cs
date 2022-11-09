using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur
{
    public static class Map
    {
        private static Unites.Unite[,] unitesTab = new Unites.Unite[10, 10];
        private static Parcelle[] parcelleTab = new Parcelle[17];

        static Map()
        {
            for (int parcelleCompteur = 0; parcelleCompteur < 17; parcelleCompteur++)
            {
                parcelleTab[parcelleCompteur] = new Parcelle();
            }
        }

        public static void AddUniteToMap(Unites.Unite newUnite)
        {
            unitesTab[newUnite.GetposX(), newUnite.GetposY()] = newUnite;
            if (newUnite.Getparcelleletter() != 0)
            {
                parcelleTab[newUnite.Getparcelleletter() - 65].addUnite(newUnite);
            }
        }

        public static void PutGraine(int posX, int posY, int status)
        {
            unitesTab[posX, posY].SetStatus(status);
        }

        public static Unites.Unite GetUnite(int posX, int posY)
        {
            Unites.Unite gettedUnite = unitesTab[posX, posY];
            return gettedUnite;
        }

        public static List<Parcelle> GetParcelles()
        {
            List<Parcelle> parcelleList = parcelleTab.ToList();
            return parcelleList;
        }

    }
}
