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
    public partial class RetragereNumerar : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=folder");
        string limba = Form1.passinglimba;
        string pin = Pin.passingPin;
        public static string suma;
        bool ok = false;
        bool money = true;
        int disponibil,suma_f;
        string codul = Pin.cardcod;
        string data, ora, tranzactia;

        public RetragereNumerar()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Pin pin = new Pin();
            pin.Show();
            this.Hide();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            verif_numerar(150);
            if (money == true)
            {
                retragere();
                con.Open();
                istoric(150);
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update bancomat set Suma= Suma - 150 where PIN = '" + pin + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                suma = "150";
            }
        }

        private void RetragereNumerar_Load(object sender, EventArgs e)
        {
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            selectare_limba();
            
        }

        public void retragere()
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = true;
            label11.Visible = true;
            label12.Visible = true;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button1.Enabled = false;
            button8.Enabled = false;
            ok = true;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ( ok == true)
            {
                
                Chitanta f1 = new Chitanta();
                f1.Show();
                this.Hide();

            }
            else
            {
                verif_numerar(50);
                if (money == true)
                {
                    retragere();
                    con.Open();
                    istoric(50);
                    MySqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update bancomat set Suma= Suma - 50 where PIN = '" + pin + "'";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    suma = "50";
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (ok == true)
            {
                Form1 f1 = new Form1();
                f1.Show();
                this.Hide();
            }
            else
            {
                verif_numerar(100);
                if (money == true)
                {
                    retragere();
                    con.Open();
                    istoric(100);
                    MySqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update bancomat set Suma= Suma - 100 where PIN = '" + pin + "'";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    suma = "100";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            verif_numerar(200);
            if (money == true)
            {
                retragere();
                con.Open();
                istoric(200);
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update bancomat set Suma= Suma - 200 where PIN = '" + pin + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                suma = "200";
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            verif_numerar(400);
            if (money == true) { 
            retragere();
            con.Open();
            istoric(400);
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update bancomat set Suma= Suma - 400 where PIN = '" + pin + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            suma = "400";
        }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            verif_numerar(20);
            if (money == true)
            {
                retragere();
                con.Open();
                istoric(20);
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update bancomat set Suma= Suma - 20 where PIN = '" + pin + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                suma = "20";
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            AltaSuma AS = new AltaSuma();
            AS.Show();
            this.Hide();
        }

        public void selectare_limba()
        {
            if ( limba == "romana")
            {
                label12.Text = "Doriți chitanță?";
                label5.Text = "Selectați suma pe care doriți să o retrageți din contul dv.";
                label6.Text = "ANULARE";
                label8.Text = "ALTĂ SUMĂ";
                label10.Text = "DA";
                label11.Text = "NU";
            }
            else
            {
                label12.Text = "Do you want the receipt?";
                label5.Text = "Select the sum you want retreat from your account";
                label6.Text = "CANCEL";
                label8.Text = "ANOTHER SUM";
                label10.Text = "YES";
                label11.Text = "NO";
            }
        }

        public void verif_numerar( int bani)
        {
            con.Open();
            obt_suma();
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
           
            con.Close();
        }

        public void istoric( int retragere)
        {
            var src = DateTime.Now;
            data = src.Day.ToString() + "." + src.Month.ToString() + "." + src.Year.ToString();
            ora = src.Hour.ToString() + ":" + src.Minute.ToString() + ":" + src.Second.ToString();
            tranzactia = "Retragere numerar:" + retragere;
            suma_f = disponibil - retragere;            
            string newCon = "insert into istoricbancomat(cod_card,Data,Ora,Suma_i,Tranzactia,Suma_f) VALUES ('" + codul + "','" + data + "','" + ora + "','" + disponibil + "','" + tranzactia + "','" + suma_f + "')";
            MySqlCommand cmd = new MySqlCommand(newCon, con);
            cmd.ExecuteNonQuery();
        }

        public void obt_suma()
        {
            MySqlCommand cmd;
            MySqlDataReader mdr;
            string selectQuery = "select * from bancomat where PIN = '" + pin + "'";
            cmd = new MySqlCommand(selectQuery, con);
            mdr = cmd.ExecuteReader();
            if (mdr.Read())
            {
                disponibil = mdr.GetInt32("Suma");

            }
            mdr.Close();
        }

        
    }

}
    

