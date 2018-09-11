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
    public partial class AltaSuma : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=folder");        
        string limba = Form1.passinglimba;
        string pin = Pin.passingPin;
        string cod = Pin.cardcod;
        string data, ora, tranzactia;
        public static string sumapars;
        public static string checksum;
        int bani;
        bool ok = false;
        int disponibil;
        bool money = true; 
        public AltaSuma()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            lbPin.Text = lbPin.Text + "1";
            

        }
        
        private void button8_Click(object sender, EventArgs e)
        {

            lbPin.Text = lbPin.Text + "3";
            
        }

        private void button12_Click(object sender, EventArgs e)
        {

            lbPin.Text = lbPin.Text + "4";
            
        }

        private void button9_Click(object sender, EventArgs e)
        {

            lbPin.Text = lbPin.Text + "5";
            
        }

        private void button16_Click(object sender, EventArgs e)
        {

            lbPin.Text = lbPin.Text + "6";
            
        }        

        private void button10_Click(object sender, EventArgs e)
        {

            lbPin.Text = lbPin.Text + "8";
            
        }

        private void button11_Click(object sender, EventArgs e)
        {

            lbPin.Text = lbPin.Text + "9";
            
        }

        private void button13_Click(object sender, EventArgs e)
        {

            lbPin.Text = lbPin.Text + "0";

        }

        private void button15_Click(object sender, EventArgs e)
        {
            lbPin.Text = "";
            money = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult iExit;

            if (limba == "romana")
            {
                iExit = MessageBox.Show("Dorești să părăsești tranzacția?", "Sistem Bancomat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else
            {
                iExit = MessageBox.Show("Do you want to leave the transaction?", "ATM Sistem", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            if (iExit == DialogResult.Yes)
            {
                Form1 f1 = new Form1();
                f1.Show();
                this.Hide();
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            lbPin.Text = lbPin.Text + "2";
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            lbPin.Text = lbPin.Text + "7";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ok == false)
            {
                lbPin.Text = "";
                money = true;
            }
            else
            {
                Form1 f1 = new Form1();
                f1.Show();
                this.Hide();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ok == false)
            {
                bani = Int32.Parse(lbPin.Text.ToString());
                verif_numerar(bani);
                if (money == true)
                {
                    if (bani % 10 == 0)
                    {
                        if (bani <= 3000)
                        {
                            con.Open();
                            istoric(bani);
                            MySqlCommand cmd = con.CreateCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "update bancomat set Suma= Suma - '" + bani + "' where PIN = '" + pin + "'";
                            sumapars = bani.ToString();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            ok = true;
                            terminare();
                        }
                        else
                        {
                            lbPin.Text = "";
                            if (limba == "romana")
                            {
                                MessageBox.Show("Introduceți maxim 3000 de lei");
                            }
                            else
                            {
                                MessageBox.Show("Enter maxim 3000 RON");
                            }
                        }
                    }

                    else
                    {
                        lbPin.Text = "";
                        if (limba == "romana")
                        {
                            MessageBox.Show("Suma trebuie să fie multiplu de 10");
                        }
                        else
                        {
                            MessageBox.Show("Sum must be multiple of 10");
                        }

                    }
                } 
            }
            else
            {
                checksum = "alta";
                Chitanta f1 = new Chitanta();
                f1.Show();
                this.Hide();
            }
            
        }

        private void AltaSuma_Load(object sender, EventArgs e)
        {
            label4.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            selectare_limba();
        }
        public void terminare()
        {
            label4.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            

        }
        public void selectare_limba()
        {
            if ( limba == "romana")
            {
                label4.Text = "Doriți chitanță?";
                label5.Text = "Introduceți Suma Dorită";
                label1.Text = "Suma introdusă trebuie să fie multiplu de 10";
                label7.Text = "Nu";
                label8.Text = "Da";
                label6.Text = "Corect, apăsați aici";
                label2.Text = "Incorect, apăsați aici";
            }
            else
            {
                label4.Text = "Do you want the receipt?";
                label5.Text = "Enter the sum";
                label1.Text = "Sum must be multiple be 10";
                label7.Text = "No";
                label8.Text = "Yes";
                label6.Text = "Correct, click here";
                label2.Text = "Wrong, click here";
            }
        }

        public void verif_numerar(int bani)
        {
            con.Open();
            MySqlCommand cmd;
            MySqlDataReader mdr;
            string selectQuery = "select * from bancomat where PIN = '" + pin + "'";
            cmd = new MySqlCommand(selectQuery, con);
            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                disponibil = mdr.GetInt32("Suma");

            }
            if (limba == "romana")
            {
                if (bani > disponibil)
                {
                    MessageBox.Show("Nu ai suficienti bani in cont");
                    money = false;
                }
            }
            else
            {
                if (bani > disponibil)
                {
                    MessageBox.Show("You don't have money");
                    money = false;
                }
            }
            mdr.Close();
            con.Close();
        }

        public void istoric( int retragere)
        {
            var src = DateTime.Now;
            data = src.Day.ToString() + "." + src.Month.ToString() + "." + src.Year.ToString();
            ora = src.Hour.ToString() + ":" + src.Minute.ToString() + ":" + src.Second.ToString();
            tranzactia = "Retragere numerar:" + retragere;
            int suma_f = disponibil - retragere;
            string newCon = "insert into istoricbancomat(cod_card,Data,Ora,Suma_i,Tranzactia,Suma_f) VALUES ('" + cod + "','" + data + "','" + ora + "','" + disponibil + "','" + tranzactia + "','" + suma_f + "')";
            MySqlCommand cmd = new MySqlCommand(newCon, con);
            cmd.ExecuteNonQuery();
        }

    }
}
