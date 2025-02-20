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
    public partial class Izvodjaci : Form
    {
        public Izvodjaci()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Meni meni = new Meni();
            meni.Show();
            this.Close();
        }

        private void Izvodjaci_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["serenada"].ConnectionString;

        private void LoadData()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT Naziv, ZemljaPorijekla, GodinaFormiranja, BrojPratilaca, KratakOpis FROM IZVODJAC ORDER BY Naziv", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;

                connection.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text.Trim();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT Naziv, ZemljaPorijekla, GodinaFormiranja, BrojPratilaca, KratakOpis FROM izvodjac WHERE Naziv LIKE @searchValue", connection);
                command.Parameters.AddWithValue("@searchValue", "%" + searchValue + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;

                connection.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int idKorisnika = GlobalneVarijable.korisnik.IdKorisnika;
            string nazivIzvodjaca = dataGridView1.SelectedRows[0].Cells["Naziv"].Value.ToString();

            int idIzvodjaca = GetIdIzvodjaca(nazivIzvodjaca);

            if (idIzvodjaca == -1)
            {
                MessageBox.Show("Izvodjac nije pronađen.");
                return;
            }

            if (ProvjeriPracenjeIzvodjaca(idKorisnika, idIzvodjaca))
            {
                MessageBox.Show("Izvodjac je već dodat u listu praćenja.");
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("ZapratiIzvodjaca", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@p_idKorisnika", idKorisnika);
                command.Parameters.AddWithValue("@p_idIzvodjaca", idIzvodjaca);

                command.ExecuteNonQuery();
                LoadData();
                MessageBox.Show("Izvodjac je uspješno dodat u listu praćenja.");
                connection.Close();
            }
        }

        private int GetIdIzvodjaca(string nazivIzvodjaca)
        {
            int idIzvodjaca = -1;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT IdIzvodjaca FROM IZVODJAC WHERE Naziv = @Naziv", connection);
                command.Parameters.AddWithValue("@Naziv", nazivIzvodjaca);

                object result = command.ExecuteScalar();
                if (result != null)
                {
                    idIzvodjaca = Convert.ToInt32(result);
                }

                connection.Close();
            }

            return idIzvodjaca;
        }

        private bool ProvjeriPracenjeIzvodjaca(int idKorisnika, int idIzvodjaca)
        {
            bool postojiPracenje = false;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM PRACENJE WHERE KORISNIK_IdKorisnika = @IdKorisnika AND IZVODJAC_IdIzvodjaca = @IdIzvodjaca", connection);
                command.Parameters.AddWithValue("@IdKorisnika", idKorisnika);
                command.Parameters.AddWithValue("@IdIzvodjaca", idIzvodjaca);

                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count > 0)
                {
                    postojiPracenje = true;
                }

                connection.Close();
            }

            return postojiPracenje;
        }
    }
}
