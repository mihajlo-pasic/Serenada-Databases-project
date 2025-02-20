using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serenada
{
    internal class Pjesma
    {
        public int IdPjesme { get; set; }
        public string Naziv { get; set; }
        public TimeSpan Trajanje { get; set; }
        public DateTime DatumIzdanja { get; set; }
        public int BrojSvidjanja { get; set; }
        public string TekstPjesme { get; set; }
        public Album Album { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Pjesma pjesma &&
                   IdPjesme == pjesma.IdPjesme;
        }

        public override int GetHashCode()
        {
            return -1077227218 + IdPjesme.GetHashCode();
        }

        public override string ToString()
        {
            return Naziv;
        }
    }
}
