using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur
{
    public class Parcelle
    {
        private List<Unites.Unite> assignedUnites = new List<Unites.Unite>();

        public void addUnite(Unites.Unite newUnite)
        {
            assignedUnites.Add(newUnite);
        }
        public List<Unites.Unite> GetAssignedUnites() // retourne la position horizontale de l'unité
        {
            return this.assignedUnites;
        }
    }
}
