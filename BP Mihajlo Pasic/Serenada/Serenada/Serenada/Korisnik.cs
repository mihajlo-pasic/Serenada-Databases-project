using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serenada
{
    internal class Korisnik
    {
        public int IdKorisnika { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string Telefon { get; set; }
        public DateTime DatumRegistracije { get; set; }
        public DateTime DatumBrisanja { get; set; }
        public bool Izbrisan { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Korisnik korisnik &&
                   IdKorisnika == korisnik.IdKorisnika;
        }

        public override int GetHashCode()
        {
            return -1255590651 + IdKorisnika.GetHashCode();
        }

        public override string ToString()
        {
            return Prezime + ", " + Ime;
        }

        /*public Korisnik()
        {
            IdKorisnika = 0;
            Ime = "";
            Prezime = "";
            Email = "";
            Lozinka = "";
            Telefon = "";
            DatumRegistracije = new DateTime();
            DatumBrisanja = new DateTime();
            Izbrisan = false;
        }*/

        /*public int getID() { return ID; }
        public string getIme() { return Ime; }
        public string getPrezime() { return Prezime; }
        public string getEmail() { return Email; }
        public string getLozinka() { return Lozinka; }
        public string getTelefon() { return Telefon; }
        public DateTime getDatumRegistracije() { return DatumRegistracije; }
        public DateTime getDatumBrisanja() { return DatumBrisanja; }
        public bool isIzbrisan() { return Izbrisan; }

        public void setID(int ID) { this.ID = ID; }
        public void setIme(string Ime) { this.Ime = Ime; }
        public void setPrezime(string Prezime) { this.Prezime = Prezime; }
        public void setEmail(string Email) { this.Email = Email; }
        public void setLozinka(string Lozinka) { this.Lozinka = Lozinka; }
        public void setTelefon(string Telefon) { this.Telefon = Telefon; }
        public void setDatumRegistracije(DateTime DatumRegistracije) { this.DatumRegistracije = DatumRegistracije; }
        public void setDatumBrisanja(DateTime DatumBrisanja) { this.DatumBrisanja = DatumBrisanja; }
        public void setIzbrisan(bool Izbrisan) { this.Izbrisan = Izbrisan; }*/
    }
}
