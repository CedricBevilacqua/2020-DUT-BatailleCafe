using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Moteur.Initialisation
{
    public class Decode
    {
        /// <summary>
        /// Décode les informations reçues et organise les informations sur la carte.
        /// </summary>
        /// <param name="encodedMap">Trame de la carte</param>
        public static void StartDecode(byte[] encodedMap)
        {
            String mapTrame = ByteToASCII(encodedMap); //Traduction de la trame ASCII de la carte en chaine de caractères
            int[] mapTab = SeparateChaine(mapTrame); //Séparation de la chaine en entiers représentant les cases du plateau
            String[] mapTypes = ExtractType(mapTab); //Extrait les types de chaque case et soustrait la valeur qui indiquait le type
            Boolean[,] mapBorders = ExtractBorders(mapTab); //Extrait les bordures de chaque case et soustrait les valeurs qui l'indiquent
            Unites.Unite[,] unitesTab = ObjectCreatorUnite(mapTypes, mapBorders); //Création des objets Unités et rangement dans un tableau
            Char[,] parcelleLetter = DetectParcelle(unitesTab); //Détection des parcelles
            PutUnitesInMap(unitesTab, parcelleLetter); //Création des parcelles et assignation des Unités dans chaque parcelle
        }

        /// <summary>
        /// Convertir une chaine de valeurs byte en chaine de caractères ASCII.
        /// </summary>
        /// <param name="carChaine">Trame de la carte</param>
        /// <returns>Chaine de caractère de la trame de la carte</returns>
        private static String ByteToASCII(byte[] carChaine)
        {
            String convertedChaine = Encoding.ASCII.GetString(carChaine); //Conversion des byte ASCII en caractères String
            return convertedChaine;
        }

        public String GetByteToASCII(byte[] carChaine)
        {
            return ByteToASCII(carChaine);
        }

        /// <summary>
        /// Sépare chaque nombre d’une chaine de caractère séparés par le caractère « : » et les range dans un tableau d’entiers.
        /// </summary>
        /// <param name="mapTrame">Chaine de caractères de la trame de la carte</param>
        /// <returns>Tableau d'entiers des valeurs de chaque unité</returns>
        private static int[] SeparateChaine(String mapTrame)
        {
            int[] mapTab = new int[100]; //On sait que le plateau de jeu fait 100 cases
            int indexTrame = 0; //Permet d'avancer de parcourir la trame

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

        public int[] GetSeparateChaine(String mapTrame)
        {
            return SeparateChaine(mapTrame);
        }

        /// <summary>
        /// Détermine le type de chaque unité à partir des valeurs de chaque unité contenu dans un tableau.
        /// </summary>
        /// <param name="mapTab">Tableau de valeur de chaque unité de la carte</param>
        /// <returns>Type de chaque unité de la carte</returns>
        private static String[] ExtractType(int[] mapTab)
        {
            //Codage : 64 = Mer ; 32 = Forêt ; 0 = Terre

            String[] mapTypes = new String[100];

            for (int indexCase = 0; indexCase < 100; indexCase++)
            {
                if ((mapTab[indexCase] - 64) <= 15 && (mapTab[indexCase] - 64) >= 0) //Bordures + Mer
                {
                    mapTypes[indexCase] = "Mer";
                    mapTab[indexCase] = mapTab[indexCase] - 64; //On retire la valeur qui indiquait le type
                }
                else if ((mapTab[indexCase] - 32) <= 15 && (mapTab[indexCase] - 32) >= 0) //Bordure + Forêt
                {
                    mapTypes[indexCase] = "Forêt";
                    mapTab[indexCase] = mapTab[indexCase] - 32; //On retire la valeur qui indiquait le type
                }
                else //Bordure uniquement signifie aucune particularité de type on définit Terre par défaut
                {
                    mapTypes[indexCase] = "Terre";
                }
            }

            return mapTypes;
        }

        public String[] GetExtractType(int[] mapTab)
        {
            return ExtractType(mapTab);
        }

        /// <summary>
        /// Détermine les bordures de chaque unité à partir des valeurs de chaque unité contenu dans un tableau une fois les valeurs des types soustraits.
        /// </summary>
        /// <param name="mapTab">Tableau de valeur de chaque unité de la carte</param>
        /// <returns>Etat des bordures de chaque unité de la carte</returns>
        private static Boolean[,] ExtractBorders(int[] mapTab)
        {
            //Codage : 8 = Est ; 4 = Sud ; 2 = Ouest ; 1 = Nord

            Boolean[,] mapBorders = new Boolean[100, 4]; //Index de la deuxième dimension dans l'ordre Nord, Sud, Est, Ouest

            for (int indexCase = 0; indexCase < 100; indexCase++)
            {
                if ((mapTab[indexCase] - 8) >= 0) //Test de la frontière Est
                {
                    mapTab[indexCase] = mapTab[indexCase] - 8; //Retrait de la valeur de la frontière à la case
                    mapBorders[indexCase, 2] = true; //Présence d'une frontière
                }

                if ((mapTab[indexCase] - 4) >= 0) //Test de la frontière Sud
                {
                    mapTab[indexCase] = mapTab[indexCase] - 4; //Retrait de la valeur de la frontière à la case
                    mapBorders[indexCase, 1] = true; //Présence d'une frontière
                }

                if ((mapTab[indexCase] - 2) >= 0) //Test de la frontière Ouest
                {
                    mapTab[indexCase] = mapTab[indexCase] - 2; //Retrait de la valeur de la frontière à la case
                    mapBorders[indexCase, 3] = true; //Présence d'une frontière
                }

                if ((mapTab[indexCase] - 1) >= 0) //Test de la frontière Nord
                {
                    mapTab[indexCase] = mapTab[indexCase] - 4; //Retrait de la valeur de la frontière à la case
                    mapBorders[indexCase, 0] = true; //Présence d'une frontière
                }
            }

            return mapBorders;
        }

        public Boolean[,] GetExtractBorders(int[] mapTab)
        {
            return ExtractBorders(mapTab);
        }

        /// <summary>
        /// Instanciation des objets Unite en fonction de leur type et rangement dans un tableau d’objets à partir du tableau de types de de bordures de chaque unité.
        /// </summary>
        /// <param name="mapTypes">Chaine de caractères de la trame de la carte</param>
        /// <param name="mapBorders">Tableau de l'état des bordures de chaque unité de la carte</param>
        /// <returns></returns>
        private static Unites.Unite[,] ObjectCreatorUnite(String[] mapTypes, Boolean[,] mapBorders)
        {
            Unites.Unite[,] unitesTab = new Unites.Unite[10, 10];

            for (int indexCase = 0; indexCase < 100; indexCase++)
            {
                int posX = indexCase % 10;
                int posY = indexCase / 10;
                Unites.Unite unite;

                if (mapTypes[indexCase] == "Mer")
                {
                    unite = new Unites.Mer(posX, posY, mapBorders[indexCase, 0], mapBorders[indexCase, 1], mapBorders[indexCase, 2], mapBorders[indexCase, 3]);
                }
                else if (mapTypes[indexCase] == "Forêt")
                {
                    unite = new Unites.Foret(posX, posY, mapBorders[indexCase, 0], mapBorders[indexCase, 1], mapBorders[indexCase, 2], mapBorders[indexCase, 3]);
                }
                else
                {
                    unite = new Unites.Terre(posX, posY, mapBorders[indexCase, 0], mapBorders[indexCase, 1], mapBorders[indexCase, 2], mapBorders[indexCase, 3]);
                }

                unitesTab[posX, posY] = unite;
            }

            return unitesTab;
        }

        public Unites.Unite[,] GetObjectCreatorUnite(String[] mapTypes, Boolean[,] mapBorders)
        {
            return ObjectCreatorUnite(mapTypes, mapBorders);
        }

        /// <summary>
        /// Détermination de la parcelle à laquelle appartient chaque unité.
        /// </summary>
        /// <param name="unitesTab">Tableau à deux dimension des objets unité de la carte</param>
        /// <returns>Tableau de caractères à deux dimensions indiquant la parcelle de chaque unité</returns>
        private static Char[,] DetectParcelle(Unites.Unite[,] unitesTab)
        {
            Char[,] parcelleLetter = new Char[10, 10];
            Char actualLetterForParcelle = 'A';

            for(int boucleParcelle = 0; boucleParcelle < 17; boucleParcelle++)
            {
                SearchNonAttributedEarthUnite(parcelleLetter, actualLetterForParcelle, unitesTab);
                for (int boucleDetection = 0; boucleDetection < 6; boucleDetection++)
                {
                    for (int boucleY = 0; boucleY < 10; boucleY++)
                    {
                        for (int boucleX = 0; boucleX < 10; boucleX++)
                        {
                            if (unitesTab[boucleX, boucleY].GetType().Name == "Terre")
                            {
                                UpdateWithNearUnites(unitesTab, parcelleLetter, boucleX, boucleY);
                            }
                        }
                    }
                }
                actualLetterForParcelle++;
            }

            return parcelleLetter;
        }

        public Char[,] GetDetectParcelle(Unites.Unite[,] unitesTab)
        {
            return DetectParcelle(unitesTab);
        }

        /// <summary>
        /// Recherche une unité de type Terre qui n’a pas encore de parcelle attribuée et lui attribue la lettre de parcelle suivante.
        /// </summary>
        /// <param name="parcelleLetter">Tableau de caractères à deux dimensions indiquant la parcelle de chaque unité</param>
        /// <param name="actualLetterForParcelle">Letter de la parcelle actuelle en cours de détermination</param>
        /// <param name="unitesTab">Tableau d'objets unité pour chaque toutes les unités de la carte</param>
        private static void SearchNonAttributedEarthUnite(Char[,] parcelleLetter, Char actualLetterForParcelle, Unites.Unite[,] unitesTab)
        {
            int boucleLn = 0, boucleCol = 0;
            while(boucleLn < 10)
            {
                while(boucleCol < 10)
                {
                    if (parcelleLetter[boucleLn, boucleCol] == 0 && unitesTab[boucleLn, boucleCol].GetType().Name == "Terre")
                    {
                        parcelleLetter[boucleLn, boucleCol] = actualLetterForParcelle;
                        boucleCol = 10;
                        boucleLn = 10;
                    }
                    boucleCol++;
                }
                boucleLn++;
                boucleCol = 0;
            }
        }

        public void GetSearchNonAttributedEarthUnite(Char[,] parcelleLetter, Char actualLetterForParcelle, Unites.Unite[,] unitesTab)
        {
           SearchNonAttributedEarthUnite(parcelleLetter, actualLetterForParcelle, unitesTab);
        }

        /// <summary>
        /// Assigne à l’unité en question la lettre de parcelle de l’unité adjacente qui n’est pas séparée par une bordure.
        /// </summary>
        /// <param name="unitesTab">Tableau des objets unité pour toutes les unités de la carte</param>
        /// <param name="parcelleLetter">Tableau de caractères indiquant la parcelle de chaque unité</param>
        /// <param name="boucleX">Coordonnée X de l'unité en cours d'analyse</param>
        /// <param name="boucleY">Coordonnée Y de l'unité en cours d'analyse</param>
        private static void UpdateWithNearUnites(Unites.Unite[,] unitesTab, Char[,] parcelleLetter, int boucleX, int boucleY)
        {
            if (unitesTab[boucleX, boucleY].GetborderUp() == false)
            {
                if (parcelleLetter[boucleX, boucleY - 1] != 0)
                {
                    parcelleLetter[boucleX, boucleY] = parcelleLetter[boucleX, boucleY - 1];
                }
            }
            if (unitesTab[boucleX, boucleY].GetborderDown() == false)
            {
                if (parcelleLetter[boucleX, boucleY + 1] != 0)
                {
                    parcelleLetter[boucleX, boucleY] = parcelleLetter[boucleX, boucleY + 1];
                }
            }
            if (unitesTab[boucleX, boucleY].GetborderLeft() == false)
            {
                if (parcelleLetter[boucleX - 1, boucleY] != 0)
                {
                    parcelleLetter[boucleX, boucleY] = parcelleLetter[boucleX - 1, boucleY];
                }
            }
            if (unitesTab[boucleX, boucleY].GetborderRight() == false)
            {
                if (parcelleLetter[boucleX + 1, boucleY] != 0)
                {
                    parcelleLetter[boucleX, boucleY] = parcelleLetter[boucleX + 1, boucleY];
                }
            }
        }

        public void GetUpdateWithNearUnites(Unites.Unite[,] unitesTab, Char[,] parcelleLetter, int boucleX, int boucleY)
        {
            UpdateWithNearUnites(unitesTab, parcelleLetter, boucleX, boucleY);
        }

        /// <summary>
        /// Assignation de sa parcelle à chaque Unité et ajout de celles-ci à l’objet Map qui contient toutes les Unités et les Parcelle.
        /// </summary>
        /// <param name="unitesTab">Tableau des objets unité de toute la carte</param>
        /// <param name="parcelleLetter">Tableau à deux dimensions de caractère indiquant la parcelle de chaque unité de la carte</param>
        private static void PutUnitesInMap(Unites.Unite[,] unitesTab, Char[,] parcelleLetter)
        {
            for(int boucleLn = 0; boucleLn < 10; boucleLn++)
            {
                for(int boucleCol = 0; boucleCol < 10; boucleCol++)
                {
                    unitesTab[boucleLn, boucleCol].SetparcelleLetter(parcelleLetter[boucleLn, boucleCol]);
                    Map.AddUniteToMap(unitesTab[boucleLn, boucleCol]);
                }
            }
        }

        public void GetPutUnitesInMap(Unites.Unite[,] unitesTab, Char[,] parcelleLetter)
        {
            PutUnitesInMap(unitesTab, parcelleLetter);
        }
    }
}
