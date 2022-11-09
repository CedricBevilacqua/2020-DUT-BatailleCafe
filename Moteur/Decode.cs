using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur
{
    public class Decode
    {
        public static void decode(byte[] encodedMap)
        {
            String mapTrame = byteToASCII(encodedMap); //Traduction de la trame ASCII de la carte en chaine de caractères
            int[] mapTab = separateChaine(mapTrame); //Séparation de la chaine en entiers représentant les cases du plateau
        }

        private static String byteToASCII(byte[] carChaine)
        {
            String convertedChaine = Encoding.ASCII.GetString(carChaine); //Conversion des byte ASCII en caractères String
            return convertedChaine;
        }

        private static int[] separateChaine(String mapTrame)
        {
            int[] mapTab = new int[100]; //On sait que le plateau de jeu fait 100 cases
            int indexTrame = 0; //Permet d'avancer de parcourur la trame

            for (int nbCase = 0; nbCase < mapTab.Length; nbCase++)
            {
                String chaineToParse = String.Concat(mapTrame[indexTrame], mapTrame[indexTrame + 1]);
                if (int.TryParse(chaineToParse, out mapTab[nbCase]))
                {
                    indexTrame = indexTrame + 3;
                }
                else
                {
                    mapTab[nbCase] = int.Parse(mapTrame[indexTrame].ToString());
                    indexTrame = indexTrame + 2;
                }

                if (mapTrame[indexTrame] == '|')
                {
                    indexTrame++;
                }
            }

            return mapTab;
        }
    }
}
