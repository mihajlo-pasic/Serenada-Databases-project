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
    public partial class Svidjanje : Form
    {
        public Svidjanje()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Meni meni = new Meni();
            meni.Show();
            this.Close();
        }

        private void Svidjanje_Load(object sender, EventArgs e)
        {
            PrikaziOmiljenePjesme();
        }

        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["serenada"].ConnectionString;

        private void PrikaziOmiljenePjesme()
        {
            int idKorisnika = GlobalneVarijable.korisnik.IdKorisnika;
            
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("PrikaziOmiljenePjesmeKorisnika", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@p_idKorisnika", idKorisnika);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string filter = textBox1.Text.Trim();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("PrikaziOmiljenePjesmeKorisnika", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@p_idKorisnika", GlobalneVarijable.korisnik.IdKorisnika);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                DataView dataView = new DataView(dataTable);
                dataView.RowFilter = string.Format("Naziv LIKE '%{0}%'", filter);

                dataGridView1.DataSource = dataView;

                connection.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string nazivPjesme = dataGridView1.SelectedRows[0].Cells["Naziv"].Value.ToString();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("RemovePjesmaFromSvidjanje", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Dodajemo parametre uskladištene procedure
                command.Parameters.AddWithValue("@p_idKorisnika", GlobalneVarijable.korisnik.IdKorisnika);
                command.Parameters.AddWithValue("@p_nazivPjesme", nazivPjesme);

                command.ExecuteNonQuery();

                connection.Close();
            }
            PrikaziOmiljenePjesme();
        }
    }
}
