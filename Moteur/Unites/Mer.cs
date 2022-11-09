using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur.Unites
{
    public class Mer : Unite
    {
        public Mer(int posX, int posY, Boolean borderUp, Boolean borderDown, Boolean borderRight, Boolean borderLeft) : base(posX, posY, borderUp, borderDown, borderRight, borderLeft) { }
    }
}
