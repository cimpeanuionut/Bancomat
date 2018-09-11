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
    public partial class RateCredit : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=folder");
        string limba = Form1.passinglimba;
        public static bool ok1 = false;
        public static string credit;
        bool ok = false;
        string cod = Pin.cardcod;
        string data, ora, tranzactia;
        int suma_i, suma_f;
        string pin = Pin.passingPin;
        int bani;
        public RateCredit()
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
        private void button14_Click(object sender, EventArgs e)
        {
            lbPin.Text = lbPin.Text + "7";
        }
        private void button7_Click(object sender, EventArgs e)
        {
            lbPin.Text = lbPin.Text + "2";
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
                iExit = MessageBox.Show("Do you want to leave the transaction?", "Atm Sistem", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }


            if (iExit == DialogResult.Yes)
            {
                Form1 f1 = new Form1();
                f1.Show();
                this.Hide();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            lbPin.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ok == false)
            {
                lbPin.Text = "";
            }
            else
            {
                Form1 f1 = new Form1();
                f1.Show();
                this.Hide();
            }
        }

        private void RateCredit_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
            label3.Visible = false;
            label6.Visible = false;
            selectare_limba();
        }

        public void final()
        {
            label1.Visible = true;
            label3.Visible = true;
            label6.Visible = true;
            label2.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ok == false)
            {
                bani = Int32.Parse(lbPin.Text.ToString());
                credit = bani.ToString();
                if (bani % 10 == 0)
                {
                    if (bani <= 3000)
                    {
                        con.Open();
                        istoric();
                        MySqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update bancomat set imprumut=imprumut + '" + bani + "' where PIN = '" + pin + "'";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        ok = true;
                        final();
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
            else
            {
                ok1 = true;
                Chitanta f1 = new Chitanta();
                f1.Show();
                this.Hide();
                ok1 = false;
            }

        }

        public void selectare_limba()
        {
            if (limba == "romana")
            {
                label3.Text = "Doriți chitanță?";
                label5.Text = "Introduceți o sumă multiplu de 10";
                label6.Text = "Nu";
                label1.Text = "Da";
                label4.Text = "Corect, apăsați aici";
                label2.Text = "Incorect, apăsați aici";
            }
            else
            {
                label3.Text = "Do you want the receipt?";
                label5.Text = "Enter a sum multiple be 10";
                label6.Text = "No";
                label1.Text = "Yes";
                label4.Text = "Correct, click here";
                label2.Text = "Wrong, click here";
            }
        }

        public void istoric()
        {
            var src = DateTime.Now;
            data = src.Day.ToString() + "." + src.Month.ToString() + "." + src.Year.ToString();
            ora = src.Hour.ToString() + ":" + src.Minute.ToString() + ":" + src.Second.ToString();
            tranzactia = "Credit de:" +bani;
            obt_i();
            suma_f = suma_i + bani;

            string newCon = "insert into istoricbancomat(cod_card,Data,Ora,Suma_i,Tranzactia,Suma_f) VALUES ('" + cod + "','" + data + "','" + ora + "','" + suma_i + "','" + tranzactia + "','" + suma_f+ "')";
            MySqlCommand cmd = new MySqlCommand(newCon, con);
            cmd.ExecuteNonQuery();

        }

        public void obt_i()
        {
            MySqlCommand cmd;
            MySqlDataReader mdr;
            string selectQuery = "select * from bancomat where PIN = '" + pin + "'";
            cmd = new MySqlCommand(selectQuery, con);
            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                suma_i = mdr.GetInt32("imprumut");
                

            }
            mdr.Close();
        }
    }
}
