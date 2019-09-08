using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tbIme.Focus();
        }

        private void pocni()
        {
            string ime = tbIme.Text;
            Igra igra = new Igra(ime);
            igra.Show();
        }

        private void btnPocni_Click(object sender, EventArgs e)
        {
            pocni();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                pocni();
            }
        }
    }
}
