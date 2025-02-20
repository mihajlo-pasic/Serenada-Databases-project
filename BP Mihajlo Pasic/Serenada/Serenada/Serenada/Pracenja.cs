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
    public partial class Pracenja : Form
    {
        public Pracenja()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Meni meni = new Meni();
            meni.Show();
            this.Close();
        }

        private void Pracenja_Load(object sender, EventArgs e)
        {
            PrikaziPratioceZaKorisnika();
        }
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["serenada"].ConnectionString;

        private void PrikaziPratioceZaKorisnika()
        {
            int idKorisnika = GlobalneVarijable.korisnik.IdKorisnika; // Pretpostavljam da je ovdje GlobalniIdKorisnika globalna varijabla koja sadrži IdKorisnika

            
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("PrikaziPratioceZaKorisnika", connection);
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

                MySqlCommand command = new MySqlCommand("PrikaziPratioceZaKorisnika", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@p_idKorisnika", GlobalneVarijable.korisnik.IdKorisnika);

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                DataView dataView = new DataView(dataTable);
                dataView.RowFilter = string.Format("Izvodjac LIKE '%{0}%'", filter);

                dataGridView1.DataSource = dataView;

                connection.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string nazivIzvodjaca = dataGridView1.SelectedRows[0].Cells["Izvodjac"].Value.ToString();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("RemoveIzvodjacFromPracenje", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@p_idKorisnika", GlobalneVarijable.korisnik.IdKorisnika);
                command.Parameters.AddWithValue("@p_nazivIzvodjaca", nazivIzvodjaca);

                command.ExecuteNonQuery();
                PrikaziPratioceZaKorisnika();

                connection.Close();
            }
        }
    }
}
