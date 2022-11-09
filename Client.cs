using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace La_bataille_du_café
{
    public partial class Client : Form
    {
        List<PictureBox> cases = new List<PictureBox>();
        List<PictureBox> bordersUp = new List<PictureBox>();
        List<PictureBox> bordersDown = new List<PictureBox>();
        List<PictureBox> bordersRight = new List<PictureBox>();
        List<PictureBox> bordersLeft = new List<PictureBox>();

        public Client()
        {
            InitializeComponent();
        }


        private void Client_Load(object sender, EventArgs e)
        {
            byte[] map_trame = new byte[0];
            try
            {
                map_trame = Moteur.Initialisation.GetMap.StartGetMap(); //Récupération de la carte
            }
            catch (System.Net.Sockets.SocketException error)
            {
                MessageBox.Show("La connexion au serveur a échoué !" + "\n" + "Le serveur est injoignable, vérifiez votre connexion internet." + "\n\n" + error, "Erreur de connexion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Moteur.Initialisation.Decode.StartDecode(map_trame);

            byte nbcaselongueur = 0;
            byte nbcaselargeur = 0;
            int x = 280;
            int y = 132;

            PictureBox start_picture;

            while (nbcaselongueur < 10 && nbcaselargeur < 10)
            {
                while (nbcaselongueur < 10)
                {
                    start_picture = new PictureBox();
                    cases.Add(start_picture);
                    Point loc = new Point();
                    loc.X = x;
                    loc.Y = y;

                    start_picture.Name = "pic" + nbcaselongueur.ToString() + nbcaselargeur.ToString();
                    start_picture.Size = new Size(50, 50);
                    start_picture.Location = loc;
                    start_picture.BackColor = Color.White;
                    start_picture.Visible = true;
                    start_picture.BorderStyle = BorderStyle.Fixed3D;
                    start_picture.SizeMode = PictureBoxSizeMode.Zoom;
                    start_picture.BackgroundImageLayout = ImageLayout.Stretch;
                    Controls.Add(start_picture);
                    start_picture.BringToFront();
                    MakeBorders(start_picture);
                    x += 50;

                    if (x == (50 * 10 + 280))
                    {
                        x = 280;
                        y += 50;
                    }
                    nbcaselongueur += 1;
                }
                nbcaselargeur += 1;
                nbcaselongueur = 0;
            }

            ShowMap();
            Moteur.Jeu.Play.GoPlay();
            updateMap();
        }

        private void MakeBorders(PictureBox gamecase)
        {
            PictureBox border_picture;

            border_picture = new PictureBox();
            border_picture.Location = new Point(gamecase.Location.X, gamecase.Location.Y - 3);
            border_picture.Size = new Size(gamecase.Size.Width, 6);
            border_picture.BackColor = Color.Black;
            border_picture.Visible = false;
            Controls.Add(border_picture);
            border_picture.BringToFront();
            bordersUp.Add(border_picture);

            border_picture = new PictureBox();
            border_picture.Location = new Point(gamecase.Location.X, gamecase.Location.Y + gamecase.Size.Height - 3);
            border_picture.Size = new Size(gamecase.Size.Width, 6);
            border_picture.BackColor = Color.Black;
            border_picture.Visible = false;
            Controls.Add(border_picture);
            border_picture.BringToFront();
            bordersDown.Add(border_picture);

            border_picture = new PictureBox();
            border_picture.Location = new Point(gamecase.Location.X + gamecase.Size.Width - 3, gamecase.Location.Y);
            border_picture.Size = new Size(6, gamecase.Size.Height);
            border_picture.BackColor = Color.Black;
            border_picture.Visible = false;
            Controls.Add(border_picture);
            border_picture.BringToFront();
            bordersRight.Add(border_picture);

            border_picture = new PictureBox();
            border_picture.Location = new Point(gamecase.Location.X - 3, gamecase.Location.Y);
            border_picture.Size = new Size(6, gamecase.Size.Height);
            border_picture.BackColor = Color.Black;
            border_picture.Visible = false;
            Controls.Add(border_picture);
            border_picture.BringToFront();
            bordersLeft.Add(border_picture);
        }

        private void ShowMap()
        {
            for (int boucleY = 0; boucleY < 10; boucleY++)
            {
                for (int boucleX = 0; boucleX < 10; boucleX++)
                {
                    if (Moteur.Map.GetUnite(boucleX, boucleY).GetType().Name == "Terre")
                    {
                        foreach (Control ctrlboucle in this.Controls)
                        {
                            if (ctrlboucle.Name == "pic" + boucleX.ToString() + boucleY.ToString())
                            {
                                ctrlboucle.BackColor = Color.DarkOrange;
                            }
                        }
                    }
                    else if (Moteur.Map.GetUnite(boucleX, boucleY).GetType().Name == "Mer")
                    {
                        foreach (Control ctrlboucle in this.Controls)
                        {
                            if (ctrlboucle.Name == "pic" + boucleX.ToString() + boucleY.ToString())
                            {
                                ctrlboucle.BackColor = Color.DarkBlue;
                            }
                        }
                    }
                    else if (Moteur.Map.GetUnite(boucleX, boucleY).GetType().Name == "Foret")
                    {
                        foreach (Control ctrlboucle in this.Controls)
                        {
                            if (ctrlboucle.Name == "pic" + boucleX.ToString() + boucleY.ToString())
                            {
                                ctrlboucle.BackColor = Color.DarkGreen;
                            }
                        }
                    }

                    if (Moteur.Map.GetUnite(boucleX, boucleY).GetborderUp() == true)
                    {
                        bordersUp.ElementAt(boucleY * 10 + boucleX).Visible = true;
                    }
                    if (Moteur.Map.GetUnite(boucleX, boucleY).GetborderDown() == true)
                    {
                        bordersDown.ElementAt(boucleY * 10 + boucleX).Visible = true;
                    }
                    if (Moteur.Map.GetUnite(boucleX, boucleY).GetborderRight() == true)
                    {
                        bordersRight.ElementAt(boucleY * 10 + boucleX).Visible = true;
                    }
                    if (Moteur.Map.GetUnite(boucleX, boucleY).GetborderLeft() == true)
                    {
                        bordersLeft.ElementAt(boucleY * 10 + boucleX).Visible = true;
                    }
                }
            }
        }

        private void updateMap()
        {
            for (int boucleY = 0; boucleY < 10; boucleY++)
            {
                for (int boucleX = 0; boucleX < 10; boucleX++)
                {
                    if (Moteur.Map.GetUnite(boucleX, boucleY).GetStatus() == 0)
                    {
                        foreach (Control ctrlboucle in this.Controls)
                        {
                            if (ctrlboucle.Name == "pic" + boucleX.ToString() + boucleY.ToString())
                            {
                                ctrlboucle.BackgroundImage = null;
                            }
                        }
                    }
                    else if (Moteur.Map.GetUnite(boucleX, boucleY).GetStatus() == 1)
                    {
                        foreach (Control ctrlboucle in this.Controls)
                        {
                            if (ctrlboucle.Name == "pic" + boucleX.ToString() + boucleY.ToString())
                            {

                                ctrlboucle.BackgroundImage = Properties.Resources.Graine_blanche;
                            }
                        }
                    }
                    else if (Moteur.Map.GetUnite(boucleX, boucleY).GetStatus() == 2)
                    {
                        foreach (Control ctrlboucle in this.Controls)
                        {
                            if (ctrlboucle.Name == "pic" + boucleX.ToString() + boucleY.ToString())
                            {

                                ctrlboucle.BackgroundImage = Properties.Resources.Graine_noire;
                            }
                        }
                    }
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
