using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur.Unites
{
    public abstract class Unite
    {
        private int[] position = new int[2]; //Ordre : X (largeur) ; Y (hauteur)
        private Boolean[] borders = new Boolean[4]; //Ordre : Nord, Sud, Est, Ouest
        private Char parcelleLetter;
        private int status; //0:Vide ; 1:Allié ; 2:Ennemi

        public Unite(int posX, int posY, Boolean borderUp, Boolean borderDown, Boolean borderRight, Boolean borderLeft)
        {
            position[0] = posX;
            position[1] = posY;
            borders[0] = borderUp;
            borders[1] = borderDown;
            borders[2] = borderRight;
            borders[3] = borderLeft;
            status = 0;
        }

        public int GetposX() // retourne la position horizontale de l'unité
        {
            return this.position[0];
        }

        public int GetposY() // retourne la position verticale de l'unité
        {
            return this.position[1];
        }

        public Boolean GetborderUp() // retourne la vérification de l'existence d'une fontière au-dessus de l'unité
        {
            return this.borders[0];
        }

        public Boolean GetborderDown() // retourne la vérification de l'existence d'une fontière sous de l'unité
        {
            return this.borders[1];
        }

        public Boolean GetborderRight() // retourne la vérification de l'existence d'une fontière à droite de l'unité
        {
            return this.borders[2];
        }

        public Boolean GetborderLeft() // retourne la vérification de l'existence d'une fontière à gauche de l'unité
        {
            return this.borders[3];
        }

        public Char Getparcelleletter() // retourne la lettre de la parcelle correspondante à l'unitée
        {
            return this.parcelleLetter;
        }

        public int GetStatus() // retourne l'état d'occupation de l'unité
        {
            return this.status;
        }

        public void SetborderUp(Boolean borderValue) // indique la présence d'une fontière au-dessus de l'unité
        {
            this.borders[0] = borderValue;
        }

        public void SetborderDown(Boolean borderValue) // indique la présence d'une fontière sous de l'unité
        {
            this.borders[1] = borderValue;
        }

        public void SetborderRight(Boolean borderValue) // indique la présence d'une fontière à droite de l'unité
        {
            this.borders[2] = borderValue;
        }

        public void SetborderLeft(Boolean borderValue) // indique la présence d'une fontière à gauche de l'unité
        {
            this.borders[3] = borderValue;
        }

        public void SetparcelleLetter(Char letterParcelle) // attribut une lettre de parcelle à l'unité
        {
            this.parcelleLetter = letterParcelle;
        }

        public void SetStatus(int status) // attribut un état d'occupation à l'unité
        {
            this.status = status;
        }

    }
}
