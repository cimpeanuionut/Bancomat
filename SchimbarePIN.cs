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
    public partial class SchimbarePIN : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=folder");
        bool OK = false;
        string limba = Form1.passinglimba;
        int PIN;
        public SchimbarePIN()
        {
            InitializeComponent();
        }
        public void verificarelungimePIN()
        {
            if (lbPin.Text.Length > 4)
            {
                if (limba == " romana")
                {
                    MessageBox.Show("PIN-ul este invalid");
                }
                else
                {
                    MessageBox.Show("Invalid PIN");
                }
                lbPin.Text = "";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            lbPin.Text = lbPin.Text + "1";
            verificarelungimePIN();

        }

        private void button7_Click(object sender, EventArgs e)
        {

            lbPin.Text = lbPin.Text + "2";
            verificarelungimePIN();

        }

        private void button8_Click(object sender, EventArgs e)
        {

            lbPin.Text = lbPin.Text + "3";
            verificarelungimePIN();
        }

        private void button12_Click(object sender, EventArgs e)
        {

            lbPin.Text = lbPin.Text + "4";
            verificarelungimePIN();
        }

        private void button9_Click(object sender, EventArgs e)
        {

            lbPin.Text = lbPin.Text + "5";
            verificarelungimePIN();
        }

        private void button16_Click(object sender, EventArgs e)
        {

            lbPin.Text = lbPin.Text + "6";
            verificarelungimePIN();
        }

        private void button14_Click(object sender, EventArgs e)
        {

            lbPin.Text = lbPin.Text + "7";
            verificarelungimePIN();
        }

        private void button10_Click(object sender, EventArgs e)
        {

            lbPin.Text = lbPin.Text + "8";
            verificarelungimePIN();
        }

        private void button11_Click(object sender, EventArgs e)
        {

            lbPin.Text = lbPin.Text + "9";
            verificarelungimePIN();
        }

        private void button13_Click(object sender, EventArgs e)
        {

            lbPin.Text = lbPin.Text + "0";
            verificarelungimePIN();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            lbPin.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult iExit;
            if (limba == "romana")
            {
                iExit = MessageBox.Show("Dorești să renunți la schimbarea PIN?", "Sistem Bancomat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else
            {
                iExit = MessageBox.Show("Do you want to quit the PIN change?", "ATM Sistem", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            if (iExit == DialogResult.Yes)
            {
                Pin f1 = new Pin();
                f1.Show();
                this.Hide();
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
           

                con.Open();
            if (OK == true)
            {
                
                DialogResult iExit;
                if (limba == "romana")
                {
                    iExit = MessageBox.Show("Dorești să modifici PIN-ul?", "Sistem Bancomat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                else
                {
                    iExit = MessageBox.Show("Do you want change the PIN?", "ATM Sistem", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                if (iExit == DialogResult.Yes)
                {
                    MySqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update bancomat set PIN= '" + lbPin.Text + "' where PIN = '" + PIN + "'";
                    cmd.ExecuteNonQuery();
                    if (limba == "romana")
                    {
                        MessageBox.Show("PIN-ul a fost modificat cu succes");
                    }
                    else
                    {
                        MessageBox.Show("The PIN was change");
                    }
                    Pin f1 = new Pin();
                    f1.Show();
                    this.Hide();

                }
                
            }
            con.Close();
        }

        private void SchimbarePIN_Load(object sender, EventArgs e)
        {
            label5.Visible = false;
            selectare_limba();
        }
        public void preluare_date_Baza()
        {
            MySqlCommand cmd;
            MySqlDataReader mdr;
            string selectQuery = "select * from bancomat where PIN = '" + lbPin.Text + "'";
            cmd = new MySqlCommand(selectQuery, con);
            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                PIN = mdr.GetInt32("PIN");
                OK = true;
                label1.Visible = false;
                label5.Visible = true;
                label4.Visible = false;
                button1.Enabled = false;
            }
            else
            {
                MessageBox.Show("PIN Invalid");
            }
            lbPin.Text = "";
            mdr.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            preluare_date_Baza();
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Pin f1 = new Pin();
            f1.Show();
            this.Hide();
        }

        public void selectare_limba()
        {
            if ( limba == "romana")
            {
                label4.Text = "Validați Vechiul PIN";
                label2.Text = "Renunțare Schimbare PIN";
                label1.Text = "Introduceți vechiul cod PIN";
                label5.Text = "Introduceți noul cod PIN";
            }else if ( limba == "engleza")
            {
                label4.Text = "Validate Old PIN";
                label2.Text = "Renouncing Change PIN";
                label1.Text = "Insert old PIN";
                label5.Text = "Insert new PIN";
            }
        }

        
    }
}
