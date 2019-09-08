using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class Igra : Form
    {
        string ime;
        int poeni;
        int figura = 0;

        Figura[,] figure;
        Figura f;

        Keys trenutnoDugme = Keys.Down;

        public Igra(string ime)
        {
            InitializeComponent();
            this.ime = ime;
            lblIme.Text = ime;
        }

        private void novaIgraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            figure = new Figura[20, 10];
            poeni = 0;
            figura = 0;
            lblPoeni.Text = poeni.ToString();
            lblFigura.Text = figura.ToString();
            NovaFigura();
            GameTimer.Start();
        }

        private void NovaFigura()
        {
            for (int i = 0; i < 10; i++)
            {
                if (figure[2, i] != null)
                {
                    GameTimer.Stop();
                    MessageBox.Show($"Pobedili ste, osvojili ste {poeni} poena");
                }
            }
            if (new Random().Next(2) == 0)
                f = new Mala();
            else
                f = new Velika();

            figure[f.X, f.Y] = f;
            figura++;
            lblFigura.Text = figura.ToString();
        }

        private void pomeri()
        {
            figure[f.Y, f.X] = null;
            switch (trenutnoDugme)
            {
                case Keys.Down:
                    f.Y++;
                    break;
                case Keys.Left:
                    if (f.X > 0)
                        f.X--;
                    break;
                case Keys.Right:
                    if ((f.X < 9 && f is Mala) || (f.X < 8 && f is Velika))
                        f.X++;
                    break;
            }
            trenutnoDugme = Keys.Down;
            figure[f.Y, f.X] = f;

            if (f.Y == 19 ||
                figure[f.Y + 1, f.X] != null ||
                (f.X > 0 && f.Y < 20 && figure[f.Y + 1, f.X - 1] is Velika) ||
                (f.X >= 0 && f.Y < 20 && f is Velika && figure[f.Y + 1, f.X + 1] != null))
            {
                proveriRed();
                NovaFigura();
            }

            GamePanel.Invalidate();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            pomeri();
        }

        private void proveriRed()
        {
            GameTimer.Stop();
            for (int i = 19; i >= 0; i--)
            {
                bool pun = true;
                for (int j = 0; j < 10; j++)
                {
                    if (figure[i, j] == null && !(j > 0 && figure[i, j - 1] is Velika))
                    {
                        pun = false;
                    }
                }
                if (pun)
                {
                    for (int k = i; k >= 1; k--)
                    {
                        for (int l = 0; l < 10; l++)
                        {
                            figure[k, l] = figure[k - 1, l];
                            if (figure[k, l] != null)
                                figure[k, l].Y++;
                        }
                    }
                    poeni += 100;
                    lblPoeni.Text = poeni.ToString();
                    if (poeni != 0 && poeni % 500 == 0 && GameTimer.Interval > 50)
                    {
                        GameTimer.Interval = GameTimer.Interval - 50;
                    }
                }
            }
            GameTimer.Start();
        }

        private void proveriPobedu()
        {
            if (f.Y == 1)
            {
                GameTimer.Stop();
                MessageBox.Show($"Pobeda, osvojili ste {poeni} poena.");
            }
        }

        private void GamePanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (figure == null) return;

            foreach (Figura fig in figure)
            {
                fig?.Nacrtaj(g);
            }
        }

        private void Igra_KeyDown(object sender, KeyEventArgs e)
        {
            trenutnoDugme = e.KeyCode;
            if (trenutnoDugme == Keys.P || trenutnoDugme == Keys.Space)
            {
                if (GameTimer.Enabled)
                    GameTimer.Stop();
                else
                    GameTimer.Start();
            }

            if (trenutnoDugme == Keys.Down)
            {
                pomeri();
            }
        }

        private void zatvoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
