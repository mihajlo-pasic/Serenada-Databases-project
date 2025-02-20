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
    public partial class Albumi : Form
    {
        public Albumi()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Meni meni = new Meni();
            meni.Show();
            this.Close();
        }

        private void Albumi_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["serenada"].ConnectionString;

        private void LoadData()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT * FROM AlbumView", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchValue = textBox1.Text.Trim();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT * FROM AlbumView WHERE Naziv LIKE @searchValue", connection);
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
            string nazivAlbuma = dataGridView1.SelectedRows[0].Cells["Naziv"].Value.ToString();

            int idAlbuma = GetIdAlbumaByNaziv(nazivAlbuma);

            if (idAlbuma == -1)
            {
                MessageBox.Show("Album nije pronađen.");
                return;
            }

            DataTable dtPjesme = GetPjesmeByAlbumId(idAlbuma);
            Prikaz prikaz = new Prikaz(dtPjesme);
            prikaz.ShowDialog();
        }

        private int GetIdAlbumaByNaziv(string nazivAlbuma)
        {
            int idAlbuma = -1;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("SELECT IdAlbuma FROM ALBUM WHERE Naziv = @Naziv", connection);
                command.Parameters.AddWithValue("@Naziv", nazivAlbuma);

                object result = command.ExecuteScalar();
                if (result != null)
                {
                    idAlbuma = Convert.ToInt32(result);
                }

                connection.Close();
            }

            return idAlbuma;
        }

        private DataTable GetPjesmeByAlbumId(int idAlbuma)
        {
            DataTable dtPjesme = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand("GetPjesmeByAlbumId", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@p_idAlbuma", idAlbuma);

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(dtPjesme);
                }

                connection.Close();
            }

            return dtPjesme;
        }
    }

}
