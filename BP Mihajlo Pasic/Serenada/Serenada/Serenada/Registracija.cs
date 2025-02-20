using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serenada
{
    public partial class Registracija : Form
    {
        public Registracija()
        {
            InitializeComponent();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Ime")
            {
                textBox1.Text = "";
                textBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Ime";
                textBox1.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Prezime")
            {
                textBox2.Text = "";
                textBox2.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Text = "Prezime";
                textBox2.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Email")
            {
                textBox3.Text = "";
                textBox3.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                textBox3.Text = "Email";
                textBox3.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Lozinka")
            {
                textBox4.Text = "";
                textBox4.ForeColor = System.Drawing.SystemColors.WindowText; 
                if (checkBox1.Checked == false)
                {
                    textBox4.PasswordChar = '*';
                }
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                textBox4.Text = "Lozinka";
                textBox4.ForeColor = System.Drawing.SystemColors.GrayText;
                if (checkBox1.Checked == true || checkBox1.Checked == false) { textBox4.PasswordChar = '\0'; }
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "Potvrdite lozinku")
            {
                textBox5.Text = "";
                textBox5.ForeColor = System.Drawing.SystemColors.WindowText; 
                if (checkBox2.Checked == false)
                {
                    textBox5.PasswordChar = '*';
                }
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                textBox5.Text = "Potvrdite lozinku";
                textBox5.ForeColor = System.Drawing.SystemColors.GrayText;
                if (checkBox2.Checked == true || checkBox2.Checked == false) { textBox5.PasswordChar = '\0'; }
            }
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (textBox6.Text == "Telefon")
            {
                textBox6.Text = "";
                textBox6.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox6.Text))
            {
                textBox6.Text = "Telefon";
                textBox6.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox4.Text != "Lozinka")
            {
                textBox4.PasswordChar = checkBox1.Checked ? '\0' : '*';
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox5.Text != "Potvrdite lozinku")
            {
                textBox5.PasswordChar = checkBox2.Checked ? '\0' : '*';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GlobalneVarijable.start.Show();
            this.Close();
        }

        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["serenada"].ConnectionString;

        private void button2_Click(object sender, EventArgs e)
        {
            string ime = textBox1.Text;
            string prezime = textBox2.Text;
            string email = textBox3.Text;
            string lozinka = textBox4.Text;
            string potvrdaLozinke = textBox5.Text;
            string telefon = textBox6.Text;
            
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("RegistracijaKorisnika", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_ime", ime);
                    cmd.Parameters.AddWithValue("@p_prezime", prezime);
                    cmd.Parameters.AddWithValue("@p_email", email);
                    cmd.Parameters.AddWithValue("@p_lozinka", lozinka);
                    cmd.Parameters.AddWithValue("@p_potvrda_lozinke", potvrdaLozinke);
                    cmd.Parameters.AddWithValue("@p_telefon", telefon);

                    // Definiranje izlaznog parametra
                    MySqlParameter outputParam = new MySqlParameter("@p_uspjeh", MySqlDbType.Bit);
                    outputParam.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(outputParam);

                    cmd.ExecuteNonQuery();

                    // Dohvaćanje vrijednosti izlaznog parametra
                    bool uspjeh = Convert.ToBoolean(cmd.Parameters["@p_uspjeh"].Value);

                    if (uspjeh)
                    {
                        MessageBox.Show("Registracija uspješna!", "Čestitamo!");
                    }
                    else
                    {
                        MessageBox.Show("Registracija NIJE uspješna! Lozinke se ne podudaraju ili postoji nalog sa unijetim mejlom u sistemu ili nisu popunjena sva polja.", "Greška!");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Email adresa je vec pridruzena drugom korisnickom nalogu!");
                }
            }
        }
    }
}
