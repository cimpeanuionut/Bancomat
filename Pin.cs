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
    public partial class Pin : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=folder");
        public static string passingPin;
        public static string cardcod;
        string limba = Form1.passinglimba;
        Boolean PIN = false;
        public Pin()
        {
            InitializeComponent();
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
                iExit = MessageBox.Show("Dorești să părăsești tranzacția?", "Sistem Bancomat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }else 
            {
                iExit = MessageBox.Show("Do you want to leave the transaction?", "ATM Sistem", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            if ( iExit == DialogResult.Yes)
            {
                Form1 f1 = new Form1();
                f1.Show();
                this.Hide();
            }
        }

        private void Pin_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label6.Visible = false;
            selectare_limba();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            con.Open();
            MySqlCommand cmd;
            MySqlDataReader mdr;
            string selectQuery = "select * from bancomat where PIN = '" + lbPin.Text + "'";
            cmd = new MySqlCommand(selectQuery, con);
            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = false;
                label6.Visible = true;
                passingPin = lbPin.Text;
                cardcod = mdr.GetString("cod_card");
                lbPin.Text = "";
                PIN = true;
                
            }
            else
            {
                if (limba == "romana")
                {
                    MessageBox.Show("PIN-ul este invalid");
                }
                else
                {
                    MessageBox.Show("Invalid PIN");
                }
                lbPin.Text = "";
            }

            mdr.Close();
            con.Close();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (PIN == true)
            {
                InterogareSold IST = new InterogareSold();
                IST.Show();
                this.Hide();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ( PIN ==true)
            {
                SchimbarePIN SP = new SchimbarePIN();
                SP.Show();
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (PIN == true)
            {
                RetragereNumerar RN = new RetragereNumerar();
                RN.Show();
                this.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (PIN == true)
            {
                RateCredit PF = new RateCredit();
                PF.Show();
                this.Hide();
            }
        }

        public void verificarelungimePIN()
        {
            if (lbPin.Text.Length > 4)
            {
                if (limba == "romana")
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

        public void selectare_limba()
        {
            if ( limba == "romana")
            {
                label3.Text = "Interogare Sold";
                label4.Text = "Schimbare PIN";
                label1.Text = "Retragere Numerar";
                label2.Text = "Rate Credit";
                label6.Text = "Selectați o tranzacție";
                label5.Text = "Introduceți PIN";
            }else if ( limba == "engleza")
            {
                label3.Text = "Sold Interrogation";
                label4.Text = "Change PIN";
                label1.Text = "Cash Withdrawal";
                label2.Text = "Credits Rate";
                label6.Text = "Select a transaction";
                label5.Text = "Insert PIN";
            }
        }

    }
}
