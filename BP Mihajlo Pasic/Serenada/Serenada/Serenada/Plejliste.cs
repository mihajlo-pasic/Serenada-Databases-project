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
    public partial class Plejliste : Form
    {
        public Plejliste()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Meni meni = new Meni();
            meni.Show();
            this.Close();
        }

        private void Plejliste_Load(object sender, EventArgs e)
        {
            PrikaziPlejliste();
        }
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["serenada"].ConnectionString;

        private void PrikaziPlejliste()
        {
            int idKorisnika = GlobalneVarijable.korisnik.IdKorisnika;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("PrikaziPlejlisteZaKorisnika", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@p_idKorisnika", idKorisnika);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;

                connection.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string filter = textBox1.Text.Trim();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("PrikaziPlejlisteZaKorisnika", connection);
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

        private void button3_Click(object sender, EventArgs e)
        {
            string nazivPlejliste = dataGridView1.SelectedRows[0].Cells["Naziv"].Value.ToString();

            int idPlejliste = GetIdPlejlisteByNaziv(nazivPlejliste);

            if (idPlejliste == -1)
            {
                MessageBox.Show("Plejlista nije pronađena.");
                return;
            }

            DataTable dtPjesme = GetPjesmeByPlejlistaId(idPlejliste);
            Prikaz prikaz = new Prikaz(dtPjesme);
            prikaz.ShowDialog();
        }

        private int GetIdPlejlisteByNaziv(string nazivPlejliste)
        {
            int idPlejliste = -1;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT IdPLEJLISTA FROM PLEJLISTA WHERE Naziv = @Naziv", connection);
                command.Parameters.AddWithValue("@Naziv", nazivPlejliste);

                object result = command.ExecuteScalar();
                if (result != null)
                {
                    idPlejliste = Convert.ToInt32(result);
                }

                connection.Close();
            }

            return idPlejliste;
        }

        private DataTable GetPjesmeByPlejlistaId(int idPlejliste)
        {
            DataTable dtPjesme = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("GetPjesmeByPlejlistaId", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@p_idPlejliste", idPlejliste);

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(dtPjesme);
                }

                connection.Close();
            }

            return dtPjesme;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string nazivPlejliste = dataGridView1.SelectedRows[0].Cells["Naziv"].Value.ToString();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("DeletePlaylist", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@p_idKorisnika", GlobalneVarijable.korisnik.IdKorisnika);
                command.Parameters.AddWithValue("@p_nazivPlejliste", nazivPlejliste);

                command.ExecuteNonQuery();
                PrikaziPlejliste();
            }

        }
    }
}
