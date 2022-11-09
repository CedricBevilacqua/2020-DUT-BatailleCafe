using Moteur.Unites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Moteur.Jeu
{
    public static class IA
    {
        public static String Play(int lastPosX, int lastPosY)
        {
            String answerPos = "";
            List<Parcelle> zoneRecherche;
            List<Parcelle> zoneRechercheBASE;

            ///REGLE PAR DEFAUT : Tenter de poser une graine sur une parcelle de 6
            zoneRecherche = detectPossiblePlaysOnParcelles(lastPosX, lastPosY);
            zoneRecherche = filterByNbUnites(6, zoneRecherche);
            validCoordonees(zoneRecherche, lastPosX, lastPosY, ref answerPos);

            ///REGLE PAR DEFAUT : Tenter de poser une graine sur une parcelle de 4
            zoneRecherche = detectPossiblePlaysOnParcelles(lastPosX, lastPosY);
            zoneRecherche = filterByNbUnites(4, zoneRecherche);
            validCoordonees(zoneRecherche, lastPosX, lastPosY, ref answerPos);

            ///REGLE PAR DEFAUT : Tenter de poser une graine sur une parcelle de 3
            zoneRecherche = detectPossiblePlaysOnParcelles(lastPosX, lastPosY);
            zoneRecherche = filterByNbUnites(3, zoneRecherche);
            validCoordonees(zoneRecherche, lastPosX, lastPosY, ref answerPos);

            ///REGLE PAR DEFAUT : Tenter de poser une graine sur une parcelle de 2
            zoneRecherche = detectPossiblePlaysOnParcelles(lastPosX, lastPosY);
            zoneRecherche = filterByNbUnites(2, zoneRecherche);
            validCoordonees(zoneRecherche, lastPosX, lastPosY, ref answerPos);

            ///REGLE D'EXPENSION : Si une parcelle de 4 unités est vide et n'a pas de graine ennemie
            zoneRecherche = detectPossiblePlaysOnParcelles(lastPosX, lastPosY);
            zoneRecherche = rechercheParcelle(4, 0, 4, zoneRecherche);
            validCoordonees(zoneRecherche, lastPosX, lastPosY, ref answerPos);

            ///REGLE D'EXPENSION : Si une parcelle de 2 unités est vide ou pas entièrement semée par des graines alliées ALORS poser une graine sur une case de cette parcelle
            zoneRecherche = detectPossiblePlaysOnParcelles(lastPosX, lastPosY);
            zoneRechercheBASE = zoneRecherche;
            zoneRecherche = rechercheParcelle(2, 0, 2, zoneRecherche); //Recherche parcelles de 2 unités totalement vides
            zoneRecherche.AddRange(rechercheParcelle(2, 1, 1, zoneRechercheBASE)); //Recherche parcelles de 2 unités avec 1 graine alliée
            validCoordonees(zoneRecherche, lastPosX, lastPosY, ref answerPos);

            ///REGLE D'EXPENSION : Si une parcelle de 3 unités a 0 ou 1 seule graine alliée et n'est p as pleine et n'a pas de graine ennemie ALORS poser une graine supplémentaire sur cette parcelle
            zoneRecherche = detectPossiblePlaysOnParcelles(lastPosX, lastPosY);
            zoneRechercheBASE = zoneRecherche;
            zoneRecherche = rechercheParcelle(3, 1, 0, zoneRechercheBASE); //Recherche parcelles de 3 unités avec aucune graine alliée
            zoneRecherche.AddRange(rechercheParcelle(3, 1, 1, zoneRechercheBASE)); //Recherche parcelles de 3 unités avec 1 graine alliée
            zoneRecherche = rechercheParcelle(3, 2, 0, zoneRecherche); //Filtre ou on garde parcelles de 3 unités avec aucune graine ennemie
            validCoordonees(zoneRecherche, lastPosX, lastPosY, ref answerPos);

            ///REGLE OFFENSIVE : Si l'ennemi a posé une seule graine sur une parcelle de 2 unités ou aucune graine alliée n'est présente
            zoneRecherche = detectPossiblePlaysOnParcelles(lastPosX, lastPosY);
            zoneRecherche = rechercheParcelle(2, 2, 1, zoneRecherche); //Recherche parcelle 2 unités avec 1 graine ennemie
            zoneRecherche = rechercheParcelle(2, 1, 0, zoneRecherche); //Recherche parcelle 2 unités avec 0 graine alliée
            validCoordonees(zoneRecherche, lastPosX, lastPosY, ref answerPos);

            ///REGLE OFFENSIVE : Si l'ennemi a posé  une graine sur une parcelle de 3 unités sur laquelle on possède déjà une graine
            zoneRecherche = detectPossiblePlaysOnParcelles(lastPosX, lastPosY);
            zoneRecherche = rechercheParcelle(3, 1, 1, zoneRecherche); //Recherche parcelle 3 unités ou on possède une graine
            zoneRecherche = rechercheParcelle(3, 2, 1, zoneRecherche); //Recherche parcelle 3 unités ou l'ennemi possède une graine
            zoneRecherche = rechercheParcelle(3, 0, 1, zoneRecherche); //Recherche parcelle 3 unités ou une unité est vide
            validCoordonees(zoneRecherche, lastPosX, lastPosY, ref answerPos);

            ///REGLE OFFENSIVE : Si une parcelle de 4 unités possède déjà une graine alliée et dispose davantage de graines ennemies
            zoneRecherche = detectPossiblePlaysOnParcelles(lastPosX, lastPosY);
            zoneRecherche = rechercheParcelle(4, 1, 1, zoneRecherche); //Recherche parcelle 4 unités avec 1 graine alliée
            zoneRecherche = rechercheParcelle(4, 2, 2, zoneRecherche); //Recherche parcelle 4 unités avec 2 graines ennemies
            validCoordonees(zoneRecherche, lastPosX, lastPosY, ref answerPos);

            String answerPosInverse = String.Concat(answerPos[1], answerPos[0]);
            return "A:" + answerPosInverse;
        }

        private static List<Parcelle> detectPossiblePlaysOnParcelles(int lastPosX, int lastPosY)
        {
            List<Parcelle> parcellesSelectionnees = new List<Parcelle>();

            foreach(Parcelle parcelleEtudiee in Map.GetParcelles())
            {
                foreach(Unite uniteEtudiee in parcelleEtudiee.GetAssignedUnites())
                {
                    if((uniteEtudiee.GetposX() == lastPosX || uniteEtudiee.GetposY() == lastPosY) && uniteEtudiee.GetStatus() == 0 && uniteEtudiee.Getparcelleletter() != Map.GetUnite(lastPosX, lastPosY).Getparcelleletter())
                    {
                        parcellesSelectionnees.Add(parcelleEtudiee);
                        break;
                    }
                }
            }

            return parcellesSelectionnees;
        }

        private static List<Parcelle> filterByNbUnites(int tailleParcelle, List<Parcelle> zoneRecherche)
        {
            List<Parcelle> resultats = new List<Parcelle>();
            foreach(Parcelle parcelleEtudiee in zoneRecherche)
            {
                if(parcelleEtudiee.GetAssignedUnites().Count == tailleParcelle) { resultats.Add(parcelleEtudiee); }
            }
            return resultats;
        }

        private static List<Parcelle> rechercheParcelle(int tailleParcelle, int typeGraines, int nbGraines, List<Parcelle> zoneRecherche) //Types de graines : 0:Vide ; 1:Allié ; 2:Ennemi
        {
            List<Parcelle> resultats = new List<Parcelle>();
            foreach(Parcelle parcelleEtudiee in  zoneRecherche)
            {
                if (parcelleEtudiee.GetAssignedUnites().Count() == tailleParcelle)
                {
                    int compteurGraine = 0;
                    foreach(Unite uniteEtudiee in parcelleEtudiee.GetAssignedUnites())
                    {
                        if (uniteEtudiee.GetStatus() == typeGraines) { compteurGraine++; }
                    }
                    if (compteurGraine == nbGraines)
                    {
                        resultats.Add(parcelleEtudiee);
                    }
                }
            }
            return resultats;
        }

        private static void validCoordonees(List<Parcelle> zoneRecherche, int lastPosX, int lastPosY, ref String answerPos)
        {
            if (zoneRecherche.Count > 0)
            {
                foreach (Unite uniteEtudiee in zoneRecherche.ElementAt(0).GetAssignedUnites())
                {
                    if (uniteEtudiee.GetStatus() == 0 && (uniteEtudiee.GetposX() == lastPosX || uniteEtudiee.GetposY() == lastPosY))
                    {
                        if (uniteEtudiee.Getparcelleletter() != Map.GetUnite(lastPosX, lastPosY).Getparcelleletter())
                        {
                            answerPos = uniteEtudiee.GetposX().ToString() + uniteEtudiee.GetposY().ToString();
                        }
                    }
                }
            }
        }
    }
}
