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
    public partial class Meni : Form
    {
        public Meni()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            GlobalneVarijable.start.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Pjesme pjesme = new Pjesme();
            this.Close();
            pjesme.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Albumi album = new Albumi();
            album.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Izvodjaci izvodjaci = new Izvodjaci();
            izvodjaci.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Plejliste plej = new Plejliste();
            plej.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Svidjanje svidj = new Svidjanje();
            svidj.Show();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Pracenja p = new Pracenja();
            p.Show();
            this.Close();
            
        }

        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["serenada"].ConnectionString;

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Da li ste sigurni da želite da izbrišete nalog?", "Potvrda brisanja", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Provjera odgovora korisnika
            if (result == DialogResult.Yes)
            {
                int userId = GlobalneVarijable.korisnik.IdKorisnika;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand deleteCommand = new MySqlCommand("DeleteUser", connection);
                    deleteCommand.CommandType = CommandType.StoredProcedure;
                    deleteCommand.Parameters.AddWithValue("@userId", userId);
                    deleteCommand.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Vaš nalog je uspješno izbrisan!");
                    GlobalneVarijable.start.Show();
                    this.Close();
                }
            }
        }
    }
}
