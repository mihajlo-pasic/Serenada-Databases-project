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
    public partial class Izbornik : Form
    {
        public Izbornik()
        {
            InitializeComponent();
        }

        private void Izbornik_Load(object sender, EventArgs e)
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
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Naziv plejliste ne može biti prazan string!", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string nazivPlejliste = textBox1.Text;
                int idKorisnika = GlobalneVarijable.korisnik.IdKorisnika;

                DodajNovuPlejlistu(nazivPlejliste, idKorisnika);
            }
        }
        private void DodajNovuPlejlistu(string nazivPlejliste, int idKorisnika)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    MySqlCommand checkPlaylistCommand = new MySqlCommand("SELECT COUNT(*) FROM PLEJLISTA WHERE Naziv = @nazivPlejliste AND KORISNIK_IdKorisnika = @idKorisnika;", connection);
                    checkPlaylistCommand.Parameters.AddWithValue("@nazivPlejliste", nazivPlejliste);
                    checkPlaylistCommand.Parameters.AddWithValue("@idKorisnika", idKorisnika);
                    int count = Convert.ToInt32(checkPlaylistCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        // Plejlista već postoji, prikaži odgovarajuću poruku
                        MessageBox.Show("Plejlista sa navedenim nazivom već postoji!");
                        return;
                    }
                    // Dodaj novu plejlistu
                    MySqlCommand insertPlaylistCommand = new MySqlCommand("INSERT INTO PLEJLISTA (Naziv, KORISNIK_IdKorisnika) VALUES (@nazivPlejliste, @idKorisnika);", connection);
                    insertPlaylistCommand.Parameters.AddWithValue("@nazivPlejliste", nazivPlejliste);
                    insertPlaylistCommand.Parameters.AddWithValue("@idKorisnika", idKorisnika);
                    insertPlaylistCommand.ExecuteNonQuery();

                    // Pronađi IdPjesme na osnovu naziva pjesme
                    MySqlCommand selectSongIdCommand = new MySqlCommand("SELECT IdPjesme FROM PJESMA WHERE Naziv = @nazivPjesme;", connection);
                    selectSongIdCommand.Parameters.AddWithValue("@nazivPjesme", GlobalneVarijable.pjesma.Naziv);
                    int idPjesme = Convert.ToInt32(selectSongIdCommand.ExecuteScalar());

                    // Dodaj pjesmu u plejlistu
                    MySqlCommand insertSongCommand = new MySqlCommand("INSERT INTO LISTA (PLEJLISTA_idPLEJLISTA, PJESMA_IdPjesme, DatumDodavanja) VALUES ((SELECT IdPLEJLISTA FROM PLEJLISTA WHERE Naziv = @nazivPlejliste AND KORISNIK_IdKorisnika = @idKorisnika), @idPjesme, CURRENT_DATE());", connection);
                    insertSongCommand.Parameters.AddWithValue("@nazivPlejliste", nazivPlejliste);
                    insertSongCommand.Parameters.AddWithValue("@idKorisnika", idKorisnika);
                    insertSongCommand.Parameters.AddWithValue("@idPjesme", idPjesme);
                    insertSongCommand.ExecuteNonQuery();

                    connection.Close();
                    PrikaziPlejliste();
                    MessageBox.Show("Uspjesno kreirana nova plejlista!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Plejlista sa navedenim nazivom već postoji!");
                }
            }
        }

        private void DodajPjesmuUPlejlistu(string nazivPlejliste, int idKorisnika)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Pronađi IdPlejliste na osnovu naziva plejliste
                    MySqlCommand selectPlaylistIdCommand = new MySqlCommand("SELECT IdPLEJLISTA FROM PLEJLISTA WHERE Naziv = @nazivPlejliste AND KORISNIK_IdKorisnika = @idKorisnika;", connection);
                    selectPlaylistIdCommand.Parameters.AddWithValue("@nazivPlejliste", nazivPlejliste);
                    selectPlaylistIdCommand.Parameters.AddWithValue("@idKorisnika", idKorisnika);
                    int idPlejliste = Convert.ToInt32(selectPlaylistIdCommand.ExecuteScalar());

                    // Pronađi IdPjesme na osnovu naziva pjesme
                    MySqlCommand selectSongIdCommand = new MySqlCommand("SELECT IdPjesme FROM PJESMA WHERE Naziv = @nazivPjesme;", connection);
                    selectSongIdCommand.Parameters.AddWithValue("@nazivPjesme", GlobalneVarijable.pjesma.Naziv);
                    int idPjesme = Convert.ToInt32(selectSongIdCommand.ExecuteScalar());

                    // Dodaj pjesmu u plejlistu
                    MySqlCommand insertSongCommand = new MySqlCommand("INSERT INTO LISTA (PLEJLISTA_idPLEJLISTA, PJESMA_IdPjesme, DatumDodavanja) VALUES (@idPlejliste, @idPjesme, CURRENT_DATE());", connection);
                    insertSongCommand.Parameters.AddWithValue("@idPlejliste", idPlejliste);
                    insertSongCommand.Parameters.AddWithValue("@idPjesme", idPjesme);
                    insertSongCommand.ExecuteNonQuery();

                    PrikaziPlejliste();
                    MessageBox.Show("Uspjesno dodata pjesma na plejlistu!");
                    connection.Close();
                }
                catch (Exception ex)
                { 
                   MessageBox.Show("Pjesma se već nalazi u plejlisti!");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Molimo odaberite plejlistu.");
                return;
            }

            if (GlobalneVarijable.pjesma == null)
            {
                MessageBox.Show("Molimo odaberite pjesmu koju želite dodati u plejlistu.");
                return;
            }

            int idKorisnika = GlobalneVarijable.korisnik.IdKorisnika;
            string nazivPlejliste = dataGridView1.SelectedRows[0].Cells["Naziv"].Value.ToString();
            DodajPjesmuUPlejlistu(nazivPlejliste, idKorisnika);
        }
    }
}
