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
    public partial class InterogareSold : Form
    {
        string limba = Form1.passinglimba;
        string Sold;
        string codul;
        string card;
        string data, ora, tranzactia;
        int suma;
        MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=folder");
        public InterogareSold()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Pin pin = new Pin();
            pin.Show();
            this.Hide();
        }

        private void InterogareSold_Load(object sender, EventArgs e)
        {
            label6.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            button3.Enabled = false;
            button4.Enabled = false;
            groupBox1.Visible = false;
            selectare_limba();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            label2.Visible = false;
            label5.Visible = false;
            label6.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = false;
            con.Open();
            afisaresold();
            chitanta();
            istoric();
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            label2.Visible = false;
            label5.Visible = false;
            label6.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = false;
            con.Open();
            afisaresold();
            istoric();
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        public  void afisaresold()
        {

            codul = Pin.passingPin;
            MySqlCommand cmd;
            MySqlDataReader mdr;
            string selectQuery = "select * from bancomat where PIN = '" +codul+ "'";
            cmd = new MySqlCommand(selectQuery, con);
            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                Sold = mdr.GetString("Suma");
                card = mdr.GetString("cod_card");
               
            }
            mdr.Close();
            lbPin.Text = Sold;
        }

        public void selectare_limba()
        {
            if ( limba == "romana")
            {
                label3.Text = "Da! Apăsați aici!";
                label4.Text = "Nu! Apăsați aici! ";
                label1.Text = "Da";
                label2.Text = "Nu";
                label6.Text = "Doriți altă tranzacție?";
                label5.Text = "Doriți chitanță?";
            }
            else
            {
                label3.Text = "Yes! Click here!";
                label4.Text = "No! Click here! ";
                label1.Text = "Yes";
                label2.Text = "No";
                label6.Text = "Do you want another transaction?";
                label5.Text = "Do you want the receipt?";
            }
        }

        public void chitanta()
        {
            groupBox1.Visible = true;
            Random rnd = new Random();
            int nr = rnd.Next(1, 10000);
            if ( limba == "romana")
            {
                label10.Text = "Locația:București";
                label9.Text = "Data:";
                label11.Text = "Ora:";
                label8.Text = "Nr. chitanță:";
                label12.Text = "ATM Cod:w62inb#p0";
                label13.Text = "Card:";
                label14.Text = "Succes";
                label16.Text = "Sold Disponibil:";
                label7.Text = "Banca Comercială Română";
            }
            else
            {
                label10.Text = "Location:Bucharest";
                label9.Text = "Date:";
                label11.Text = "Hour:";
                label8.Text = "Number of the receipt:";
                label12.Text = "ATM Code:w62inb#p0";
                label13.Text = "Card:";
                label14.Text = "Succes";
                label16.Text = "Available Amount:";
                label7.Text = "Romanian Commercial Bank";
            }

            var src = DateTime.Now;
            label9.Text = label9.Text + src.Day + "." + src.Month + "." + src.Year;
            label11.Text = label11.Text + src.Hour + ":" + src.Minute + ":" + src.Second;
            label16.Text = label16.Text + Sold;
            label8.Text = label8.Text + nr.ToString();
            label13.Text = label13.Text + card;
        }

        public void istoric()
        {
            var src = DateTime.Now;
            data = src.Day.ToString() + "." + src.Month.ToString() + "." + src.Year.ToString();
            ora = src.Hour.ToString() + ":" + src.Minute.ToString() + ":" + src.Second.ToString();
            tranzactia = "Interogare Sold";
            
            string newCon = "insert into istoricbancomat(cod_card,Data,Ora,Suma_i,Tranzactia) VALUES ('" + card + "','" + data + "','" + ora + "','" + Sold + "','" + tranzactia + "')";
            MySqlCommand cmd = new MySqlCommand(newCon, con);
            cmd.ExecuteNonQuery();

        }
    }
}
