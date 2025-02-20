using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serenada
{
    internal class Plejlista
    {
        public int IdPlejliste { get; set; }
        public string Naziv { get; set; }
        public Korisnik Korisnik { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Plejlista plej &&
                   IdPlejliste == plej.IdPlejliste;
        }

        public override int GetHashCode()
        {
            return -981443820 + IdPlejliste.GetHashCode();
        }

        public override string ToString()
        {
            return Naziv;
        }
    }
}
