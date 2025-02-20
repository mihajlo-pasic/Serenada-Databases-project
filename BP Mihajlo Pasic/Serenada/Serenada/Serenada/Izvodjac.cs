using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serenada
{
    internal class Izvodjac
    {
        public int IdIzvodjaca { get; set; }
        public string Naziv { get; set; }
        public string ZemljaPorijekla { get; set; }
        public int GodinaFormiranja { get; set; }
        public int BrojPratilaca { get; set; }
        public string KratakOpis { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Izvodjac izvodjac &&
                   IdIzvodjaca == izvodjac.IdIzvodjaca;
        }

        public override int GetHashCode()
        {
            return -1221475543 + IdIzvodjaca.GetHashCode();
        }

        public override string ToString()
        {
            return Naziv;
        }
    }
}
