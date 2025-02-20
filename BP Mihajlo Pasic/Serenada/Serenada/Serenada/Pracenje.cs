using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serenada
{
    internal class Pracenje
    {
        public DateTime DatumDodavanja { get; set; }
        public Korisnik Korisnik { get; set; }
        public Izvodjac Izvodjac { get; set; }

        /*public override bool Equals(object obj)
        {
            return obj is Pracenje zaprat &&
                   Pracenje == zaprat.PersonId;
        }

        public override int GetHashCode()
        {
            return -1255590651 + PersonId.GetHashCode();
        }

        public override string ToString()
        {
            return LastName + ", " + FirstName;
        }*/
    }
}
