using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Serenada
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
            GlobalneVarijable.start = this;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Email")
            {
                textBox1.Text = "";
                textBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Lozinka")
            {
                textBox2.Text = "";
                textBox2.ForeColor = System.Drawing.SystemColors.WindowText;
                if (checkBox1.Checked == false)
                {
                    textBox2.PasswordChar = '*';
                }
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Email";
                textBox1.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                textBox2.Text = "Lozinka";
                textBox2.ForeColor = System.Drawing.SystemColors.GrayText;
                if (checkBox1.Checked == true || checkBox1.Checked == false) { textBox2.PasswordChar = '\0'; }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "Lozinka")
            {
                textBox2.PasswordChar = checkBox1.Checked ? '\0' : '*';
            }
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Purple;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
        }

        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["serenada"].ConnectionString;

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string password = textBox2.Text;

            // Kreiramo MySqlConnection objekt
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Otvaramo konekciju
                    connection.Open();

                    // Kreiramo MySqlCommand objekt
                    MySqlCommand command = new MySqlCommand("VerifyUser", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Dodajemo parametre
                    command.Parameters.AddWithValue("@p_email", email);
                    command.Parameters.AddWithValue("@p_password", password);
                    command.Parameters.Add("@p_authenticated", MySqlDbType.Int32).Direction = System.Data.ParameterDirection.Output;

                    // Izvršavamo proceduru
                    command.ExecuteNonQuery();

                    // Čitamo output parametre
                    int isAuthenticated = Convert.ToInt32(command.Parameters["@p_authenticated"].Value);

                    MySqlCommand command2 = new MySqlCommand("PronadjiIdKorisnika", connection);
                    command2.CommandType = CommandType.StoredProcedure;
                    command2.Parameters.AddWithValue("@p_email", email);
                    command2.Parameters.Add("@p_idKorisnika", MySqlDbType.Int32).Direction = ParameterDirection.Output;

                    command2.ExecuteNonQuery();

                    int IdKorisnika = Convert.ToInt32(command2.Parameters["@p_idKorisnika"].Value);
                    GlobalneVarijable.korisnik.IdKorisnika = IdKorisnika;

                    if (isAuthenticated == 0)
                    {
                        MessageBox.Show("Nepravilna lozinka ili korisnik nije registrovan ili se pristupa sa izbrisanim nalogom!", "Greška!");

                    }
                    else
                    {
                        GlobalneVarijable.start.Hide();
                        Meni meni = new Meni();
                        meni.Show();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Greška! Nepravilan unos podataka!");
                }
            }
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            GlobalneVarijable.start.Hide();
            Registracija registracija = new Registracija();
            registracija.Show();
        }

        
    }
}
