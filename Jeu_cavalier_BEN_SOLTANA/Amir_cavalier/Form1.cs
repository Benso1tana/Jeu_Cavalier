using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// ce projet C# est ralisé par Amir BEN SOLTANA
//ING2 Instrumentation
//Encadré par Monsieur Karim FOUGHALI
namespace Amir_cavalier
{
    public partial class Jeu_de_Cavalier : Form
    {
        const int DameSize = 8;   //la taille du Damier
        const int btnSize = 60;  //taille de bouttons du Damier
        int ClickButton = 0;    //clicker sur un boutton du Damier
        int btn;               //un boutton du Damier
        int btn_xPos = 0;     //la positon du boutton suivant l'axe x
        int btn_yPos = 0;    //la positon du boutton suivant l'axe y
        Button[,] Button_dame = new Button[8, 8];
        int[] btnPas = new int[8]; //Déplacement
        int[] BtnClicked = new int[64];//64 boutons de la Grille
        bool ENDgame = true;//fin de jeu
       
        public Jeu_de_Cavalier() //Initialiser le jeu 
        {
            InitializeComponent();
            label7.Text = "";
            label7.Visible = false;
            menuStrip1.Visible = false;
            label3.Text = "Jeu Cavalier";
            label3.ForeColor = Color.Red;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            textBox1.Visible = true;
            Rejouer.Visible = false;
            pictureBox2.Image = Image.FromFile("image\\cavalier2.png");
            button1.Visible = true;
            button2.Visible = true;
            pictureBox1.Visible = false;
            label1.Visible = false;
            label6.Visible = false;
            button6.Visible = false;
        }
        private void btn_clicked(object sender, EventArgs e) //clicker sur un bouton de la Grille
        {
            button2.Enabled = false;
            Button btn = sender as Button;
            this.btn = Convert.ToInt32(btn.Name);
           BtnClicked[ClickButton] = this.btn;
            ClickButton += 1;
            disableButton();
           // proposer les différents chemins nécessaires pour le déplacement de cavalier
           btnPas[0] = this.btn - 21;
            btnPas[1] = this.btn - 19;
            btnPas[2] = this.btn - 12;
            btnPas[3] = this.btn - 8;
            btnPas[4] = this.btn + 8;
            btnPas[5] = this.btn + 12;
            btnPas[6] = this.btn + 19;
            btnPas[7] = this.btn + 21;
            coleurDame(DameSize);//changement de coleur du damier
            Deplacement(BtnClicked);// deplacer le cavalier
            btn.BackgroundImage = Image.FromFile("image\\cavalier.jpg");//afficher l'image du cavalier
            ENDgame = true;//fin de jeu
            foreach (int choixBTN in btnPas)
            {
                int valide = verification(BtnClicked, choixBTN);
                if (valide == 0)
                {
                    // Active le bouton 
                    foreach (Button buttonToEnable in Button_dame)
                    {
                        if (choixBTN == Convert.ToInt32(buttonToEnable.Name))
                        {
                            buttonToEnable.Enabled = true;
                            buttonToEnable.BackColor = Color.Green;// afficher le chemin de déplacement de cavalier par une couleur verte
                            ENDgame = false;
                        }
                    }
                }
            }
            if (ENDgame)
            {
                Findujeu(ClickButton);
            }
        }
        private int verification(int[] array, int value)// verification si le cavalier peut se deplacer encore
        {
            foreach (int chemin in array)
            {
                if (value == chemin)
                {
                    return 1;
                }
            }
            return 0;
        }
        private void Bouton_Grille(int x, int y, int name, int color) // creation des boutons de la Grille
        {
            Button btn = new Button();
            btn.Size = new Size(btnSize, btnSize);
            Damier.Controls.Add(btn);
            btn.Location = new Point(x, y);
            if (color == 0)
            {
                btn.BackColor = Color.White;
            }
            if (color == 1)
            {
                btn.BackColor = Color.Red;
            }
            btn.Name = Convert.ToString(name);
            btn.Text = Convert.ToString(name);
            btn.Click += new EventHandler(btn_clicked);
            Damier.Controls.Add(btn);
        }
        public void AfficheDame(int Damesaize)//Afficher le damier
        {
            for (int i = 0; i < Damesaize; i++)
            {
                btn_xPos = 0;
                for (int j = 0; j < Damesaize; j++)
                {
                    Button_dame[i, j] = new Button();
                    ((Button)Button_dame[i, j]).Name = "" + (i + 1) + j; 
                    ((Button)Button_dame[i, j]).Size = new Size(btnSize, btnSize);
                    ((Button)Button_dame[i, j]).Location = new Point(btn_xPos, btn_yPos);
                    ((Button)Button_dame[i, j]).Click += new EventHandler(btn_clicked);
                    Damier.Controls.Add((Button)Button_dame[i, j]);
                    btn_xPos += btnSize;
                }
                    btn_yPos += btnSize;
            }
        }
        public void coleurDame(int Damesize)//Initialiser le damier avec des couleurs (blanc et marron)
        {
            for (int i = 0; i < Damesize; i++)
            {
                for (int j = 0; j < Damesize; j++)
                {
                    ((Button)Button_dame[i, j]).BackgroundImage = null;
                    if (j % 2 == 0 && i % 2 == 0 || j % 2 == 1 && i % 2 == 1)
                    {
                        ((Button)Button_dame[i, j]).BackColor = Color.White;
                    }
                    else
                    {
                        ((Button)Button_dame[i, j]).BackColor = Color.Black;
                    }
                }
            }
        }
        private void Deplacement(int[] buttonUsed) // deplacer le cavalier 
        {
            foreach (Button colorButton in Button_dame)
            {
                for (int i = 0; i < ClickButton; i++)
                {
                    if (buttonUsed[i] == Convert.ToInt32(colorButton.Name))
                    {
                        colorButton.BackgroundImage= Image.FromFile("image\\tic.jpg");
                    }
                }
            }
        }
        private void Jouer(object sender, EventArgs e) // lancer le jeu
        {
            label6.Visible = true;
            label6.Text = "Placé votre Cavalier \n vous avez le choix !";
            menuStrip1.Visible = true;
            label3.Visible = false;
            pictureBox2.Visible = false;
            label7.Visible = true;
            label4.Visible = false;
            label5.Visible = false;
            textBox1.Visible = false;
            AfficheDame(DameSize);
            coleurDame(DameSize);
            button1.Visible = false;
            button2.Visible = false;
            button6.Visible = true;
        }
        private void Findujeu(int nbButtonClicked) // fin du jeu et affichage de score obtenu
        {
            label5.Text= textBox1.Text;
            disableButton();
            if (nbButtonClicked == 64)// si on termine le jeu 
            {
               
                label7.Text = label5.Text+"!  Vous avez gagné !";
                Rejouer.Visible = true;
            }
            else
            { 
                label7.Text = label5.Text+"! Dommage Vous avez perdu \n  vous avez obtenu " + nbButtonClicked + " points";
                pictureBox1.Image = Image.FromFile("image\\game over.jpg");
                pictureBox1.Visible = true;
                Rejouer.Visible = true;
            }
        }
        private void disableButton()
        {
            foreach (Button buttonDisabled in Button_dame)  //Verrouillage de tous les boutons
            {
                buttonDisabled.Enabled = false;
            }
        }
        private void Regle_cavalier(object sender, EventArgs e)// Règles de jeu
        {
          MessageBox.Show("Cliquez sur une case, puis déplacez \nvotre cavalier selon les règles du jeu d'échec.\n\nLe but est d'atteindre un maximum \nde cases.", "Tableau des scores", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button2);
        }
        private void Rejouer_Click(object sender, EventArgs e) // Rejouer une partie
        {
           
            ClickButton = 0;
            coleurDame(DameSize);
            foreach (Button buttonDisabled in Button_dame) //Verrouillage de tous les boutons
            {
                buttonDisabled.Enabled = true;
                buttonDisabled.Text = "";
            }
            pictureBox1.Visible = false;
            Rejouer.Visible = false;
            button2.Enabled = true;
            label1.Text = "";
        }
        private void Aleatoire(object sender, EventArgs e)
        {
            //button2.Visible = false;
            //button3.Enabled = false;
            Array.Clear(BtnClicked, 0, BtnClicked.Length);
            Array.Clear(btnPas, 0, btnPas.Length);
            Random alea= new Random();
            int l = alea.Next(0, 7);     //un entier aleatoire compris entre 0 et 7
            int x = alea.Next(0, 7);
            btn = Convert.ToInt32(Button_dame[l,x].Name);
            BtnClicked[ClickButton] = btn;
            ClickButton += 1;
            Button_dame[l, x].Enabled = false;
            //proposer les différents chemins nécessaires pour le déplacement de cavalier
            btnPas[0] = btn - 21;                      
            btnPas[1] = btn - 19; 
            btnPas[2] = btn - 12;  
            btnPas[3] = btn - 8;   
            btnPas[4] = btn + 8;  
            btnPas[5] = btn + 12;  
            btnPas[6] = btn + 19;  
            btnPas[7] = btn + 21;  
            ENDgame = true;

            foreach (int choixBtn in btnPas)
            {
                int isValid = verification(BtnClicked, choixBtn);
                if (isValid == 0)//le cavalier peut se déplacer encore 
                {

                    foreach (Button buttonToEnable in Button_dame) //activer le bouton et changer le couleur
                    {
                        if (choixBtn == Convert.ToInt32(buttonToEnable.Name))
                        {
                            buttonToEnable.Enabled = true;
                            buttonToEnable.BackColor = Color.Red;
                            ENDgame = false;
                        }
                    }
                }
                Button_dame[l, x].BackColor=Color.Yellow;

            }
        }

        private void Apropos(object sender, EventArgs e)//Afficher un message pour décrire le jeu de cavalier
        {
            MessageBox.Show("Ce jeu a été réalisé dans le cadre d'un projet sous C# par Mohamed Amine BEN SOLTANA. \nLe but est d'atteindre un maximum de cases."," jeu du cavalier", MessageBoxButtons.OK, MessageBoxIcon.None);
        }
    }
}



