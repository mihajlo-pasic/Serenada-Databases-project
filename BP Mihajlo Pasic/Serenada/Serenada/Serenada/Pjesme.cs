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
using MySql.Data.MySqlClient;

namespace Serenada
{
    public partial class Pjesme : Form
    {
       
        public Pjesme()
        {
            InitializeComponent();
        }

        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["serenada"].ConnectionString;

        private void Pjesme_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT * FROM PjesmeView", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;

                connection.Close();
            }
        }

     

        private void button1_Click(object sender, EventArgs e)
        {
            Meni meni = new Meni();
            meni.Show();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text.Trim();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT * FROM PjesmeView WHERE Naziv LIKE @searchValue", connection);
                command.Parameters.AddWithValue("@searchValue", "%" + searchValue + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;

                connection.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int idKorisnika = GlobalneVarijable.korisnik.IdKorisnika;
            string nazivPjesme = dataGridView1.SelectedRows[0].Cells["Naziv"].Value.ToString();

            int idPjesme = GetIdPjesme(nazivPjesme);

            if (idPjesme == -1)
            {
                MessageBox.Show("Pjesma nije pronađena.");
                return;
            }

            if (ProvjeriPostojanjeSvidjanja(idKorisnika, idPjesme))
            {
                MessageBox.Show("Pjesma je već dodata u listu omiljenih.");
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("DodajPjesmuUSvidjanja", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@p_idKorisnika", idKorisnika);
                command.Parameters.AddWithValue("@p_idPjesme", idPjesme);

                command.ExecuteNonQuery();
                LoadData();
                MessageBox.Show("Pjesma je uspješno dodata u listu omiljenih.");
                connection.Close();
            }
        }

        private int GetIdPjesme(string nazivPjesme)
        {
            int idPjesme = -1;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT IdPjesme FROM PJESMA WHERE Naziv = @Naziv", connection);
                command.Parameters.AddWithValue("@Naziv", nazivPjesme);

                object result = command.ExecuteScalar();
                if (result != null)
                {
                    idPjesme = Convert.ToInt32(result);
                }

                connection.Close();
            }

            return idPjesme;
        }

        private bool ProvjeriPostojanjeSvidjanja(int idKorisnika, int idPjesme)
        {
            bool postojiSvidjanje = false;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM SVIDJANJE WHERE KORISNIK_IdKorisnika = @IdKorisnika AND PJESMA_IdPjesme = @IdPjesme", connection);
                command.Parameters.AddWithValue("@IdKorisnika", idKorisnika);
                command.Parameters.AddWithValue("@IdPjesme", idPjesme);

                int count = Convert.ToInt32(command.ExecuteScalar());
                if (count > 0)
                {
                    postojiSvidjanje = true;
                }

                connection.Close();
            }

            return postojiSvidjanje;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GlobalneVarijable.pjesma.Naziv = dataGridView1.SelectedRows[0].Cells["Naziv"].Value.ToString();
            Izbornik izbornik = new Izbornik();
            izbornik.ShowDialog();
            
        }
    }
}
