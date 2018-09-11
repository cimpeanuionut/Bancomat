using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bancomat
{
    public partial class Form1 : Form
    {
        public static string passinglimba;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            passinglimba = "romana";
            Pin p = new Pin();
            p.Show();
            this.Hide();
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            passinglimba = "engleza";
            Pin p = new Pin();
            p.Show();
            this.Hide();
        }
    }
}
