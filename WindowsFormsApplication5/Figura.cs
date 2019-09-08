using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication5
{
    public abstract class Figura
    {
        public int X { get; set; }
        public int Y { get; set; }

        protected Color boja;

        public Figura()
        {
            X = 5;
            Y = 1;
            boja = NapraviBoju();
        }

        public abstract void Nacrtaj(Graphics g);

        private static Color[] boje = { Color.Red, Color.Green, Color.Blue, Color.Yellow };
        private static Random rand = new Random();
        public static Color NapraviBoju()
        {
            return boje[rand.Next(boje.Length)];
        }
    }

    public class Mala : Figura
    {
        public override void Nacrtaj(Graphics g)
        {
            g.FillEllipse(new SolidBrush(this.boja), new Rectangle(new Point(this.X * 20, this.Y * 20), new Size(20, 20)));
        }
    }

    public class Velika : Figura
    {
        public override void Nacrtaj(Graphics g)
        {
            g.FillRectangle(new SolidBrush(this.boja), new Rectangle(new Point(this.X * 20, this.Y * 20), new Size(40, 20)));
        }
    }
}
