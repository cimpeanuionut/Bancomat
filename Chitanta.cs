using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Bancomat
{
    public partial class Chitanta : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=folder");
        bool ok = RateCredit.ok1; 
        string limba = Form1.passinglimba;
        string card = Pin.cardcod;
        string asuma = AltaSuma.sumapars;
        string pin = Pin.passingPin;       
        string credit = RateCredit.credit;
        string verif = AltaSuma.checksum;
        string suma = RetragereNumerar.suma;
        string disponibil;
        public Chitanta()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
            
        }

        private void Chitanta_Load(object sender, EventArgs e)
        {
            chitanta();
        }

        public void obt_dispo()
        {
            con.Open();
            MySqlCommand cmd;
            MySqlDataReader mdr;
            string selectQuery = "select * from bancomat where PIN = '" + pin + "'";
            cmd = new MySqlCommand(selectQuery, con);
            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                disponibil = mdr.GetString("Suma");
                
            }
            mdr.Close();
            con.Close();
        }

        public void chitanta()
        {
            var src = DateTime.Now;
            Random rnd = new Random();
            int nr = rnd.Next(1, 10000);
            label7.Text = "Card:" + card;
            label6.Text = "Succes";
            if ( limba == "romana")
            {
                label1.Text = "Banca Comercială Română";
                label9.Text = "Data:";
                label2.Text = "Ora:";
                label3.Text = "Locația:București";
                label4.Text = "Nr. chitanță:";
                label5.Text = "ATM Cod:w62inb#p0";
                
                if (ok == true)
                {
                    label8.Text = "Credit:" + credit;
                    label10.Visible = false;
                    
                } 
                else
                {
                    
                    if (verif == "alta")
                    {
                        label8.Text = "Suma extrasă:" + asuma;
                    }
                    else
                    {
                        label8.Text = "Suma extrasă:" + suma;
                    }
                    obt_dispo();
                    label10.Text = "Disponibil:" + disponibil;
                }
                
                                
            }
            else
            {
                label1.Text = "Romanian Commercial Bank";
                label9.Text = "Date:";
                label2.Text = "Hour:";
                label3.Text = "Location:Bucharest";
                label4.Text = "Number of the receipt:";
                label5.Text = "ATM Code:w62inb#p0";
                if (ok == true)
                {
                    label8.Text = "Credit:" + credit;
                    label10.Visible = false;
                }
                else
                {
                    if (verif == "alta")
                    {
                        label8.Text = "The amount extracted:" + asuma;
                    }
                    else
                    {
                        label8.Text = "The amount extracted:" + suma;
                    }
                    obt_dispo();
                    label10.Text = "Available:" + disponibil;
                    
                }
            }
            label9.Text = label9.Text + src.Day + "." + src.Month + "." + src.Year;
            label2.Text = label2.Text + src.Hour + ":" + src.Minute + ":" + src.Second;
            label4.Text = label4.Text + nr.ToString();
            
        }
    }
}
